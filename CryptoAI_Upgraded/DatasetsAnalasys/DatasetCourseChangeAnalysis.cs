using CryptoAI_Upgraded.DataLocalChoosing;
using CryptoAI_Upgraded.Datasets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoAI_Upgraded.DatasetsAnalasys
{
    public partial class DatasetCourseChangeAnalysis : Form
    {
        private List<LocalKlinesDataset> datasets;
        private const float spotTradeBuyComission = 0.001f;//0.1 percent
        private const float spotTradeSellComission = 0.001f;//0.1 percent
        public DatasetCourseChangeAnalysis(List<LocalKlinesDataset> datasets)
        {
            if (datasets == null) throw new Exception("DatasetCourseChangeAnalysis.Contsruct failed. datasets cant be null");
            this.datasets = datasets;
            InitializeComponent();

            if (datasets.Count == 0) AnalyzeBut.Enabled = false;
            TargetProfitPercentText.Validating += ValidateValue;
            TargetPredictionIntervalsText.Validating += ValidateValue;
        }

        private void ValidateValue(object? sender, CancelEventArgs e)
        {
            TextBox box = sender as TextBox;

        }

        private async void AnalyzeBut_Click(object sender, EventArgs e)
        {
            AnalyzeBut.Enabled = false;
            //AnaliseResultDisp
            await analizeTask(5);
            AnalyzeBut.Enabled = true;

        }

        private async Task analizeTask(int analizeIntervalTarget)
        {
            //analyzing
            for(int datasetNum = 0; datasetNum < datasets.Count; datasetNum++)
            {
                KlinesDay day = datasets[datasetNum].LoadKlinesFromCache();
                KlinesDay? nestDay = datasetNum < datasets.Count ? datasets[datasetNum].LoadKlinesFromCache() : null;
                await Task.Yield();
                for (int dayIntervalNum = 0; dayIntervalNum < day.data.Count; dayIntervalNum++)
                {
                    KLine dayIntervalFragment = day.data[dayIntervalNum];
                    if(dayIntervalNum + analizeIntervalTarget < day.data.Count)
                    {
                        if (nestDay == null) break;//finish
                        //int nextDay
                        //nestDay.data[]
                    }
                }
            }
        }
    }
}
