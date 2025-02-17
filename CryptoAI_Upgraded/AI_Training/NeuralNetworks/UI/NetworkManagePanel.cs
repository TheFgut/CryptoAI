using CryptoAI_Upgraded.AI_Training.NeuralNetworkCreating;
using Keras.Models;

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

                NetworkDetailsPanel.Text = $"$Inputs: {neuralNetwork.inputsFeatures}\n" +
                    $"Outputs: {neuralNetwork.outputCount}\n" +
                    $"Layers: {neuralNetwork.layersCount}\n" +
                    $"Total neurons: {neuralNetwork.neuronsCount}";

                //NetworkDetailsPanel.Text = "";
                //var weights = neuralNetwork.model.GetWeights();
                //foreach (var w in weights)
                //{
                //    NetworkDetailsPanel.Text += $"{w}   ";
                //}
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
