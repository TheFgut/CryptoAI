using CryptoAI_Upgraded.AI_Training.NeuralNetworks;
using CryptoAI_Upgraded.AI_Training.NeuralNetworks.UI;
using CryptoAI_Upgraded.Datasets;
using CryptoAI_Upgraded.Datasets.DataWalkers;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoAI_Upgraded.DatasetsAnalasys
{
    public partial class DatasetCourseChangeAnalysis : Form
    {
        private const float spotTradeBuyComission = 0.001f;//0.1 percent
        private const float spotTradeSellComission = 0.001f;//0.1 percent

        private Task analizeTask;
        private List<LocalKlinesDataset> datasets;
        private NeuralNetwork? neuralNetwork;
        public DatasetCourseChangeAnalysis()
        {
            InitializeComponent();

            networkManagePanel1.onNetworkChanges += AssingModel;
            datasets = datasetsManagerPanel1.choosedLocalDatasets;
            AssingModel(null);
        }


        private void AnalyzeBut_Click(object sender, EventArgs e)
        {
            if (datasets.Count == 0)
            {
                MessageBox.Show($"Datasets count should be more than one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            analizeTask = Analize(neuralNetwork.outputCount);
        }

        private async Task Analize(int predictionsLength)
        {
            AnaliseResultDisp.Text = "";
            Stopwatch timer = Stopwatch.StartNew();
            //datas
            LinkedList<double> errors = new LinkedList<double>();
            LinkedList<double> guessedDir = new LinkedList<double>();//1-guessed, 0-not guessed
            //metrics
            int analizeStepsAmount = 0;
            int skippedSteps = 0;
            double averageError = 0;
            double guessedDirPercent = 0;

            try
            {
                object referencedProgressInt = 0;
                LSTMDataWalker dataWalker = new LSTMDataWalker(datasets, neuralNetwork.networkConfig);

                do
                {
                    KLine pos = dataWalker.position;
                    List<double[]>? predictionResult = Walk(dataWalker, predictionsLength);

                    if (predictionResult == null) break;
                    analizeStepsAmount++;
                    AnaliseResultDisp.Text += $"Step: {predictionResult[0][0]} {predictionResult[1][0]}\n";
                    //getting error metrics and datas
                    double error = 0;
                    for (int i = 0; i < predictionResult[1].Length;i++)
                    {
                        if (predictionResult[1][i] == double.NaN)
                        {
                            skippedSteps++;
                            continue;
                        }
                        error += Math.Abs(predictionResult[0][i] - predictionResult[1][i]);
                    }
                    error /= predictionResult[0].Length;
                    errors.AddLast(error);
                    //getting guessed dir
                    double finalPred = 0;
                    double finalEx = 0;
                    //for (int i = 0; i < predictionResult[0].Length; i++)
                    //{
                    //    finalPred += predictionResult[0][i];
                    //    finalEx += predictionResult[1][i];
                    //}
                    finalPred = predictionResult[0][predictionResult[0].Length - 1] - (double)pos.OpenPrice;
                    finalEx = predictionResult[1][predictionResult[0].Length - 1] - (double)pos.OpenPrice;
                    guessedDir.AddLast(Math.Sign(finalPred) == Math.Sign(finalEx) ? 1 : 0);
                } while (true);
                await Task.Yield();
                averageError = errors.Average();
                guessedDirPercent = guessedDir.Average() * 100;
            }
            catch (Exception ex)
            {
                Invoke((Action)(() =>
                {
                    MessageBox.Show($"Exception during execution of Analize {ex.Message}\n {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }


            timer.Stop();
            Invoke((Action)(() =>
            {
                //displaying
                AnaliseResultDisp.Text += $"Analized steps: {analizeStepsAmount}\n";
                AnaliseResultDisp.Text += $"Average error: {averageError}\n";
                AnaliseResultDisp.Text += $"Guessed dir percent: {guessedDirPercent}%\n";
                MessageBox.Show($"Analize duration: {timer.ElapsedMilliseconds / 1000f} seconds", "Analize finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }));
        }

        public List<double[]>? Walk(LSTMDataWalker dataWalker, int predictionsLength)
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
            List<double> expectedList = new List<double>();
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


        private void AssingModel(NeuralNetwork? model)
        {
            this.neuralNetwork = model;
            if (model == null)
            {
                AnalyzeBut.Enabled = false;
            }
            else
            {
                AnalyzeBut.Enabled = true;
            }
        }
    }
}