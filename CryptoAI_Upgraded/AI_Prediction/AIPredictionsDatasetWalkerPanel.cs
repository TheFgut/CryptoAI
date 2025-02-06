using CryptoAI_Upgraded.AI_Training.NeuralNetworks;
using CryptoAI_Upgraded.Datasets.DataWalkers;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CryptoAI_Upgraded.AI_Prediction
{
    public partial class AIPredictionsDatasetWalkerPanel : UserControl
    {
        private LSTMDataWalker dataWalker;
        private List<LocalKlinesDataset> datasets;
        private NeuralNetwork? network;

        int WalkerNum = 0;
        int predictionsCount = 15;
        public AIPredictionsDatasetWalkerPanel()
        {
            datasets = new List<LocalKlinesDataset>();
            InitializeComponent();
        }

        public void DataSetsChanged(List<LocalKlinesDataset> datasets)
        {
            this.datasets = datasets;
            if (network != null && datasets.Count >= 1)
            {
                Init(network, datasets);
            }
        }

        public void NetworkChanged(NeuralNetwork? network)
        {
            this.network = network;
            if (network != null && datasets.Count >= 1)
            {
                Init(network, datasets);
            }
        }

        private void Init(NeuralNetwork network, List<LocalKlinesDataset> datasets)
        {
            dataWalker = new LSTMDataWalker(datasets, network.inputCount, network.outputCount, network.timeFragments);
            List<double[]> result = WalkAt(0, predictionsCount);
            ShwoDataInChart(result[0], result[1]);
        }

        public List<double[]> WalkAt(int step, int predictionsCount)
        {
            List<double> predictions = new List<double>();

            double[,] data = dataWalker.WalkAt(step, out var expected);
            double[,,] input = Helpers.ConvertArrTo3DArray(data);
            double[,,] normalized = Helpers.Normalization.Normalize(out double min, out double max, input);
            double collectorPred = 0;
            int predCounter = predictionsCount;
            while (predCounter > 0)
            {
                double[,,] dataWithMinMax = Helpers.ArrayValuesInjection.InjectValues(normalized, min, max);
                double[] predictionArr = network.Predict(dataWithMinMax);
                double prediction = predictionArr[0];

                double denormalized = Helpers.Normalization.Denormalize(prediction,min,max);
                collectorPred += denormalized;
                predictions.Add(collectorPred);

                normalized = UpdateInput(normalized, prediction);
                predCounter--;
            }

            //getting expected arr
            double collectorEx = 0;
            List<double> expectedList = new List<double>();
            for (int i = 0; i < predictionsCount; i++)
            {
                //collectorEx += expectedOutput[0, i, 0];
                collectorEx += expected[0];
                expectedList.Add(collectorEx);

                dataWalker.WalkAt(step, out expected);
                step++;
            }
            return new List<double[]>() { expectedList.ToArray(),
                predictions.ToArray() };
        }

        private void ShwoDataInChart(double[] expected, double[] predictions)
        {
            // Основной график
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // Добавление области для основного графика
            var mainArea = new ChartArea("MainArea");
            chart1.ChartAreas.Add(mainArea);

            // Настройка осей основного графика
            mainArea.AxisX.Title = "Время";
            mainArea.AxisY.Title = "Цена";

            // Ряд для основного графика
            Series series1 = new Series("Line 1")
            {
                ChartType = SeriesChartType.Line,
                Name = "Expected",
                Color = System.Drawing.Color.Blue,
                BorderWidth = 2
            };

            // Создаем вторую линию
            Series series2 = new Series("Line 2")
            {
                ChartType = SeriesChartType.Line,
                Name = "Prediction",
                Color = System.Drawing.Color.Red,
                BorderWidth = 2
            };

            details.Text = "Expected:";
            // Заполнение данных основного графика
            for (int i = 0; i < expected.Length; i++)
            {
                series1.Points.AddXY(i, expected[i]);
                details.Text += $" {expected[i]}";
            }
            details.Text += "\n\nPredictions:";
            // Заполнение данных мини-графика
            for (int i = 0; i < predictions.Length; i++)
            {
                series2.Points.AddXY(i, predictions[i]);
                details.Text += $" {predictions[i]}";
            }

            chart1.Series.Add(series1);
            chart1.Series.Add(series2);

        }

        #region helpers
        double[,,] UpdateInput(double[,,] input, double newValue)
        {
            int timeSteps = input.GetLength(1);
            int inputCounts = input.GetLength(2);
            double[,,] newInput = new double[1, timeSteps, inputCounts];

            // Сдвигаем данные
            for (int t = 1; t < timeSteps; t++)
            {
                for (int n = 1; n < inputCounts; n++)
                {
                    newInput[0, t - 1, n - 1] = input[0, t - 1, n];
                }
                newInput[0, t - 1, inputCounts - 1] = input[0, t, inputCounts - 1];
            }

            // Добавляем новое значение
            newInput[0, timeSteps - 1, inputCounts - 1] = newValue;

            return newInput;
        }
        #endregion

        private void GoRightBut_Click(object sender, EventArgs e)
        {
            WalkerNum++;
            List<double[]> result = WalkAt(WalkerNum, predictionsCount);
            ShwoDataInChart(result[0], result[1]);
        }

        private void GoLeftBut_Click(object sender, EventArgs e)
        {
            if (WalkerNum <= 0) return;
            WalkerNum--;
            List<double[]> result = WalkAt(WalkerNum, predictionsCount);
            ShwoDataInChart(result[0], result[1]);
        }
    }
}
