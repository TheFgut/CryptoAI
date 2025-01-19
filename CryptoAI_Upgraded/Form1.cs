using Binance.Net.Interfaces;
using CryptoAI_Upgraded.AI_Training;
using CryptoAI_Upgraded.DataLocalChoosing;
using CryptoAI_Upgraded.DataSaving;
using CryptoAI_Upgraded.Datasets;
using CryptoAI_Upgraded.DatasetsAnalasys;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using Keras.Layers;
using Keras.Models;
using Numpy;

namespace CryptoAI_Upgraded
{
    public partial class Form1 : Form
    {
        private List<LocalKlinesDataset> choosedLocalDatasets;
        private LoadingKlinesForms? loadingKlinesForms;
        private LoadLocalDatasetsForm? loadingLocalForm;
        private DatasetGraphicDisplayForm? datasetsDisplay;
        private DatasetCourseChangeAnalysis? datasetsCourseAnalysis;
        private AI_TrainWindow? aiTrainWindow;
        public Form1()
        {
            choosedLocalDatasets = new List<LocalKlinesDataset>();
            InitializeComponent();
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
                datasetsCourseAnalysis = new DatasetCourseChangeAnalysis(choosedLocalDatasets);
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
                // Простая модель для прогрева
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
    }
}
