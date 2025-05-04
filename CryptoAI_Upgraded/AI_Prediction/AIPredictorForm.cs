
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
