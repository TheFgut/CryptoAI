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

namespace CryptoAI_Upgraded.Datasets.NromalizationAndConvertion
{
    public partial class DatasetConvertorAndNormalizerWindow : Form
    {
        private string? savePath;
        private List<LocalKlinesDataset>? localKlinesDatasets;
        public DatasetConvertorAndNormalizerWindow()
        {
            InitializeComponent();
            datasetsManagerPanel1.onDataChanged += DatasetsLoaded;
            CheckNormalizeDataPossibility();
        }

        private void NormalizeDatasetBut_Click(object sender, EventArgs e)
        {
            DatasetNormalizerAndConverter nromalizer = new DatasetNormalizerAndConverter();
            nromalizer.Convert(localKlinesDatasets, savePath);
            localKlinesDatasets = null;
        }

        private void ChoosePathBut_Click(object sender, EventArgs e)
        {
            savePath = SelectFile();
            CheckNormalizeDataPossibility();
        }

        private void DatasetsLoaded(List<LocalKlinesDataset> localKlinesDatasets)
        {
            this.localKlinesDatasets = localKlinesDatasets;
            CheckNormalizeDataPossibility();
        }

        private string? SelectFile()
        {
            using (FolderBrowserDialog chooseFolderDialog = new FolderBrowserDialog())
            {
                chooseFolderDialog.Description = "Choose save path";
                chooseFolderDialog.ShowNewFolderButton = true;
                if (chooseFolderDialog.ShowDialog() == DialogResult.OK)
                {
                    return chooseFolderDialog.SelectedPath;
                }
            }
            return null;
        }

        private void CheckNormalizeDataPossibility()
        {
            if(localKlinesDatasets == null || localKlinesDatasets.Count == 0 
                || savePath == null)
            {
                NormalizeDatasetBut.Enabled = false;
            }
            else
            {
                NormalizeDatasetBut.Enabled = true;
            }
        }
    }
}
