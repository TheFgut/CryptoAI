using CryptoAI_Upgraded.DataLocalChoosing;
using CryptoAI_Upgraded.Datasets;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoAI_Upgraded.DatasetsManaging.UI
{
    public partial class DatasetsManagerPanel : UserControl
    {
        public List<LocalKlinesDataset> choosedLocalDatasets { get; private set; }
        public Action<List<LocalKlinesDataset>>? onDataChanged { get; set; }
        private LoadLocalDatasetsForm? loadingLocalForm;
        public DatasetsManagerPanel()
        {
            choosedLocalDatasets = new List<LocalKlinesDataset>();
            InitializeComponent();
            DatasetsDetailsDisp.Text = "No datasets loaded";
        }

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

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("[");
            if (choosedLocalDatasets != null && choosedLocalDatasets.Count >= requiredDatasetsCount)
            {
                for (int i = 0; i < requiredDatasetsCount; i++)
                {
                    List<KLine> klines = choosedLocalDatasets[i].LoadKlinesIndependant().data;
                    foreach (var cline in klines)
                    {
                        string priceStr = ((double)cline.OpenPrice).ToString();
                        strBuilder.Append($"{priceStr.Replace(',','.')},");
                    }
                }
            }

            DatasetsDetailsDisp.Text = strBuilder.ToString();

            onDataChanged?.Invoke(choosedLocalDatasets);
        }
    }
}
