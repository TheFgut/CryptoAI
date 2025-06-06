﻿using CryptoAI_Upgraded.AI_Training.NeuralNetworks;
using CryptoAI_Upgraded.Datasets.DataWalkers;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using CryptoAI_Upgraded.DataSaving;
using CryptoAI_Upgraded.DataLocalChoosing;
using CryptoAI_Upgraded.DataAnalasys;
using Python.Runtime;
using Keras.Optimizers;

namespace CryptoAI_Upgraded.AI_Training
{
    public partial class AI_TrainWindow : Form
    {
        private List<LocalKlinesDataset> trainingDatasets;
        private List<LocalKlinesDataset> testingDatasets;
        private Task? trainingTask;
        private CancellationTokenSource? trainingCnacellationToken;
        private NeuralNetwork? neuralNetwork;
        private TrainingConfigData trainingConfig;
        LocalLoaderAndSaverBSON<TrainingConfigData> trainConfigLoader;
        private AI_TrainSetupWindow? setupWindow;

        public AI_TrainWindow()
        {
            InitializeComponent();
            networkManagePanel1.onNetworkChanges += AssingModel;
            LoadBestNetworkBut.Enabled = false;
            AssingModel(null);
            trainingDatasets = trainingDatasetsManager.choosedLocalDatasets;
            trainingDatasetsManager.title = "Training datasets";
            testingDatasets = testingDatasetsManager.choosedLocalDatasets;
            testingDatasetsManager.title = "Testing datasets";
            TrainingSpeedTextBox.Enabled = false;

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
            batchesCountTextBox.Text = trainingConfig.batchesCount.ToString();
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
                trainingDatasets.Select(d => new DatasetID(d)).ToList());
            NetworkAccAnalize testAnalize = null;
            try
            {

                object referencedProgressInt = 0;
                LSTMDataWalker dataWalker = new LSTMDataWalker(trainingDatasets, neuralNetwork.networkConfig);
                LSTMDataWalker? testDataWalker = testingDatasets.Count == 0 ? null :
                    new LSTMDataWalker(testingDatasets, neuralNetwork.networkConfig);

                await neuralNetwork.TrainLSTMNetwork_Old(dataWalker, trainingConfig.batchesCount, analyticsCollector,
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
                    }, trainingConfig, token, testDataWalker);

                if (testingDatasets.Count > 0)
                {
                    NerworkAccuraccyAnalizer testAnalizer = new NerworkAccuraccyAnalizer();
                    testAnalize = await testAnalizer.Analize(neuralNetwork, testingDatasets);
                    if (testAnalize.success)
                    {
                        Helpers.DataPlotting.PlotMultipleSeries(lestPredictionsChart, new string[] { "test_predict", "test_real" },
                            testAnalize.predict.ToArray(), testAnalize.real.ToArray());
                    }
                    NerworkAccuraccyAnalizer trainAnalizer = new NerworkAccuraccyAnalizer();
                    NetworkAccAnalize trainAnalize = await trainAnalizer.Analize(neuralNetwork, trainingDatasets);
                    if (trainAnalize.success)
                    {
                        Helpers.DataPlotting.PlotMultipleSeries(trainPredictionsChart, new string[] { "train_predict", "train_real" },
                            trainAnalize.predict.ToArray(), trainAnalize.real.ToArray());
                    }
                }


                lossesChart.Series.Clear();
                Helpers.DataPlotting.DisplayDataOnChart(lossesChart, analyticsCollector.trainingRunsData.Select(r => r.averageError)
                    .ToArray(), analyticsCollector.runsPassed, "loss awerage error", Color.Yellow);
                Helpers.DataPlotting.DisplayDataOnChart(lossesChart, analyticsCollector.trainingRunsData.Select(r => r.maxError)
                    .ToArray(), analyticsCollector.runsPassed, "loss max error", Color.Red);
                Helpers.DataPlotting.DisplayDataOnChart(lossesChart, analyticsCollector.trainingRunsData.Select(r => r.minError)
                    .ToArray(), analyticsCollector.runsPassed, "loss min error", Color.Green);

                TestErrorsChart.Series.Clear();
                Helpers.DataPlotting.DisplayDataOnChart(TestErrorsChart, analyticsCollector.trainingRunsData.Select(r => r.avarageTestError)
                    .ToArray(), analyticsCollector.runsPassed, "test awerage error", Color.Green);
                LoadBestNetworkBut.Enabled = true;
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
                if (testAnalize != null && testAnalize.success) trainingResultPanel.Rtf += $"{testAnalize.guessedDirPercent}";
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
                MessageBox.Show($"Training duration: {timer.ElapsedMilliseconds / 1000f} seconds",
                    "Training finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TrainingProgressBar.Value = 0;
                ActivateAllButs();
            }));
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
                TrainingSpeedTextBox.Enabled = false;
            }
            else
            {
                StartLearningBut.Enabled = true;
                TrainingSpeedTextBox.Enabled = true;
                TrainingSpeedTextBox.Text = neuralNetwork.training_speed.ToString();

            }
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
                setupWindow = new AI_TrainSetupWindow(trainingConfig);
                setupWindow.FormClosed += (sender, args) => setupWindow = null;
                setupWindow.Show();
            }
        }

        private void AI_TrainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            trainConfigLoader.Save(trainingConfig);
        }

        private void LoadBestNetworkBut_Click(object sender, EventArgs e)
        {
            NeuralNetwork? network = new NeuralNetwork($"{DataPaths.networksPath}\\temporarySavedNetwork");
            networkManagePanel1.AssignNetwork(network);

            MessageBox.Show($"Stats:\n{network.trainingStatistics.lastTrain.ToString()}", "Best network assigned",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #region validation

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

        private void batchesCountTextBox_Validated(object sender, EventArgs e)
        {
            int result;
            if (!int.TryParse(batchesCountTextBox.Text, out result))
            {
                MessageBox.Show($"Your input: \"{batchesCountTextBox.Text}\" is incorrect. Please write a number", "IputError",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                batchesCountTextBox.Text = trainingConfig.batchesCount.ToString();
                return;
            }
            if (result <= 0)
            {
                MessageBox.Show($"Your input: \"{batchesCountTextBox.Text}\" is incorrect. Batches count can`t be less than 1", "IputError",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                batchesCountTextBox.Text = trainingConfig.batchesCount.ToString();
                return;
            }
            trainingConfig.batchesCount = result;
            trainConfigLoader.Save(trainingConfig);
        }
        #endregion

        private void TrainingSpeedTextBox_Validated(object sender, EventArgs e)
        {
            double result;
            if (!double.TryParse(TrainingSpeedTextBox.Text, out result))
            {
                MessageBox.Show($"Your input: \"{TrainingSpeedTextBox.Text}\" is incorrect. Please write a number", "IputError",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TrainingSpeedTextBox.Text = neuralNetwork.training_speed.ToString();
                return;
            }
            neuralNetwork.training_speed = result;
        }
    }
}
