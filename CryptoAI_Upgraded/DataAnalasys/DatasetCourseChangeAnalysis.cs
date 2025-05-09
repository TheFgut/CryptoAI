using CryptoAI_Upgraded.AI_Training.NeuralNetworks;
using CryptoAI_Upgraded.Datasets;
using CryptoAI_Upgraded.Datasets.DataWalkers;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using static CryptoAI_Upgraded.Helpers;
using CryptoAI_Upgraded.DataAnalasys;

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
            List<double> real = new List<double>();
            List<double> predict = new List<double>();
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
                    List<double[]>? predictionResult = Walk(dataWalker, predictionsLength);
                    KLine pos = dataWalker.position;

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
                    double lastReal = predictionResult[0][predictionResult[0].Length - 1];
                    double lastPred = predictionResult[1][predictionResult[0].Length - 1];
                    real.Add(lastReal);
                    predict.Add(lastPred);
                    finalPred = lastReal - (double)pos.OpenPrice;
                    finalEx = lastPred - (double)pos.OpenPrice;
                    guessedDir.AddLast(getDirection(finalPred) == getDirection(finalEx) ? 1 : 0);
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
                double[] sma = MovingAverage.Simple(real.ToArray(), 3);
                PlotMultipleSeries(new[] { "real" , "predictions", "sma"}, real.ToArray(), predict.ToArray(), sma);
                new MultiSeriesChartForm(real.ToArray(), predict.ToArray(), sma).Show();
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

        public void PlotMultipleSeries(string[] labels, params double[][] dataArrays)
        {
            if (labels.Length != dataArrays.Length)
                throw new ArgumentException("Количество меток должно совпадать с количеством массивов данных.");

            // Очистим старые серии и настройки
            PredictionsGraphic.Series.Clear();
            PredictionsGraphic.ChartAreas.Clear();
            PredictionsGraphic.Legends.Clear();

            // Создаем область для построения
            var area = new ChartArea("MainArea");
            PredictionsGraphic.ChartAreas.Add(area);

            // Легенда
            var legend = new Legend("Legend");
            PredictionsGraphic.Legends.Add(legend);

            // Цвета можно задать заранее или сгенерировать случайно
            Color[] palette = new[]
            {
            Color.Blue, Color.Red, Color.Green, Color.Orange, Color.Purple,
            Color.Brown, Color.Magenta, Color.Cyan, Color.Lime, Color.Sienna
            };

            for (int i = 0; i < dataArrays.Length; i++)
            {
                var series = new Series(labels[i])
                {
                    ChartType = SeriesChartType.Line,
                    ChartArea = "MainArea",
                    Legend = "Legend",
                    BorderWidth = 2,
                    Color = palette[i % palette.Length]
                };

                // Добавляем точки в серию
                var data = dataArrays[i];
                for (int x = 0; x < data.Length; x++)
                {
                    series.Points.AddXY(x, data[x]);
                }

                PredictionsGraphic.Series.Add(series);
            }

            // Настройки осей (опционально)
            area.AxisX.Title = "Індекс";
            area.AxisY.Title = "Значення";
            area.AxisY.Minimum = dataArrays.Min(arr => arr.Min());
            area.AxisY.Maximum = dataArrays.Max(arr => arr.Max());
            area.AxisY.LabelStyle.Format = "F2";
            area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
        }

        private int getDirection(double dir)
        {
            double neutralCoef = 0.003;
            return Math.Abs(dir) < neutralCoef ? 0 : Math.Sign(dir);
        }
    }
}