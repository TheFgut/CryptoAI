using CryptoAI_Upgraded.Datasets.DataWalkers;
using Keras.Layers;
using Keras.Models;
using Numpy;
using CryptoAI_Upgraded.Datasets;
using System.Drawing.Text;
using System.Reflection;
using System.ComponentModel;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks
{
    public class NeuralNetwork
    {
        public int inputsCount => neuralData.inputCount;
        public int outputCount => neuralData.outputCount;
        
        private Sequential model;
        private NNData neuralData;
        #region creation
        public NeuralNetwork(BindingList<NNLayerConfig> config)
        {
            if(config == null || config.Count < 2) throw new ArgumentNullException("NeuralNetwork.Creation failed. config cant be null or count cant be less than 2");
            this.model = CreateNetwork(config);
            neuralData = new NNData(config);
        }

        private Sequential CreateNetwork(BindingList<NNLayerConfig> config)
        {
            Sequential model = new Sequential();
            //creating input
            if (config[0].layerType == LayerType.LSTM) model.Add(CreateLSTMLayerFromConfig(config[0], config[1].layerType == LayerType.LSTM,
                new Keras.Shape(60, config[0].neuronsCount)));
            else if (config[0].layerType == LayerType.Dense) model.Add(CreateDenseLayerFromConfig(config[0],
                new Keras.Shape(config[0].neuronsCount)));
            for (int i = 1; i < config.Count; i++)
            {
                if (config[i].layerType == LayerType.LSTM)model.Add(CreateLSTMLayerFromConfig(config[i], config[i + 1].layerType == LayerType.LSTM));
                else if (config[i].layerType == LayerType.Dense) model.Add(CreateDenseLayerFromConfig(config[i]));
            }
            // Компиляция модели
            model.Compile(optimizer: new Keras.Optimizers.Nadam(),
                          loss: "mean_squared_error",
                          metrics: new string[] { "mae" });

            return model;
        }

        private LSTM CreateLSTMLayerFromConfig(NNLayerConfig config, bool returnSequences, Keras.Shape? inputShape = null)
        {
            string? activationString = config.activation == ActivationFunc.linear ? null : config.activation.ToString();
            return new LSTM(config.neuronsCount, input_shape: inputShape,
                activation: activationString, return_sequences: returnSequences);
        }

        private Dense CreateDenseLayerFromConfig(NNLayerConfig config, Keras.Shape? inputShape = null)
        {
            string? activationString = config.activation == ActivationFunc.linear ? null : config.activation.ToString();
            return new Dense(config.neuronsCount, input_shape: inputShape, activation: activationString);
        }
        #endregion
        /// <summary>
        /// for dense layered network
        /// </summary>
        /// <param name="dataWalker"></param>
        /// <param name="runsCount"></param>
        /// <param name="analyticsCollector"></param>
        /// <param name="onProgressChange"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<NNTrainingStats> TrainDenseNetwork(TwoOutputsDataWalker dataWalker, int runsCount,
            TrainingprogressAnalyticsCollector analyticsCollector, Action<float> onProgressChange)
        {
            if (dataWalker == null) throw new Exception("NeuralNetwork.Train dataWalker cant be null");
            if (runsCount <= 0) throw new Exception("NeuralNetwork.Train runsCount should be higher than one");

            NNTrainingStats tariningStats = new NNTrainingStats(runsCount);
            for (int run = 1; run <= runsCount; run++)
            {
                onProgressChange?.Invoke(run/(float)runsCount);
                double error = 0;
                // Разбиваем данные на батчи
                int walksIterations = 0;
                do
                {
                    List<KLine> expectedOutput;
                    List<KLine> input = dataWalker.Walk(out expectedOutput);
                    if (input != null)
                    {
                        if (expectedOutput.Count < 1)
                        {
                            throw new Exception("Data walker is ass. Returned 0 as expectedOutput");
                        }
                        if (input.Count < 2)
                        {
                            throw new Exception("Data walker is ass. Returned < 2 as input");
                        }
                        var inputArr = convertToTwoDimArr(input.Select(k => (double)(k.ClosePrice)).ToArray());
                        var outputArr = convertToTwoDimArr(expectedOutput.Select(k => (double)(k.ClosePrice)).ToArray());

                        // Тренируем модель на текущем батче
                        var loss = model.TrainOnBatch(np.array(inputArr), np.array(outputArr));
                        error += loss[0];
                        walksIterations++;
                        await Task.Delay(10); // Задержка для эмуляции асинхронной работы
                    }
                } while (dataWalker.isFinishedWalking());
                dataWalker.ResetDataWalker();
                tariningStats.RecordError(error/ walksIterations);
                tariningStats.GoNext();
            }

            return tariningStats;
            // Предсказания
            //var predictions = model.Predict(x_train);
            //Console.WriteLine("Predictions:");
            //Console.WriteLine(predictions.repr);
        }

        public async Task<NNTrainingStats> TrainLSTMNetwork(LSTMDataWalker dataWalker, int runsCount,int batchesCount,
            TrainingprogressAnalyticsCollector analyticsCollector, Action<float> onProgressChange)
        {
            if (dataWalker == null) throw new Exception("NeuralNetwork.Train dataWalker cant be null");
            if (runsCount <= 0) throw new Exception("NeuralNetwork.Train runsCount should be higher than one");

            NNTrainingStats tariningStats = new NNTrainingStats(runsCount);
            for (int run = 1; run <= runsCount; run++)
            {
                onProgressChange?.Invoke(run / (float)runsCount);
                double error = 0;
                // Разбиваем данные на батчи
                int walksIterations = 0;
                do
                {

                    List<double[,]> inputBatches = new List<double[,]>();
                    List<double[,]> outputBatches = new List<double[,]>();
                    for (int i = 0; i < batchesCount; i++)
                    {
                        if (dataWalker.isFinishedWalking()) break;
                        double[,] expectedOutput;
                        double[,] input = dataWalker.Walk(out expectedOutput);
                        inputBatches.Add(input);
                        outputBatches.Add(expectedOutput);
                    }
                    if (inputBatches.Count == 0 || outputBatches.Count == 0) break;
                    // Тренируем модель на текущем батче
                    var loss = model.TrainOnBatch(np.array(Normalize(ConvertListTo3DArray(inputBatches))),
                        np.array(Normalize(ConvertListTo3DArray(outputBatches))));
                    error += loss[0];
                    walksIterations++;
                    await Task.Delay(10); // Задержка для эмуляции асинхронной работы
                } while (dataWalker.isFinishedWalking());
                dataWalker.ResetDataWalker();
                tariningStats.RecordError(error / walksIterations);
                tariningStats.GoNext();
            }

            return tariningStats;
            // Предсказания
            //var predictions = model.Predict(x_train);
            //Console.WriteLine("Predictions:");
            //Console.WriteLine(predictions.repr);
        }

        double[,,] Normalize(double[,,] data)
        {
            double min = data.Cast<double>().Min(); // Минимум в массиве
            double max = data.Cast<double>().Max(); // Максимум в массиве
            double range = max - min;

            int dim0 = data.GetLength(0);
            int dim1 = data.GetLength(1);
            int dim2 = data.GetLength(2);

            double[,,] normalizedData = new double[dim0, dim1, dim2];

            for (int i = 0; i < dim0; i++)
            {
                for (int j = 0; j < dim1; j++)
                {
                    for (int k = 0; k < dim2; k++)
                    {
                        normalizedData[i, j, k] = 2 * (data[i, j, k] - min) / range - 1;
                    }
                }
            }

            return normalizedData;
        }

        public static double[,,] ConvertListTo3DArray(List<double[,]> list)
        {
            // Получаем размеры
            int depth = list.Count;  // Количество двумерных массивов в списке
            int rows = list[0].GetLength(0);  // Количество строк в каждом двумерном массиве
            int cols = list[0].GetLength(1);  // Количество столбцов в каждом двумерном массиве

            // Создаем новый трехмерный массив
            double[,,] result = new double[depth, rows, cols];

            // Копируем данные из списка в трехмерный массив
            for (int i = 0; i < depth; i++)
            {
                double[,] currentArray = list[i];
                for (int j = 0; j < rows; j++)
                {
                    for (int k = 0; k < cols; k++)
                    {
                        result[i, j, k] = currentArray[j, k];
                    }
                }
            }

            return result;
        }

        private double[,] convertToTwoDimArr(double[] inputArr)
        {
            double[,] transformedArray = new double[1, inputArr.Length];
            for (int i = 0; i < inputArr.Length; i++)
            {
                transformedArray[0, i] = inputArr[i];
            }
            return transformedArray;
        }
    }

    public class NNData
    {
        public int inputCount { get; set;}
        public int outputCount { get; set; }

        public NNData(BindingList<NNLayerConfig> config)
        {
            inputCount = config[0].neuronsCount;
            outputCount = config[config.Count - 1].neuronsCount;
        }
    }
}
