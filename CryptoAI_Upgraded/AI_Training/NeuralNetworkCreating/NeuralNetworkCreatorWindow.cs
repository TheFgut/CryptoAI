using System.ComponentModel;
using CryptoAI_Upgraded.AI_Training.NeuralNetworks;
using CryptoAI_Upgraded.DataSaving;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworkCreating
{
    public partial class NeuralNetworkCreatorWindow : Form
    {
        private Action<NeuralNetwork> onNetworkCreatedAction;
        private SavableConfig config;
        private bool closeAfterCreation;
        public NeuralNetworkCreatorWindow(Action<NeuralNetwork> onNetworkCreatedAction, bool closeAfterCreation = true)
        {
            if (onNetworkCreatedAction == null) throw new Exception("NeuralNetworkCreatorWindow.Creation failed. onNetworkCreatedAction cant be null");
            this.onNetworkCreatedAction = onNetworkCreatedAction;
            this.closeAfterCreation = closeAfterCreation;
            InitializeComponent();
            LoadConfiguration();
            InitializeDataGrid();
        }

        private void LoadConfiguration()
        {
            config = new SavableConfig(DataPaths.appConfigurationPath, "networkCreater");
            OpenPriceCheckBox.Checked = true;
            ClosePriceCheckBox.Checked = config.GetBool("ClosePrice");
            HighPriceCheckBox.Checked = config.GetBool("HighPrice");
            LowPriceCheckBox.Checked = config.GetBool("LowPrice");
            FragmentNumCheckBox.Checked = config.GetBool("FragmentNum");
            QuoteVolumeCheckBox.Checked = config.GetBool("QuoteVolume");
            TradeCountBox.Checked = config.GetBool("TradeCount");
            PriceDeltaCheckBox.Checked = config.GetBool("PreceDelta");
            CandleTypeCheckBox.Checked = config.GetBool("CandleType");
            VolatilityCheckBox.Checked = config.GetBool("Volatility");

            InputsCountBox.Text = config.GetStrinOrDefault("InputsCount", "1");
            TimeFragmentsBox.Text = config.GetStrinOrDefault("TimeFragments", "6");
        }

        private void SaveConfiguration()
        {
            config.SetBool("OpenPrice", OpenPriceCheckBox.Checked);
            config.SetBool("ClosePrice", ClosePriceCheckBox.Checked);
            config.SetBool("HighPrice", HighPriceCheckBox.Checked);
            config.SetBool("LowPrice", LowPriceCheckBox.Checked);
            config.SetBool("FragmentNum", FragmentNumCheckBox.Checked);
            config.SetBool("QuoteVolume", QuoteVolumeCheckBox.Checked);
            config.SetBool("TradeCount", TradeCountBox.Checked);
            config.SetBool("PreceDelta", PriceDeltaCheckBox.Checked);
            config.SetBool("CandleType", CandleTypeCheckBox.Checked);
            config.SetBool("Volatility", VolatilityCheckBox.Checked);

            config.SetString("InputsCount", InputsCountBox.Text);
            config.SetString("TimeFragments", TimeFragmentsBox.Text);
            config.SetObject("LayersConfigurations", ((BindingList<NNLayerConfig>)LayersGrid.DataSource).ToList());
            config.Save();
        }

        private void InitializeDataGrid()
        {
            // Создаем список данных
            BindingList<NNLayerConfig> dataList = new BindingList<NNLayerConfig>(
                config.GetObjectOrDefault("LayersConfigurations",
                new List<NNLayerConfig>()
            {
                new NNLayerConfig(50,ActivationFunc.tanh, LayerType.LSTM, true),
                new NNLayerConfig(1,ActivationFunc.linear, LayerType.Dense, true)
            }));

            // Настройка столбцов: можно задать формат для числовых данных и для enum
            LayersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Neurons Count",
                DataPropertyName = "neuronsCount",
                ValueType = typeof(int)
            });
            LayersGrid.Columns.Add(new DataGridViewComboBoxColumn
            {
                HeaderText = "Activation Function",
                DataPropertyName = "activation",
                DataSource = Enum.GetValues(typeof(ActivationFunc)),
                ValueType = typeof(ActivationFunc)
            });
            LayersGrid.Columns.Add(new DataGridViewComboBoxColumn
            {
                HeaderText = "Layer type",
                DataPropertyName = "layerType",
                DataSource = Enum.GetValues(typeof(LayerType)),
                ValueType = typeof(LayerType)
            });
            LayersGrid.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "With Bias",
                DataPropertyName = "withBias",
                ValueType = typeof(bool)
            });

            LayersGrid.DataSource = dataList;
            // Добавляем возможность редактирования
            LayersGrid.AllowUserToAddRows = true;
            LayersGrid.AllowUserToDeleteRows = true;
        }

        private void AddLayerBut_Click(object sender, EventArgs e)
        {
            BindingList<NNLayerConfig> dataSource = (BindingList<NNLayerConfig>)LayersGrid.DataSource;
            dataSource.Insert(dataSource.Count - 2, new NNLayerConfig(10, ActivationFunc.tanh, LayerType.LSTM, true));
        }

        private void RemoveLayersBut_Click(object sender, EventArgs e)
        {
            if (LayersGrid.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in LayersGrid.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        LayersGrid.Rows.Remove(row);
                    }
                }
            }
        }

        private void CreateNetworkBut_Click(object sender, EventArgs e)
        {
            BindingList<NNLayerConfig> config = (BindingList<NNLayerConfig>)LayersGrid.DataSource;
            //generating features arr
            List<FeatureType> features = new List<FeatureType>();
            if (OpenPriceCheckBox.Checked) features.Add(FeatureType.OpenPrice);
            if (ClosePriceCheckBox.Checked) features.Add(FeatureType.ClosePrice);
            if (HighPriceCheckBox.Checked) features.Add(FeatureType.HighPrice);
            if (LowPriceCheckBox.Checked) features.Add(FeatureType.LowPrice);
            if (QuoteVolumeCheckBox.Checked) features.Add(FeatureType.QuoteVolume);
            if (TradeCountBox.Checked) features.Add(FeatureType.TradeCount);
            if (FragmentNumCheckBox.Checked) features.Add(FeatureType.FragmentNum);
            if (PriceDeltaCheckBox.Checked) features.Add(FeatureType.PriceDelta);
            if (CandleTypeCheckBox.Checked) features.Add(FeatureType.CandleType);
            if (VolatilityCheckBox.Checked) features.Add (FeatureType.Volatility);
            if (features.Count == 0)
            {
                MessageBox.Show($"Network creation failed. Choose at least one input feature", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //parsing inputs count
            int inputsCount = 0;
            if (!int.TryParse(InputsCountBox.Text, out inputsCount))
            {
                MessageBox.Show($"Inputs count are in not correct format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int timeFragmentsCount = 0;
            if (!int.TryParse(TimeFragmentsBox.Text, out timeFragmentsCount))
            {
                MessageBox.Show($"Time fragments count are in not correct format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //generating config
            NNConfigData configData = new NNConfigData(config, features.ToArray(), timeFragmentsCount, inputsCount);
            onNetworkCreatedAction?.Invoke(new NeuralNetwork(configData));
            if (closeAfterCreation) Close();
        }

        private void NeuralNetworkCreatorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfiguration();
        }
    }
}

public class NNLayerConfig
{
    public int neuronsCount { get; set; }
    public ActivationFunc activation { get; set; }
    public LayerType layerType { get; set; }
    public bool withBias { get; set; }

    public NNLayerConfig(int neuronsCount, ActivationFunc activation, LayerType layerType 
        , bool withBias)
    {
        this.neuronsCount = neuronsCount;
        this.activation = activation;
        this.layerType = layerType;
        this.withBias = withBias;
    }
}

public enum ActivationFunc
{
    tanh,
    relu,
    sigmoid,
    linear,
    leaky_relu
}

public enum LayerType
{
    Dense,
    LSTM
}

public enum FeatureType
{
    OpenPrice,
    ClosePrice,
    HighPrice,
    LowPrice,
    QuoteVolume,
    TradeCount,
    FragmentNum,
    PriceDelta,
    /// <summary>
    /// is going up - 1 or down - 0
    /// </summary>
    CandleType,
    Volatility
}