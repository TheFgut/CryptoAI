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
            dataWalker = new LSTMDataWalker(datasets, network.inputsCount, network.outputCount, network.timeFragments);
            List<double[]> result = WalkAt(0, predictionsCount);
            ShwoDataInChart(result[0], result[1]);
        }

        public List<double[]> WalkAt(int step, int predictionsCount)
        {
            List<double> predictions = new List<double>();

            double[,] data = dataWalker.WalkAt(step, out var expected);
            double[,,] input = Helpers.ConvertArrTo3DArray(data);
            double[,,] expectedOutput = Helpers.ConvertArrTo3DArray(expected);
            List<double[,,]> normalized = Helpers.Normalization.Normalize(input,
                expectedOutput);

            double collectorPred = 0;
            int predCounter = predictionsCount;
            while (predCounter > 0)
            {
                double prediction = network.Predict(normalized[0])[0];
                collectorPred += prediction;
                predictions.Add(collectorPred);
                input = UpdateInput(input, prediction);
                predCounter--;
            }

            //getting expected arr
            double collectorEx = 0;
            List<double> expectedList = new List<double>();
            for (int i = 0; i < predictionsCount; i++)
            {
                //collectorEx += expectedOutput[0, i, 0];
                collectorEx += normalized[1][0, i, 0];
                expectedList.Add(collectorEx);
            }
            return new List<double[]>() { expectedList.ToArray(),
                Helpers.Normalization.Normalize(predictions).ToArray() };
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
            double[,,] newInput = new double[1, timeSteps, 1];

            // Сдвигаем данные
            for (int t = 1; t < timeSteps; t++)
            {
                newInput[0, t - 1, 0] = input[0, t, 0];
            }

            // Добавляем новое значение
            newInput[0, timeSteps - 1, 0] = newValue;

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
