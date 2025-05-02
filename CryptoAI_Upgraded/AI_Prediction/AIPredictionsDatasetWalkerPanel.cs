using CryptoAI_Upgraded.AI_Training.NeuralNetworks;
using CryptoAI_Upgraded.Datasets.DataWalkers;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace CryptoAI_Upgraded.AI_Prediction
{
    public partial class AIPredictionsDatasetWalkerPanel : UserControl
    {
        private LSTMDataWalker dataWalker;
        private List<LocalKlinesDataset> datasets;
        private NeuralNetwork? network;

        int WalkerNum = 0;
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
            dataWalker = new LSTMDataWalker(datasets, network.networkConfig);

            List<double>[]? result = WalkAt(0);
            if (result != null)
            {
                DrawBranchedChart(result[0], result[1], result[2]);
                ShowDataText(result[0], result[1], result[2]);
            }
        }

        public List<double>[]? WalkAt(int step)
        {
            int predictionsCount;
            if (!int.TryParse(predictionsCountBox.Text, out predictionsCount))
            {
                MessageBox.Show("Predictions count are in not correct format","Error");
                return null;
            }

            List<double> predictions = new List<double>();

            double[,] data = dataWalker.WalkAt(step, out var expected);
            List<double> networkInputData = Enumerable.Range(0, data.GetLength(0))
                     .Select(x => data[x, 1])
                     .ToList();
            double[,,] input = Helpers.ConvertArrTo3DArray(data);
            //double[,,] normalized = Helpers.Normalization.Normalize(out double min, out double max, input);
            int predictionSteps = 0;
            int predCounter = predictionsCount;
            while (predCounter > 0)
            {
                double[,,] dataWithMinMax = Helpers.ArrayValuesInjection.InjectValues(input, 0, 0);
                float[] predictionArr = network.Predict(dataWithMinMax);
                foreach (var pred in predictionArr)
                {
                    //double denormalized = Helpers.Normalization.Denormalize(pred, min, max);
                    predictions.Add(pred);
                    input = Helpers.ArrayValuesInjection.UpdateInput(input, pred);
                    predictionSteps++;
                }
                predCounter--;
            }

            //getting expected arr
            List<double> expectedList = new List<double>();
            for (int i = 0; i < predictionSteps; i++)
            {
                expectedList.Add(expected[0]);

                dataWalker.WalkAt(step, out expected);
                step++;
            }
            return new List<double>[]{networkInputData,
                expectedList, predictions };
        }

        private void ShowDataInChart(double[] expected, double[] predictions)
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

            chart1.Series.Add(series1);
            chart1.Series.Add(series2);

        }

        public void DrawBranchedChart(List<double> networkInput, List<double> expected, List<double> predictions)
        {
            int branchPoint = networkInput.Count - 1;
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add(new ChartArea());
            //adding origin point
            expected.Insert(0, networkInput[branchPoint]);
            predictions.Insert(0, networkInput[branchPoint]);
            // Основна лінія до розгалуження
            Series seriesMain = new Series("Network input")
            {
                ChartType = SeriesChartType.Line,
                Color = System.Drawing.Color.Blue,
                BorderWidth = 2
            };

            for (int i = 0; i <= branchPoint; i++)
            {
                seriesMain.Points.AddXY(i, networkInput[i]);
            }
            chart1.Series.Add(seriesMain);

            Series seriesBranch1 = new Series("Expected output")
            {
                ChartType = SeriesChartType.Line,
                Color = System.Drawing.Color.Green,
                BorderWidth = 2
            };

            for (int i = branchPoint; i < branchPoint + expected.Count; i++)
            {
                seriesBranch1.Points.AddXY(i, expected[i - branchPoint]);
            }
            chart1.Series.Add(seriesBranch1);

            Series seriesBranch2 = new Series("Predictions")
            {
                ChartType = SeriesChartType.Line,
                Color = System.Drawing.Color.Red,
                BorderWidth = 2
            };

            for (int i = branchPoint; i < branchPoint + predictions.Count; i++)
            {
                seriesBranch2.Points.AddXY(i, predictions[i - branchPoint]);
            }
            chart1.Series.Add(seriesBranch2);
        }

        public void ShowDataText(List<double> networkInput, List<double> expected, List<double> predictions)
        {
            StringBuilder detailsString = new StringBuilder();
            detailsString.Append("Network input:");
            for (int i = 0; i < networkInput.Count; i++)
            {
                detailsString.Append($" {Math.Round(networkInput[i], 3)}");
            }
            detailsString.Append("\nExpected:");
            for (int i = 0; i < expected.Count; i++)
            {
                detailsString.Append($" {Math.Round(expected[i], 3)}");
            }
            detailsString.Append("\nPredictions:");
            for (int i = 0; i < predictions.Count; i++)
            {
                detailsString.Append($" {Math.Round(predictions[i], 3)}");
            }
            details.Text = detailsString.ToString();
        }

        private void GoRightBut_Click(object sender, EventArgs e)
        {
            WalkerNum++;
            List<double>[]? result = WalkAt(WalkerNum);
            if (result != null)
            {
                DrawBranchedChart(result[0], result[1], result[2]);
                ShowDataText(result[0], result[1], result[2]);
            }
        }

        private void GoLeftBut_Click(object sender, EventArgs e)
        {
            if (WalkerNum <= 0) return;
            WalkerNum--;
            List<double>[]? result = WalkAt(WalkerNum);
            if(result != null)
            {
                DrawBranchedChart(result[0], result[1], result[2]);
                ShowDataText(result[0], result[1], result[2]);
            }
        }
    }
}
