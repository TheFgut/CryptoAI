using CryptoAI_Upgraded.AI_Training.NeuralNetworks;
using CryptoAI_Upgraded.Datasets;
using CryptoAI_Upgraded.Datasets.DataWalkers;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using static CryptoAI_Upgraded.Helpers;
using CryptoAI_Upgraded.DataAnalasys;
using CryptoAI_Upgraded.RealtimeTrading.SimpleTrader;

namespace CryptoAI_Upgraded.DatasetsAnalasys
{
    public partial class DatasetCourseChangeAnalysis : Form
    {
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

        public DatasetCourseChangeAnalysis(NeuralNetwork neuralNetwork, List<LocalKlinesDataset> datasets)
        {
            InitializeComponent();

            networkManagePanel1.onNetworkChanges += AssingModel;
            datasetsManagerPanel1.InitFromConfig(datasets);
            this.datasets = datasetsManagerPanel1.choosedLocalDatasets;
            AssingModel(neuralNetwork);
        }

        public void StartAnalize()
        {
            if (analizeTask != null) return;
            analizeTask = Analize(neuralNetwork.outputCount);
        }

        private void AnalyzeBut_Click(object sender, EventArgs e)
        {
            if (datasets.Count == 0)
            {
                MessageBox.Show($"Datasets count should be more than one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AnalyzeBut.Enabled = false;
            StartAnalize();
        }

        private async Task Analize(int predictionsLength)
        {
            AnaliseResultDisp.Text = "";
            NerworkAccuraccyAnalizer analizer = new NerworkAccuraccyAnalizer();
            NetworkAccAnalize analize = await analizer.Analize(neuralNetwork, datasets);

            //AnaliseResultDisp.Text += $"Step: {predictionResult[0][0]} {predictionResult[1][0]}\n";

            if (!analize.success)
            {
                if (analize.exception != null)
                {
                    Invoke((Action)(() =>
                    {
                        MessageBox.Show($"Exception during execution of Analize {analize.exception.Message}\n " +
                            $"{analize.exception.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                    return;
                }
                Invoke((Action)(() =>
                {
                    MessageBox.Show($"Analize failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
                return;
            }

            SimpleTradingAnalzier netTradeAnalisys = new SimpleTradingAnalzier(false);
            //to do: use network normalizer
            string tradingAnalize = netTradeAnalisys.Analize(analize, datasets[0].LoadKlinesFromCache().normalization);

            Invoke((Action)(() =>
            {
                //displaying
                AnaliseResultDisp.Text += $"Analized steps: {analize.analizeStepsAmount}\n";
                AnaliseResultDisp.Text += $"Average error: {analize.averageError}\n";
                AnaliseResultDisp.Text += $"Guessed dir percent: {analize.guessedDirPercent}%\n";
                AnaliseResultDisp.Text += tradingAnalize;
                MessageBox.Show($"Analize duration: {analize.testDurationMillisec / 1000f} seconds", 
                    "Analize finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                double[] sma = MovingAverage.Simple(analize.real.ToArray(), 3);
                Helpers.DataPlotting.PlotMultipleSeries(PredictionsGraphic, new[] { "real" , "predictions", "sma"},
                    analize.real.ToArray(), analize.predict.ToArray(), sma);
                new MultiSeriesChartForm(analize.real.ToArray(), analize.predict.ToArray(), sma).Show();
            }));
            analizeTask = null;
            AnalyzeBut.Enabled = true;
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


    }
}