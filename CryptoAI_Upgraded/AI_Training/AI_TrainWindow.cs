using CryptoAI_Upgraded.AI_Training.NeuralNetworks;
using CryptoAI_Upgraded.Datasets.DataWalkers;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using CryptoAI_Upgraded.DataSaving;
using CryptoAI_Upgraded.DataLocalChoosing;

namespace CryptoAI_Upgraded.AI_Training
{
    public partial class AI_TrainWindow : Form
    {
        private List<LocalKlinesDataset> learningDatasets;
        private List<LocalKlinesDataset> testingDatasets;
        private Task? trainingTask;
        private CancellationTokenSource? trainingCnacellationToken;
        private NeuralNetwork? neuralNetwork;
        private TrainingConfigData trainingConfig;
        LocalLoaderAndSaverBSON<TrainingConfigData> trainConfigLoader;
        private AI_SetupWindow? setupWindow;

        public AI_TrainWindow()
        {
            InitializeComponent();
            networkManagePanel1.onNetworkChanges += AssingModel;
            AssingModel(null);
            learningDatasets = trainingDatasetsManager.choosedLocalDatasets;
            trainingDatasetsManager.title = "Training datasets";
            testingDatasets = testingDatasetsManager.choosedLocalDatasets;
            testingDatasetsManager.title = "Testing datasets";

            trainConfigLoader = new LocalLoaderAndSaverBSON<TrainingConfigData>(DataPaths.appConfigurationPath, "trainingSettings");
            TrainingConfigData? loadedConfig = trainConfigLoader.Load();
            if (loadedConfig == null) loadedConfig = TrainingConfigData.Default;
            trainingConfig = loadedConfig;
            InitConfiguration();
        }

        #region configuration
        public void InitConfiguration()
        {
            runsCountBox.Text = trainingConfig.runsCount.ToString();
            SavableConfig testDatasetsConfig = new SavableConfig(DataPaths.appConfigurationPath, "testDatasetsConfig");
            testingDatasetsManager.InitFromConfig(testDatasetsConfig);
            SavableConfig trainingDatasetsConfig = new SavableConfig(DataPaths.appConfigurationPath, "trainingDatasetsConfig");
            trainingDatasetsManager.InitFromConfig(trainingDatasetsConfig);
        }
        #endregion

        public void StartTraining()
        {
            if (trainingTask != null) throw new Exception("Cant start training twice");
            trainingCnacellationToken = new CancellationTokenSource();
            trainingTask = Train(trainingCnacellationToken.Token);
        }

        public void StopTraining()
        {
            if (trainingTask != null)
            {
                if (trainingCnacellationToken == null) throw new Exception("AI_TrainWindow.StopTraining failed. trainingCnacellationToken is null");
                trainingCnacellationToken.Cancel();
                trainingCnacellationToken = null;
                trainingTask = null;
            }
        }

