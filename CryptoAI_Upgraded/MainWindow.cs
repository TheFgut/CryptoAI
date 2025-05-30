using Binance.Net.Interfaces;
using CryptoAI_Upgraded.AI_Prediction;
using CryptoAI_Upgraded.AI_Training;
using CryptoAI_Upgraded.AI_Training.NeuralNetworks.UI.Upgraded_net_loader;
using CryptoAI_Upgraded.DataLocalChoosing;
using CryptoAI_Upgraded.DataSaving;
using CryptoAI_Upgraded.Datasets;
using CryptoAI_Upgraded.Datasets.NromalizationAndConvertion;
using CryptoAI_Upgraded.DatasetsAnalasys;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using CryptoAI_Upgraded.RealtimeTrading;
using Keras.Layers;
using Keras.Models;
using Numpy;

namespace CryptoAI_Upgraded
{
    public partial class MainWindow : Form
    {
        private List<LocalKlinesDataset> choosedLocalDatasets;
        private LoadingKlinesForms? loadingKlinesForms;
        private LoadLocalDatasetsForm? loadingLocalForm;
        private DatasetGraphicDisplayForm? datasetsDisplay;
        private DatasetCourseChangeAnalysis? datasetsCourseAnalysis;
        private AI_TrainWindow? aiTrainWindow;
        private AIPredictorForm? aiPredictor;
        private DatasetConvertorAndNormalizerWindow? datasetNormalizerWindow;
        private RealtimeTradeWindow? realtimeTradeWindow;
        private ModernNetLoader? modernNetLoader;
        public MainWindow()
        {
            choosedLocalDatasets = new List<LocalKlinesDataset>();
            InitializeComponent();
            Keras.Keras.DisablePySysConsoleLog = true;
            //trainAI_But.Enabled = false;
        }

        private void OpenLoadDataWindowBut_Click(object sender, EventArgs e)
        {
            if (loadingKlinesForms == null)
            {
                loadingKlinesForms = new LoadingKlinesForms();
                loadingKlinesForms.FormClosed += (sender, args) => loadingKlinesForms = null;
                loadingKlinesForms.Show();
            }
        }

        private void LoadLocalDatasetsBut_Click(object sender, EventArgs e)
        {
            if (loadingLocalForm == null)
            {
                loadingLocalForm = new LoadLocalDatasetsForm(choosedLocalDatasets);
                loadingLocalForm.FormClosed += (sender, args) => loadingLocalForm = null;
                loadingLocalForm.Show();
            }
        }

        private void DisplayGraphics_Click(object sender, EventArgs e)
        {
            if (datasetsDisplay == null)
            {
                datasetsDisplay = new DatasetGraphicDisplayForm(choosedLocalDatasets);
                datasetsDisplay.FormClosed += (sender, args) => datasetsDisplay = null;
                datasetsDisplay.Show();
            }
        }

        private void displayDataBut_Click(object sender, EventArgs e)
        {

        }

        private void AnalyzeCourseChangeBut_Click(object sender, EventArgs e)
        {
            if (datasetsCourseAnalysis == null)
            {
                datasetsCourseAnalysis = new DatasetCourseChangeAnalysis();
                datasetsCourseAnalysis.FormClosed += (sender, args) => datasetsCourseAnalysis = null;
                datasetsCourseAnalysis.Show();
            }
        }

        private void trainAI_But_Click(object sender, EventArgs e)
        {
            if (aiTrainWindow == null)
            {
                aiTrainWindow = new AI_TrainWindow();
                aiTrainWindow.FormClosed += (sender, args) => aiTrainWindow = null;
                aiTrainWindow.Show();
            }
        }

        public async Task WarmUpKerasAsync()
        {
            await Task.Run(() =>
            {
                // ������� ������ ��� ��������
                var dummyModel = new Sequential();
                dummyModel.Add(new Dense(10, activation: "relu", input_shape: new Keras.Shape(10)));

            });
        }

        private void ActivateTrainButtons()
        {
            trainAI_But.Enabled = true;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //await WarmUpKerasAsync();
        }

        private void AIPredictorBut_Click(object sender, EventArgs e)
        {
            if (aiPredictor == null)
            {
                aiPredictor = new AIPredictorForm();
                aiPredictor.FormClosed += (sender, args) => aiPredictor = null;
                aiPredictor.Show();
            }
        }

        private void NormalizeDatasetBut_Click(object sender, EventArgs e)
        {
            if (datasetNormalizerWindow == null)
            {
                datasetNormalizerWindow = new DatasetConvertorAndNormalizerWindow();
                datasetNormalizerWindow.FormClosed += (sender, args) => datasetNormalizerWindow = null;
                datasetNormalizerWindow.Show();
            }
        }

        private void RealtimeTradingWindow_Click(object sender, EventArgs e)
        {
            if (realtimeTradeWindow == null)
            {
                realtimeTradeWindow = new RealtimeTradeWindow();
                realtimeTradeWindow.FormClosed += (sender, args) => realtimeTradeWindow = null;
                realtimeTradeWindow.Show();
            }
        }

        private void CloudServiceBut_Click(object sender, EventArgs e)
        {
            if (modernNetLoader == null)
            {
                modernNetLoader = new ModernNetLoader();
                modernNetLoader.FormClosed += (sender, args) => modernNetLoader = null;
                modernNetLoader.Show();
            }
        }
    }
}
