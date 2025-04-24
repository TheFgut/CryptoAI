using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks.UI
{
    public partial class NetworkLearningStatsPanel : UserControl
    {
        public NetworkLearningStatsPanel()
        {
            InitializeComponent();
        }

        public void Init(NetworkTrainingsStats networkTrainingStats)
        {
            NetworkRunData[] runs = networkTrainingStats.GetTrainHistory();
            StringBuilder sb = new StringBuilder();
            if (runs.Length == 0) sb.Append("No train history");
            foreach (NetworkRunData run in runs)
            {
                sb.Append($"{run.ToString()}\n");
            }
            TrainingHistoryDisplay.Text = sb.ToString();
        }
    }
}
