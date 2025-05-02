using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks
{
    public partial class AI_SetupWindow : Form
    {
        private TrainingConfigData trainingConfig;
        public AI_SetupWindow(TrainingConfigData trainingConfig)
        {
            this.trainingConfig = trainingConfig;
            InitializeComponent();
            stopLearningTresholdTextBox.Text = trainingConfig.minErrorDeltaToStop.ToString();
            stopWhenErrorNotChangingCheckbox.Checked = trainingConfig.stopWhenErrorRising;
            runsCheckToStopTextBox.Text = trainingConfig.patienceToStop.ToString();
        }

        private void errorToStopBorderTextBox_Validated(object sender, EventArgs e)
        {
            double result;
            if (!double.TryParse(stopLearningTresholdTextBox.Text, out result))
            {
                MessageBox.Show($"Your input: \"{stopLearningTresholdTextBox.Text}\"" +
                    $" is incorrect. Please write a number", "InputError",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                stopLearningTresholdTextBox.Text = trainingConfig.minErrorDeltaToStop.ToString();
                return;
            }
            trainingConfig.minErrorDeltaToStop = result;
        }

        private void stopWhenErrorRisingCheckbox_Validated(object sender, EventArgs e)
        {
            trainingConfig.stopWhenErrorRising = stopWhenErrorNotChangingCheckbox.Checked;
        }

        private void runsCheckToStopTextBox_Validated(object sender, EventArgs e)
        {
            int result;
            if (!int.TryParse(runsCheckToStopTextBox.Text, out result))
            {
                MessageBox.Show($"Your input: \"{runsCheckToStopTextBox.Text}\"" +
                    $" is incorrect. Please write a number", "InputError",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                runsCheckToStopTextBox.Text = trainingConfig.patienceToStop.ToString();
                return;
            }
            trainingConfig.patienceToStop = result;
        }
    }
}
