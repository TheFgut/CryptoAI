using CryptoAI_Upgraded.AI_Training.NeuralNetworks.UI;
using CryptoAI_Upgraded.DatasetsManaging.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoAI_Upgraded.AI_Prediction
{
    public partial class AIPredictorForm : Form
    {
        public AIPredictorForm()
        {
            InitializeComponent();
            networkManagePanel1.onNetworkChanges += aiPredictionsDatasetWalkerPanel1.NetworkChanged;
            datasetsManagerPanel1.onDataChanged += aiPredictionsDatasetWalkerPanel1.DataSetsChanged;
        }

        private void aiPredictionsDatasetWalkerPanel1_Load(object sender, EventArgs e)
        {

        }
    }
}
