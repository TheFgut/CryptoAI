using CryptoAI_Upgraded.DataSaving;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks.UI.Upgraded_net_loader
{
    public partial class ModernNetLoader : Form
    {
        private Dictionary<string, NetParams> loadedNetsInfo;
        public ModernNetLoader()
        {
            InitializeComponent();
            InitDataGrid();
            loadedNetsInfo = GetAllNetsRecursively(DataPaths.networksPath);

            List<NetParams> listOfNets = loadedNetsInfo.Values.ToList();

            var table = new DataTable();
            table.Columns.Add("architectureString", typeof(string));
            table.Columns.Add("trainStatistics", typeof(string));
            table.Columns.Add("accuraccy", typeof(double));

            // Заполняем
            foreach (var item in listOfNets)
                table.Rows.Add(item.architectureString, item.trainStatistics, item.accuraccy);
            var bs = new BindingSource { DataSource = table };
            AwailableNetSGrid.DataSource = bs;
        }

        private void InitDataGrid()
        {
            // Создаем список данных
            //BindingList<NNLayerConfig> dataList = new BindingList<NNLayerConfig>(
            //    config.GetObjectOrDefault("LayersConfigurations",
            //    new List<NNLayerConfig>()
            //{
            //    new NNLayerConfig(50,ActivationFunc.tanh, LayerType.LSTM, true),
            //    new NNLayerConfig(1,ActivationFunc.linear, LayerType.Dense, true)
            //}));

            AwailableNetSGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Architecture",
                DataPropertyName = "architectureString",
                ValueType = typeof(string)
            });
            //AwailableNetSGrid.Columns.Add(new DataGridViewComboBoxColumn
            //{
            //    HeaderText = "Activation Function",
            //    DataPropertyName = "activation",
            //    DataSource = Enum.GetValues(typeof(ActivationFunc)),
            //    ValueType = typeof(ActivationFunc)
            //});
            AwailableNetSGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Train stats",
                DataPropertyName = "trainStatistics",
                ValueType = typeof(string)
            });
            AwailableNetSGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Accuracy",
                DataPropertyName = "accuraccy",
                ValueType = typeof(double)
            });
        }

        private Dictionary<string, NetParams> GetAllNetsRecursively(string rootPath)
        {
            var result = new Dictionary<string, NetParams>(StringComparer.OrdinalIgnoreCase);

            if (!Directory.Exists(rootPath))
                throw new DirectoryNotFoundException($"Root folder not found: {rootPath}");

            foreach (var filePath in Directory.EnumerateFiles(rootPath, "config.bson", SearchOption.AllDirectories))
            {
                string folderPath = Path.GetDirectoryName(filePath)!;
                if (!result.ContainsKey(folderPath))
                    result[folderPath] = new NetParams(folderPath);
            }

            return result;
        }

        private void ReloadBut_Click(object sender, EventArgs e)
        {

        }

        private class NetParams
        {
            public NNConfigData config { get; set; }
            public NetworkTrainingsStats? stats { get; set; }

            //params
            public string architectureString 
            { 
                get
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine($"{config.inputsLen}x");
                    for (int i = 0; i < config.networkLayers.Length;i++)
                    {
                        var layer = config.networkLayers[i];
                        string withBias = layer.withBias ? "b" : "";
                        stringBuilder.AppendLine($"{layer.neuronsCount}{withBias}");
                        if (i != config.networkLayers.Length - 1) stringBuilder.AppendLine("x");
                    }
                    return stringBuilder.ToString();
                } 
            }

            public string trainStatistics
            {
                get
                {
                    if (stats == null) return "no data";
                    if(stats.allTrains.Length == 0) return "not trained";
                    string averageTrainSpeed = stats.allTrains.Select(t => t.trainSpeed).Average().ToString("F6");
                    string awerageTrainDatasetsCount = stats.allTrains.Select(t => t.trainingDatasetIDs.Count).Average().ToString("F1");
                    return $"Awg d.l.: {awerageTrainDatasetsCount}; Awg speed: {averageTrainSpeed}";
                }
            }

            public double accuraccy
            {
                get
                {
                    if (stats == null) return 99;
                    if (stats.lastTrain == null) return 99;
                    if (stats.lastTrain.noTestMetrics) return double.Round(stats.lastTrain.averageError,5);
                    return double.Round(stats.lastTrain.avarageTestError,5);
                }
            }

            public NetParams(string directoryPath)
            {
                LocalLoaderAndSaverBSON<NNConfigData> configLoader = 
                    new LocalLoaderAndSaverBSON<NNConfigData>($"{directoryPath}\\config.bson");
                NNConfigData? loadedConfig = configLoader.Load();
                if (loadedConfig == null) throw new Exception("NetParams cant be constructed. NNConfigData is not loaded");
                config = loadedConfig;
                try
                {
                    stats = new NetworkTrainingsStats(directoryPath, true);
                }
                catch (Exception e)
                {
                    stats = null;
                }
            }
        }
    }
}
