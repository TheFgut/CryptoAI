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
    public partial class AI_TrainSetupWindow : Form
    {
        private TrainingConfigData trainingConfig;
        public AI_TrainSetupWindow(TrainingConfigData trainingConfig)
        {
            this.trainingConfig = trainingConfig;
            InitializeComponent();
            stopLearningTresholdTextBox.Text = trainingConfig.minErrorDeltaToStop.ToString();
            stopWhenErrorNotChangingCheckbox.Checked = trainingConfig.stopWhenErrorRising;
            runsCheckToStopTextBox.Text = trainingConfig.patienceToStop.ToString();

            ReduceLrOnPlateauCheckBox.Checked = trainingConfig.reduceLrOnPlateau;
            redLrOnPlPatienceTextBox.Text = trainingConfig.redLrOnPlPatience.ToString();
            redLrOnPlFactorTextBox.Text = trainingConfig.redLrOnPlFactor.ToString();
            redLrOnPlMinLrTextBox.Text = trainingConfig.redLrOnPlMinLr.ToString();
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

        private void redLrOnPlPatienceTextBox_Validated(object sender, EventArgs e)
        {
            int result;
            if (!int.TryParse(redLrOnPlPatienceTextBox.Text, out result))
            {
                MessageBox.Show($"Your input: \"{redLrOnPlPatienceTextBox.Text}\"" +
                    $" is incorrect. Please write a number", "InputError",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                redLrOnPlPatienceTextBox.Text = trainingConfig.redLrOnPlPatience.ToString();
                return;
            }
            trainingConfig.redLrOnPlPatience = result;
        }

        private void redLrOnPlFactorTextBox_Validated(object sender, EventArgs e)
        {
            float result;
            if (!float.TryParse(redLrOnPlFactorTextBox.Text, out result))
            {
                MessageBox.Show($"Your input: \"{redLrOnPlFactorTextBox.Text}\"" +
                    $" is incorrect. Please write a floating point number", "InputError",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                redLrOnPlFactorTextBox.Text = trainingConfig.redLrOnPlFactor.ToString();
                return;
            }
            trainingConfig.redLrOnPlFactor = result;
        }

        private void redLrOnPlMinLrTextBox_Validated(object sender, EventArgs e)
        {
            float result;
            if (!float.TryParse(redLrOnPlMinLrTextBox.Text, out result))
            {
                MessageBox.Show($"Your input: \"{redLrOnPlMinLrTextBox.Text}\"" +
                    $" is incorrect. Please write a floating point number", "InputError",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                redLrOnPlMinLrTextBox.Text = trainingConfig.redLrOnPlMinLr.ToString();
                return;
            }
            trainingConfig.redLrOnPlMinLr = result;
        }

        private void ReduceLrOnPlateauCheckBox_Validated(object sender, EventArgs e)
        {
            trainingConfig.reduceLrOnPlateau = ReduceLrOnPlateauCheckBox.Checked;
        }
    }
}
