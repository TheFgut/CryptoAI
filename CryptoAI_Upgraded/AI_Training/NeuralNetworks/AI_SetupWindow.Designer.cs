namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks
{
    partial class AI_SetupWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            stopWhenErrorRisingCheckbox = new CheckBox();
            stopLearningTresholdTextBox = new TextBox();
            minDeltaLabel = new Label();
            runsCheckToStopTextBox = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // stopWhenErrorRisingCheckbox
            // 
            stopWhenErrorRisingCheckbox.AutoSize = true;
            stopWhenErrorRisingCheckbox.Location = new Point(12, 78);
            stopWhenErrorRisingCheckbox.Name = "stopWhenErrorRisingCheckbox";
            stopWhenErrorRisingCheckbox.Size = new Size(141, 19);
            stopWhenErrorRisingCheckbox.TabIndex = 0;
            stopWhenErrorRisingCheckbox.Text = "stop when error rising";
            stopWhenErrorRisingCheckbox.UseVisualStyleBackColor = true;
            stopWhenErrorRisingCheckbox.Validated += stopWhenErrorRisingCheckbox_Validated;
            // 
            // stopLearningTresholdTextBox
            // 
            stopLearningTresholdTextBox.Location = new Point(117, 36);
            stopLearningTresholdTextBox.Name = "stopLearningTresholdTextBox";
            stopLearningTresholdTextBox.Size = new Size(58, 23);
            stopLearningTresholdTextBox.TabIndex = 1;
            stopLearningTresholdTextBox.Text = "0.1";
            stopLearningTresholdTextBox.Validated += errorToStopBorderTextBox_Validated;
            // 
            // minDeltaLabel
            // 
            minDeltaLabel.AutoSize = true;
            minDeltaLabel.Location = new Point(117, 9);
            minDeltaLabel.Name = "minDeltaLabel";
            minDeltaLabel.Size = new Size(57, 15);
            minDeltaLabel.TabIndex = 2;
            minDeltaLabel.Text = "Min delta";
            // 
            // runsCheckToStopTextBox
            // 
            runsCheckToStopTextBox.Location = new Point(11, 36);
            runsCheckToStopTextBox.Name = "runsCheckToStopTextBox";
            runsCheckToStopTextBox.Size = new Size(64, 23);
            runsCheckToStopTextBox.TabIndex = 3;
            runsCheckToStopTextBox.Validated += runsCheckToStopTextBox_Validated;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 12);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 4;
            label1.Text = "Patience";
            // 
            // AI_SetupWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(199, 119);
            Controls.Add(label1);
            Controls.Add(runsCheckToStopTextBox);
            Controls.Add(minDeltaLabel);
            Controls.Add(stopLearningTresholdTextBox);
            Controls.Add(stopWhenErrorRisingCheckbox);
            Name = "AI_SetupWindow";
            Text = "AI_SetupWindow";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox stopWhenErrorRisingCheckbox;
        private TextBox stopLearningTresholdTextBox;
        private Label minDeltaLabel;
        private TextBox runsCheckToStopTextBox;
        private Label label1;
    }
}