        private async Task Train(CancellationToken token)
        {
            Stopwatch timer = Stopwatch.StartNew();
            NNTrainingStats analyticsCollector = new NNTrainingStats(trainingConfig.runsCount,
                learningDatasets.Select(d => new DatasetID(d)).ToList());
            try
            {
                object referencedProgressInt = 0;
                LSTMDataWalker dataWalker = new LSTMDataWalker(learningDatasets, neuralNetwork.networkConfig);
                await neuralNetwork.TrainLSTMNetwork(dataWalker, 1, analyticsCollector,
                    (progressValue) =>
                    {
                        int progressValueInt = (int)(progressValue * 100);
                        if ((int)referencedProgressInt == progressValueInt) return;
                        referencedProgressInt = progressValueInt;
                        Invoke(
                            () =>
                        {

                            TrainingProgressBar.Value = progressValueInt;
                            int eta = (int)((timer.ElapsedMilliseconds / 1000 / progressValue) * (1 - progressValue));
                            TrainingETA.Text = $"ETA: {Helpers.FormatDuration(eta)}";
                        });
                    }, trainingConfig, token);

                errorsChart.Series.Clear();
                DisplayDataOnChart(errorsChart, analyticsCollector.trainingRunsData.Select(r => r.averageError)
                    .ToArray(), analyticsCollector.runsPassed, Color.Blue);
                DisplayDataOnChart(errorsChart, analyticsCollector.trainingRunsData.Select(r => r.maxError)
                    .ToArray(), analyticsCollector.runsPassed, Color.Red);
                DisplayDataOnChart(errorsChart, analyticsCollector.trainingRunsData.Select(r => r.minError)
                    .ToArray(), analyticsCollector.runsPassed, Color.Green);

                if (testingDatasets.Count > 0)
                {
                    await Test(token, analyticsCollector);
                }
            }
            catch (Exception ex)
            {
                Invoke((Action)(() =>
                {
                    MessageBox.Show($"Exception during execution for training {ex.Message}\n {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }

            try
            {
                trainingResultPanel.Rtf = analyticsCollector.ToRichTextString();
                trainingResultPanel.SelectionStart = trainingResultPanel.TextLength;
                trainingResultPanel.ScrollToCaret();
            }
            catch (Exception ex)
            {
                Invoke((Action)(() =>
                {
                    MessageBox.Show($"Training duration: {timer.ElapsedMilliseconds / 1000f} seconds",
                        "Display training information error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }

            trainingTask = null;
            timer.Stop();
            Invoke((Action)(() =>
            {
                MessageBox.Show($"Training duration: {timer.ElapsedMilliseconds / 1000f} seconds", "Training finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TrainingProgressBar.Value = 0;
                ActivateAllButs();
            }));
        }

        private async Task Test(CancellationToken token, NNTrainingStats analyticsCollector)
        {
            Stopwatch timer = Stopwatch.StartNew();
            try
            {
                object referencedProgressInt = 0;
                LSTMDataWalker dataWalker = new LSTMDataWalker(testingDatasets, neuralNetwork.networkConfig);
                await neuralNetwork.TestLSTMNetwork(dataWalker, analyticsCollector,
                    (progressValue) =>
                    {
                        int progressValueInt = (int)(progressValue * 100);
                        if ((int)referencedProgressInt == progressValueInt) return;
                        referencedProgressInt = progressValueInt;
                        Invoke(
                            () =>
                            {
                                TrainingProgressBar.Value = progressValueInt;
                            });
                    }, token);
            }
            catch (Exception ex)
            {
                Invoke((Action)(() =>
                {
                    MessageBox.Show($"Exception during execution of testing {ex.Message}\n {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
            trainingTask = null;
            timer.Stop();
        }
        private void DisplayDataOnChart(Chart chart, double[] data,int count, Color color)
        {
            // Создание и настройка ряда данных
            var series = new Series
            {
                ChartType = SeriesChartType.Line, // Тип графика: линия
                Color = System.Drawing.Color.Blue,
                BorderWidth = 2
            };
            chart.Series.Add(series);

            // Добавление точек в график
            for (int i = 0; i < count; i++)
            {
                series.Points.AddXY(i, data[i]);
            }
        }

        private void StartLearningBut_Click(object sender, EventArgs e)
        {
            StartTraining();
            StopLearningBut.Enabled = true;
            StartLearningBut.Enabled = false;
            runsCountBox.Enabled = false;
        }

        private void StopLearningBut_Click(object sender, EventArgs e)
        {
            StopTraining();
        }

        private void AssingModel(NeuralNetwork? model)
        {
            this.neuralNetwork = model;
            if (model == null)
            {
                StartLearningBut.Enabled = false;
                StopLearningBut.Enabled = false;
            }
            else
            {
                StartLearningBut.Enabled = true;
            }
        }

        private void runsCountBox_Validated(object sender, EventArgs e)
        {
            int result;
            if (!int.TryParse(runsCountBox.Text, out result))
            {
                MessageBox.Show($"Your input: \"{runsCountBox.Text}\" is incorrect. Please write a number", "IputError",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                runsCountBox.Text = trainingConfig.runsCount.ToString();
                return;
            }
            trainingConfig.runsCount = result;
            trainConfigLoader.Save(trainingConfig);
        }

        private void ActivateAllButs()
        {
            StartLearningBut.Enabled = true;
            StopLearningBut.Enabled = false;
            runsCountBox.Enabled = true;
        }

        private void learningSettings_Click(object sender, EventArgs e)
        {
            if (setupWindow == null)
            {
                setupWindow = new AI_SetupWindow(trainingConfig);
                setupWindow.FormClosed += (sender, args) => setupWindow = null;
                setupWindow.Show();
            }
        }

        private void AI_TrainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            trainConfigLoader.Save(trainingConfig);
        }
    }
}
