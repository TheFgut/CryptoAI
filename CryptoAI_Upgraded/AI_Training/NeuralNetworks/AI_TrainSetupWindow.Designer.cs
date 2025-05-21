namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks
{
    partial class AI_TrainSetupWindow
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
            stopWhenErrorNotChangingCheckbox = new CheckBox();
            stopLearningTresholdTextBox = new TextBox();
            minDeltaLabel = new Label();
            runsCheckToStopTextBox = new TextBox();
            label1 = new Label();
            ReduceLrOnPlateauCheckBox = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            redLrOnPlPatienceTextBox = new TextBox();
            redLrOnPlFactorTextBox = new TextBox();
            redLrOnPlMinLrTextBox = new TextBox();
            SuspendLayout();
            // 
            // stopWhenErrorNotChangingCheckbox
            // 
            stopWhenErrorNotChangingCheckbox.AutoSize = true;
            stopWhenErrorNotChangingCheckbox.Location = new Point(12, 78);
            stopWhenErrorNotChangingCheckbox.Name = "stopWhenErrorNotChangingCheckbox";
            stopWhenErrorNotChangingCheckbox.Size = new Size(183, 19);
            stopWhenErrorNotChangingCheckbox.TabIndex = 0;
            stopWhenErrorNotChangingCheckbox.Text = "stop when error not changing";
            stopWhenErrorNotChangingCheckbox.UseVisualStyleBackColor = true;
            stopWhenErrorNotChangingCheckbox.Validated += stopWhenErrorRisingCheckbox_Validated;
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
            // ReduceLrOnPlateauCheckBox
            // 
            ReduceLrOnPlateauCheckBox.AutoSize = true;
            ReduceLrOnPlateauCheckBox.Location = new Point(334, 78);
            ReduceLrOnPlateauCheckBox.Name = "ReduceLrOnPlateauCheckBox";
            ReduceLrOnPlateauCheckBox.Size = new Size(131, 19);
            ReduceLrOnPlateauCheckBox.TabIndex = 5;
            ReduceLrOnPlateauCheckBox.Text = "reduce lr on plateau";
            ReduceLrOnPlateauCheckBox.UseVisualStyleBackColor = true;
            ReduceLrOnPlateauCheckBox.Validated += ReduceLrOnPlateauCheckBox_Validated;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(298, 12);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 6;
            label2.Text = "Patience";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(379, 12);
            label3.Name = "label3";
            label3.Size = new Size(40, 15);
            label3.TabIndex = 7;
            label3.Text = "Factor";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(444, 12);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 8;
            label4.Text = "MinLr";
            // 
            // redLrOnPlPatienceTextBox
            // 
            redLrOnPlPatienceTextBox.Location = new Point(298, 36);
            redLrOnPlPatienceTextBox.Name = "redLrOnPlPatienceTextBox";
            redLrOnPlPatienceTextBox.Size = new Size(65, 23);
            redLrOnPlPatienceTextBox.TabIndex = 9;
            redLrOnPlPatienceTextBox.Validated += redLrOnPlPatienceTextBox_Validated;
            // 
            // redLrOnPlFactorTextBox
            // 
            redLrOnPlFactorTextBox.Location = new Point(368, 36);
            redLrOnPlFactorTextBox.Name = "redLrOnPlFactorTextBox";
            redLrOnPlFactorTextBox.Size = new Size(60, 23);
            redLrOnPlFactorTextBox.TabIndex = 10;
            redLrOnPlFactorTextBox.Validated += redLrOnPlFactorTextBox_Validated;
            // 
            // redLrOnPlMinLrTextBox
            // 
            redLrOnPlMinLrTextBox.Location = new Point(434, 36);
            redLrOnPlMinLrTextBox.Name = "redLrOnPlMinLrTextBox";
            redLrOnPlMinLrTextBox.Size = new Size(64, 23);
            redLrOnPlMinLrTextBox.TabIndex = 11;
            redLrOnPlMinLrTextBox.Validated += redLrOnPlMinLrTextBox_Validated;
            // 
            // AI_SetupWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(510, 119);
            Controls.Add(redLrOnPlMinLrTextBox);
            Controls.Add(redLrOnPlFactorTextBox);
            Controls.Add(redLrOnPlPatienceTextBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(ReduceLrOnPlateauCheckBox);
            Controls.Add(label1);
            Controls.Add(runsCheckToStopTextBox);
            Controls.Add(minDeltaLabel);
            Controls.Add(stopLearningTresholdTextBox);
            Controls.Add(stopWhenErrorNotChangingCheckbox);
            Name = "AI_SetupWindow";
            Text = "AI_SetupWindow";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox stopWhenErrorNotChangingCheckbox;
        private TextBox stopLearningTresholdTextBox;
        private Label minDeltaLabel;
        private TextBox runsCheckToStopTextBox;
        private Label label1;
        private CheckBox ReduceLrOnPlateauCheckBox;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox redLrOnPlPatienceTextBox;
        private TextBox redLrOnPlFactorTextBox;
        private TextBox redLrOnPlMinLrTextBox;
    }
}