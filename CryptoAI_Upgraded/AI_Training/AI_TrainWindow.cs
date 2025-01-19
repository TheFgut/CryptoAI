using Binance.Net.Objects.Models.Futures;
using CryptoAI_Upgraded.AI_Training.NeuralNetworkCreating;
using CryptoAI_Upgraded.AI_Training.NeuralNetworks;
using CryptoAI_Upgraded.Datasets.DataWalkers;
using Keras.Layers;
using Keras.Models;
using Keras.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CryptoAI_Upgraded.AI_Training.NeuralNetworks.UI;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;

namespace CryptoAI_Upgraded.AI_Training
{
    public partial class AI_TrainWindow : Form
    {
        private List<LocalKlinesDataset> datasets;
        private Task? trainingTask;
        private CancellationTokenSource? trainingCnacellationToken;
        private NeuralNetworkCreatorWindow? networkCreaterForm;
        private NeuralNetwork? neuralNetwork;

        public AI_TrainWindow()
        {
            InitializeComponent();
            networkManagePanel1.onNetworkChanges += AssingModel;
            AssingModel(null);
            datasets = datasetsManagerPanel1.choosedLocalDatasets;
        }

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
                //stop logic
            }
        }

        private async Task Train(CancellationToken token)
        {
            Stopwatch timer = Stopwatch.StartNew();
            try
            {
                object referencedProgressInt = 0;
                LSTMDataWalker dataWalker = new LSTMDataWalker(datasets, neuralNetwork.inputsCount, neuralNetwork.outputCount, 60);
                NNTrainingStats trainingStats = await neuralNetwork.TrainLSTMNetwork(dataWalker, 1000, 16, null,
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
                    });
                DisplayDataOnChart(errorsChart, trainingStats.errorsLoss);
            }
            catch (Exception ex)
            {
                Invoke((Action)(() =>
                {
                    MessageBox.Show($"Exception during execution for training {ex.Message}\n {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }

            if (token.IsCancellationRequested)//cancellation of task
            {
                token.ThrowIfCancellationRequested();
            }
            else
            {
                trainingTask = null;
            }
            timer.Stop();
            Invoke((Action)(() =>
            {
                MessageBox.Show($"Training duration: {timer.ElapsedMilliseconds / 1000f} seconds", "Training finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TrainingProgressBar.Value = 0;
            }));
        }

        private void DisplayDataOnChart(Chart chart, double[] data)
        {
            chart.Series.Clear();

            // Создание и настройка ряда данных
            var series = new Series
            {
                ChartType = SeriesChartType.Line, // Тип графика: линия
                Color = System.Drawing.Color.Blue,
                BorderWidth = 2
            };
            chart.Series.Add(series);

            // Добавление точек в график
            for (int i = 0; i < data.Length; i++)
            {
                series.Points.AddXY(i, data[i]);
            }
        }

        private void StartLearningBut_Click(object sender, EventArgs e)
        {
            StartTraining();
            StopLearningBut.Enabled = true;
            StartLearningBut.Enabled = false;
        }

        private void StopLearningBut_Click(object sender, EventArgs e)
        {
            StopTraining();
            StopLearningBut.Enabled = false;
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
                StopLearningBut.Enabled = true;
            }
        }
    }
}
