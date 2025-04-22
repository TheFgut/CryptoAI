using CryptoAI_Upgraded.AI_Training.NeuralNetworkCreating;
using Keras.Models;
using System.Text;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks.UI
{
    public partial class NetworkManagePanel : UserControl
    {
        private NeuralNetworkCreatorWindow? networkCreaterForm;
        private ModelDetailsWindow? modelDetailsForm;
        public NeuralNetwork? neuralNetwork { get; private set; }
        public Action<NeuralNetwork?>? onNetworkChanges { get; set; }
        public NetworkManagePanel()
        {
            InitializeComponent();
            if (neuralNetwork == null) SaveNetworkBut.Enabled = false;
        }

        private void SaveNetworkBut_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = DataPaths.networksPath;
                saveFileDialog.FileName = "Network";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    neuralNetwork?.Save(filePath);
                }
            }

        }

        private void CreateNetworkBut_Click(object sender, EventArgs e)
        {
            if (networkCreaterForm == null)
            {
                networkCreaterForm = new NeuralNetworkCreatorWindow(AssignNetwork);
                networkCreaterForm.FormClosed += (sender, args) => networkCreaterForm = null;
                networkCreaterForm.Show();
            }
        }

        private void LoadNeworkBut_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.SelectedPath = DataPaths.datasetsPath;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderDialog.SelectedPath;
                    try
                    {
                        NeuralNetwork? network = new NeuralNetwork(selectedPath);
                        AssignNetwork(network);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed: {ex.Message}");
                    }

                }
            }
        }

        private void NetworkNameChangePanel_TextChanged(object sender, EventArgs e)
        {

        }

        private void AssignNetwork(NeuralNetwork? neuralNetwork)
        {
            this.neuralNetwork = neuralNetwork;
            if (neuralNetwork == null)
            {
                SaveNetworkBut.Enabled = false;
                NetworkNameLabel.ForeColor = Color.Black;
                NetworkNameLabel.Text = "Network not loaded";
                NetworkDetailsPanel.Text = "";
            }
            else
            {
                SaveNetworkBut.Enabled = true;
                NetworkNameLabel.ForeColor = Color.Red;
                NetworkNameLabel.Text = "Network loaded";

                StringBuilder NetworkDetails = new StringBuilder();
                NetworkDetails.AppendLine("Network details:");
                NetworkDetails.AppendLine();
                NetworkDetails.AppendLine($"Inputs/timeFragments: {neuralNetwork.inputCount}/{neuralNetwork.timeFragments}");
                NetworkDetails.Append($"Input features: ");
                for (int i = 0; i < neuralNetwork.features.Length;i++)
                {
                    var feature = neuralNetwork.features[i];
                    string backSpace = i < neuralNetwork.features.Length - 1 ? ", " : "";
                    NetworkDetails.Append($"{feature.ToString()}{backSpace}");
                }
                NetworkDetails.AppendLine();
                NetworkDetails.AppendLine($"Layers count: {neuralNetwork.layersCount}");
                NetworkDetails.AppendLine($"Total neurons: {neuralNetwork.neuronsCount}");
                NetworkDetails.AppendLine($"Outputs count: {neuralNetwork.outputCount}\n");

                NetworkDetails.AppendLine();
                NetworkDetails.AppendLine("Layers structure:");
                foreach(var layer in neuralNetwork.networkConfig.networkLayers)
                {
                    NetworkDetails.AppendLine($"type:{layer.layerType} neurons:{layer.neuronsCount}" +
                        $" activation:{layer.activation} bias:{layer.withBias}");
                }

                NetworkDetailsPanel.Text = NetworkDetails.ToString();


            }
            onNetworkChanges?.Invoke(neuralNetwork);
            ReopenDetailsWindow();
        }

        private void NetworkNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void DetailsBut_Click(object sender, EventArgs e)
        {
            if (modelDetailsForm == null)
            {
                modelDetailsForm = new ModelDetailsWindow(neuralNetwork);
                modelDetailsForm.FormClosed += (sender, args) => modelDetailsForm = null;
                modelDetailsForm.Show();
            }
        }

        private void ReopenDetailsWindow()
        {
            if (modelDetailsForm != null)
            {
                modelDetailsForm.Close();
                modelDetailsForm = new ModelDetailsWindow(neuralNetwork);
                modelDetailsForm.FormClosed += (sender, args) => modelDetailsForm = null;
                modelDetailsForm.Show();
            }

        }
    }
}
