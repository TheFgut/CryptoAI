using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CryptoAI_Upgraded.DataLocalChoosing
{
    /// <summary>
    /// loads continyous dataset sorted by time
    /// </summary>
    public partial class LoadLocalDatasetsForm : Form
    {
        private List<LocalKlinesDataset> datasets;
        public LoadLocalDatasetsForm(List<LocalKlinesDataset> datasets)
        {
            if (datasets == null) throw new Exception("LoadLocalDatasetsForm.Contsruct failed. datasets cant be null");
            this.datasets = datasets;
            InitializeComponent();
            SelectedDatasetsDisp.SelectedValueChanged += CheckSelection;
            if (datasets.Count > 0)
            {
                foreach (LocalKlinesDataset dataset in datasets)
                {
                    SelectedDatasetsDisp.Items.Add(dataset.fileName);
                }
            }
            UpdateDaysCounter(datasets.Count);
        }


        private void LoadDatasetsBut_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Выберите файлы";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] filePaths = openFileDialog.FileNames;
                    LoadDatasetsSafe(filePaths);
                }
            }
        }

        private void LoadDatasetsSafe(string[] filePaths)
        {
            if (filePaths.Length == 0) return;
            List< LocalKlinesDataset > added = new List<LocalKlinesDataset> ();
            bool hasDifferentCurrency = false;
            string? pair = datasets.Count > 0 ? datasets[0].pair : null;
            foreach (string filePath in filePaths)
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                if (datasets.FirstOrDefault(dataset => dataset.fileName == fileName) != null) continue;//ignoring
                LocalKlinesDataset localDataset = new LocalKlinesDataset(filePath);
                if(pair == null) pair = localDataset.pair;
                else if(localDataset.pair != pair)
                {
                    hasDifferentCurrency = true;
                    break;
                }
                datasets.Add(localDataset);
                added.Add(localDataset);
            }
            if (hasDifferentCurrency)
            {
                foreach (LocalKlinesDataset ad in added)
                {
                    datasets.Remove(ad);
                }
                MessageBox.Show($"Choosed files contains different currency data.\nRemoved {added.Count} elements",
                    "You choosed wrong datasets files", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (datasets.Count == 0) return;
            datasets.Sort((x, y) => DateTime.Compare(x.date, y.date));
            List<int> datasetsGroupsStartIds = new List<int>();
            datasetsGroupsStartIds.Add(0);
            for (int i = 1; i < datasets.Count; i++)
            {
                var dif = (datasets[i].date - datasets[i - 1].date);
                if(dif.TotalDays > 1)
                {
                    datasetsGroupsStartIds.Add(i);
                }
            }
            if(datasetsGroupsStartIds.Count > 1)//need to choose biggest group
            {
                //int[] groupSizes = new int[datasetsGroupsStartIds.Count];
                //int biggestGroupNum = 0;
                //for (int i = 1; i < datasetsGroupsStartIds.Count - 1; i++)
                //{
                //    groupSizes[i - 1] = datasetsGroupsStartIds[i] - datasetsGroupsStartIds[i - 1];
                //}
                //groupSizes[groupSizes.Length - 1] = datasets.Count
                //    - datasetsGroupsStartIds[groupSizes.Length - 1];
                foreach (LocalKlinesDataset ad in added)
                {
                    datasets.Remove(ad);
                }
                MessageBox.Show($"Choosed files not creating continious time fragment.\nRemoved {added.Count} elements",
                    "You choosed wrong datasets files", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                foreach (LocalKlinesDataset ad in added)
                {
                    SelectedDatasetsDisp.Items.Add(ad.fileName);
                }
                UpdateDaysCounter(datasets.Count);
            }
        }

        private void CheckSelection(object? sender, EventArgs e)
        {
            //RemoveSelectedElementBut.Enabled = SelectedDatasetsDisp.SelectedItem != null;
        }

        private void RemoveAllElementsBut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure want to clear all?",
                "Confirm actions",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.OK)
            {
                SelectedDatasetsDisp.Items.Clear();
                datasets.Clear();
                UpdateDaysCounter(0);
            }
        }

        //to do: fix
        private void RemoveSelectedElementBut_Click(object sender, EventArgs e)
        {
            var selectedItem = SelectedDatasetsDisp.SelectedItem;
            if (selectedItem != null)
            {
                SelectedDatasetsDisp.Items.Remove(selectedItem);
                LocalKlinesDataset? toRemove = datasets.Find(element => element.fileName == (selectedItem as string));//to do: optimize
                if (toRemove == null) throw new Exception("LoadLocalDatasetsForm.RemoveSelectedElementBut_Click toRemove is null");
                datasets.Remove(toRemove);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите элемент для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateDaysCounter(int count)
        {
            countDisp.Text = $"Dataset days selected: {count}";
        }
    }
}
