using CryptoAI_Upgraded.AI_Training.NeuralNetworks;
using CryptoAI_Upgraded.Datasets;
using CryptoAI_Upgraded.Datasets.DataWalkers;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using System.Diagnostics;

namespace CryptoAI_Upgraded.DataAnalasys
{
    internal class NerworkAccuraccyAnalizer
    {
        public async Task<NetworkAccAnalize> Analize(NeuralNetwork neuralNetwork, List<LocalKlinesDataset> datasets)
        {
            NetworkAccAnalize analize = new NetworkAccAnalize();
            Stopwatch timer = Stopwatch.StartNew();

            try
            {
                object referencedProgressInt = 0;
                LSTMDataWalker dataWalker = new LSTMDataWalker(datasets, neuralNetwork.networkConfig);

                do
                {
                    KLine pos = dataWalker.position;
                    List<double[]>? predictionResult = Walk(dataWalker, neuralNetwork, neuralNetwork.outputCount);

                    if (predictionResult == null) break;
                    analize.analizeStepsAmount++;
                    //getting error metrics and datas
                    double error = 0;

                    for (int i = 0; i < predictionResult[1].Length; i++)
                    {
                        if (predictionResult[1][i] == double.NaN)
                        {
                            analize.skippedSteps++;
                            continue;
                        }
                        error += Math.Abs(predictionResult[0][i] - predictionResult[1][i]);
                    }
                    error /= predictionResult[0].Length;
                    analize.errors.AddLast(error);
                    //getting guessed dir
                    double finalPred = 0;
                    double finalEx = 0;
                    //for (int i = 0; i < predictionResult[0].Length; i++)
                    //{
                    //    finalPred += predictionResult[0][i];
                    //    finalEx += predictionResult[1][i];
                    //}
                    double lastReal = predictionResult[0][predictionResult[0].Length - 1];
                    double lastPred = predictionResult[1][predictionResult[1].Length - 1];
                    analize.real.Add(lastReal);
                    analize.predict.Add(lastPred);
                    finalPred = lastReal - (double)pos.OpenPrice;
                    finalEx = lastPred - (double)pos.OpenPrice;
                    analize.guessedDirections.AddLast(getDirection(finalPred) == getDirection(finalEx) ? 1 : 0);
                } while (true);
                await Task.Yield();
                analize.averageError = analize.errors.Average();
                analize.guessedDirPercent = analize.guessedDirections.Average() * 100;
            }
            catch (Exception ex)
            {
                analize.exception = ex;
            }


            timer.Stop();
            analize.testDurationMillisec = timer.ElapsedMilliseconds;
            analize.success = true;
            return analize;
        }

        private List<double[]>? Walk(LSTMDataWalker dataWalker, NeuralNetwork neuralNetwork, int predictionsLength)
        {
            List<double> predictions = new List<double>();

            double[,] data = dataWalker.Walk(out var expected);
            if (dataWalker.isFinishedWalking()) return null;
            double[,,] input = Helpers.ConvertArrTo3DArray(data);
            //double[,,] normalized = Helpers.Normalization.Normalize(out double min, out double max, input);
            int predCounter = predictionsLength;
            while (predCounter > 0)
            {
                double[,,] dataWithMinMax = Helpers.ArrayValuesInjection.InjectValues(input, 0, 0);

                float[] predictionArr = neuralNetwork.Predict(dataWithMinMax);
                double prediction = Convert.ToDouble(predictionArr[0]);

                //double denormalized = Helpers.Normalization.Denormalize(prediction, min, max);
                predictions.Add(prediction);

                input = Helpers.ArrayValuesInjection.UpdateInput(input, prediction);
                predCounter--;
            }

            //getting expected arr
            List<double> expectedList = new List<double>(expected);
            int startStep = dataWalker.currentStep;
            int step = startStep;
            for (int i = 0; i < predictionsLength; i++)
            {
                expectedList.Add(expected[0]);

                dataWalker.WalkAt(step, out expected);
                if (dataWalker.isFinishedWalking()) return null;
                step++;
            }
            dataWalker.SetPosition(startStep);
            return new List<double[]>() { expectedList.ToArray(),
                predictions.ToArray() };
        }

        private int getDirection(double dir)
        {
            double neutralCoef = 0.003;
            return Math.Abs(dir) < neutralCoef ? 0 : Math.Sign(dir);
        }
    }

    public class NetworkAccAnalize
    {
        //success
        public bool success;
        public Exception exception;
        //datas
        public LinkedList<double> errors = new LinkedList<double>();
        public List<double> real = new List<double>();
        public List<double> predict = new List<double>();
        public LinkedList<double> guessedDirections = new LinkedList<double>();//1-guessed, 0-not guessed
        //metrics
        public int analizeStepsAmount = 0;
        public int skippedSteps = 0;
        public double averageError = 0;
        public double guessedDirPercent = 0;

        public float testDurationMillisec;
    }
}
