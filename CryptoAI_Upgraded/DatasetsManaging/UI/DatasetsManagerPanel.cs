using CryptoAI_Upgraded.DataLocalChoosing;
using CryptoAI_Upgraded.DataSaving;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;

namespace CryptoAI_Upgraded.DatasetsManaging.UI
{
    public partial class DatasetsManagerPanel : UserControl
    {
        public List<LocalKlinesDataset> choosedLocalDatasets { get; private set; }
        public Action<List<LocalKlinesDataset>>? onDataChanged { get; set; }
        public string title { get { return DatasetsManagerLabel.Text; } set { DatasetsManagerLabel.Text = value; } }

        private LoadLocalDatasetsForm? loadingLocalForm;
        private SavableConfig config;
        public DatasetsManagerPanel()
        {
            choosedLocalDatasets = new List<LocalKlinesDataset>();
            InitializeComponent();
            DatasetsDetailsDisp.Text = "No datasets loaded";
        }

        #region configuration
        public void InitFromConfig(SavableConfig config)
        {
            this.config = config;
            List<string>  filePaths = config.GetObjectOrDefault("filesPaths", new List<string>());
            if(choosedLocalDatasets == null) choosedLocalDatasets = new List<LocalKlinesDataset>();
            foreach (string path in filePaths)
            {
                choosedLocalDatasets.Add(new LocalKlinesDataset(path));
            }
            UpdateData();
        }

        private void SaveConfig()
        {
            if (config == null) return;
            List<string> filePaths = new List<string>();
            foreach (LocalKlinesDataset dataset in choosedLocalDatasets)
            {
                filePaths.Add(dataset.filePath);
            }
            config.SetObject("filesPaths", filePaths);
            config.Save();
        }
        #endregion

        private void ModifyDatasetsBut_Click(object sender, EventArgs e)
        {
            if (loadingLocalForm == null)
            {
                loadingLocalForm = new LoadLocalDatasetsForm(choosedLocalDatasets);
                loadingLocalForm.onDatasetsChanged += UpdateData;
                loadingLocalForm.FormClosed += (sender, args) => loadingLocalForm = null;
                loadingLocalForm.Show();
            }
        }

        private void UpdateData()
        {
            string interval = choosedLocalDatasets.Count == 0 ? "none" : 
                choosedLocalDatasets[0].LoadKlinesIndependant().interval.ToString();
            DatasetsDetailsDisp.Text = $"Datasets count: {choosedLocalDatasets.Count}\n" +
                $"Dataset duration: {choosedLocalDatasets.Count} days\n" +
                $"Interval {interval}";

            int requiredDatasetsCount = 4;
            List<double> openPrices = new List<double>();

            //StringBuilder strBuilder = new StringBuilder();
            //strBuilder.Append("[");
            //if (choosedLocalDatasets != null && choosedLocalDatasets.Count >= requiredDatasetsCount)
            //{
            //    for (int i = 0; i < requiredDatasetsCount; i++)
            //    {
            //        List<KLine> klines = choosedLocalDatasets[i].LoadKlinesIndependant().data;
            //        foreach (var cline in klines)
            //        {
            //            string priceStr = ((double)cline.OpenPrice).ToString();
            //            strBuilder.Append($"{priceStr.Replace(',','.')},");
            //        }
            //    }
            //}

            //DatasetsDetailsDisp.Text = strBuilder.ToString();

            SaveConfig();
            onDataChanged?.Invoke(choosedLocalDatasets);
        }

    }
}
