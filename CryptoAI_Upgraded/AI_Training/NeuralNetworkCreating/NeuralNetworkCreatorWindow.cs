using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using Keras.Layers;
using Keras.Utils;
using Keras.Models;
using CryptoAI_Upgraded.AI_Training.NeuralNetworks;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworkCreating
{
    public partial class NeuralNetworkCreatorWindow : Form
    {
        private Action<NeuralNetwork> onNetworkCreatedAction;
        private bool closeAfterCreation;
        public NeuralNetworkCreatorWindow(Action<NeuralNetwork> onNetworkCreatedAction, bool closeAfterCreation = true)
        {
            if (onNetworkCreatedAction == null) throw new Exception("NeuralNetworkCreatorWindow.Creation failed. onNetworkCreatedAction cant be null");
            this.onNetworkCreatedAction = onNetworkCreatedAction;
            this.closeAfterCreation = closeAfterCreation;
            InitializeComponent();
            InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            // Создаем список данных
            BindingList<NNLayerConfig> dataList = new BindingList<NNLayerConfig>()
            {
                new NNLayerConfig(50,ActivationFunc.tanh, LayerType.LSTM, true),
                new NNLayerConfig(1,ActivationFunc.linear, LayerType.Dense, true)
            };

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
            dataSource.Insert(dataSource.Count - 2,new NNLayerConfig(10, ActivationFunc.tanh, LayerType.LSTM, true));
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
            if (VolumeCheckBox.Checked) features.Add(FeatureType.Volume);
            if(features.Count == 0)
            {
                MessageBox.Show($"Network creation failed. Choose at least one input feature", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //parsing inputs count
            int inputsCount = 0;
            if (!int.TryParse(IputsCountBox.Text, out inputsCount))
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
    linear
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
    Volume,
    HighPrice,
    LowPrice,
    QuoteVolume
}