namespace CryptoAI_Upgraded.DatasetsAnalasys
{
    partial class DatasetCourseChangeAnalysis
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
            AnalyzeBut = new Button();
            AnaliseResultDisp = new RichTextBox();
            AnalizeProgressBar = new ProgressBar();
            datasetsManagerPanel1 = new DatasetsManaging.UI.DatasetsManagerPanel();
            networkManagePanel1 = new AI_Training.NeuralNetworks.UI.NetworkManagePanel();
            CheckGuessedDirectionBox = new CheckBox();
            CheckErrorBox = new CheckBox();
            SuspendLayout();
            // 
            // AnalyzeBut
            // 
            AnalyzeBut.Location = new Point(12, 485);
            AnalyzeBut.Name = "AnalyzeBut";
            AnalyzeBut.Size = new Size(236, 23);
            AnalyzeBut.TabIndex = 0;
            AnalyzeBut.Text = "Analize";
            AnalyzeBut.UseVisualStyleBackColor = true;
            AnalyzeBut.Click += AnalyzeBut_Click;
            // 
            // AnaliseResultDisp
            // 
            AnaliseResultDisp.Location = new Point(3, 269);
            AnaliseResultDisp.Name = "AnaliseResultDisp";
            AnaliseResultDisp.ReadOnly = true;
            AnaliseResultDisp.Size = new Size(492, 199);
            AnaliseResultDisp.TabIndex = 5;
            AnaliseResultDisp.Text = "";
            // 
            // AnalizeProgressBar
            // 
            AnalizeProgressBar.Location = new Point(254, 485);
            AnalizeProgressBar.Name = "AnalizeProgressBar";
            AnalizeProgressBar.Size = new Size(534, 23);
            AnalizeProgressBar.TabIndex = 6;
            // 
            // datasetsManagerPanel1
            // 
            datasetsManagerPanel1.BackColor = SystemColors.ControlDark;
            datasetsManagerPanel1.Location = new Point(510, 327);
            datasetsManagerPanel1.Name = "datasetsManagerPanel1";
            datasetsManagerPanel1.onDataChanged = null;
            datasetsManagerPanel1.Size = new Size(278, 152);
            datasetsManagerPanel1.TabIndex = 7;
            // 
            // networkManagePanel1
            // 
            networkManagePanel1.BackColor = SystemColors.ControlDark;
            networkManagePanel1.Location = new Point(510, 12);
            networkManagePanel1.Name = "networkManagePanel1";
            networkManagePanel1.onNetworkChanges = null;
            networkManagePanel1.Size = new Size(278, 300);
            networkManagePanel1.TabIndex = 8;
            // 
            // CheckGuessedDirectionBox
            // 
            CheckGuessedDirectionBox.AutoSize = true;
            CheckGuessedDirectionBox.Checked = true;
            CheckGuessedDirectionBox.CheckState = CheckState.Checked;
            CheckGuessedDirectionBox.Location = new Point(12, 28);
            CheckGuessedDirectionBox.Name = "CheckGuessedDirectionBox";
            CheckGuessedDirectionBox.Size = new Size(177, 19);
            CheckGuessedDirectionBox.TabIndex = 9;
            CheckGuessedDirectionBox.Text = "CheckGuessedDirPercentage";
            CheckGuessedDirectionBox.UseVisualStyleBackColor = true;
            // 
            // CheckErrorBox
            // 
            CheckErrorBox.AutoSize = true;
            CheckErrorBox.Checked = true;
            CheckErrorBox.CheckState = CheckState.Checked;
            CheckErrorBox.Location = new Point(12, 53);
            CheckErrorBox.Name = "CheckErrorBox";
            CheckErrorBox.Size = new Size(115, 19);
            CheckErrorBox.TabIndex = 10;
            CheckErrorBox.Text = "checkErrorValues";
            CheckErrorBox.UseVisualStyleBackColor = true;
            // 
            // DatasetCourseChangeAnalysis
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(842, 519);
            Controls.Add(CheckErrorBox);
            Controls.Add(CheckGuessedDirectionBox);
            Controls.Add(networkManagePanel1);
            Controls.Add(datasetsManagerPanel1);
            Controls.Add(AnalizeProgressBar);
            Controls.Add(AnaliseResultDisp);
            Controls.Add(AnalyzeBut);
            Name = "DatasetCourseChangeAnalysis";
            Text = "DatasetCourseChangeAnalysis";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button AnalyzeBut;
        private RichTextBox AnaliseResultDisp;
        private ProgressBar AnalizeProgressBar;
        private DatasetsManaging.UI.DatasetsManagerPanel datasetsManagerPanel1;
        private AI_Training.NeuralNetworks.UI.NetworkManagePanel networkManagePanel1;
        private CheckBox CheckGuessedDirectionBox;
        private CheckBox CheckErrorBox;
    }
}