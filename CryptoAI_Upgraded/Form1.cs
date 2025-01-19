using Binance.Net.Interfaces;
using CryptoAI_Upgraded.AI_Training;
using CryptoAI_Upgraded.DataLocalChoosing;
using CryptoAI_Upgraded.DataSaving;
using CryptoAI_Upgraded.Datasets;
using CryptoAI_Upgraded.DatasetsAnalasys;
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
            trainAI_But.Enabled = false;
            InitializeKerasAsync();
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
                aiTrainWindow = new AI_TrainWindow(choosedLocalDatasets);
                aiTrainWindow.FormClosed += (sender, args) => aiTrainWindow = null;
                aiTrainWindow.Show();
            }
        }

        public void InitializeKerasAsync()
        {
            Task.Run(() =>
            {
                // Создаем минимальный слой для прогрева
                var denseLayer = new Dense(10, activation: "relu");

                // Фиктивный входной тензор
                var inputTensor = np.random.rand(1, 5); // Один пример с 5 входами

                Invoke(ActivateTrainButtons);
            });
        }

        private void ActivateTrainButtons()
        {
            trainAI_But.Enabled = true;
        }
    }
}
