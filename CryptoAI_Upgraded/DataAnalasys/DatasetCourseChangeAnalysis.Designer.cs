﻿namespace CryptoAI_Upgraded.DatasetsAnalasys
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            AnalyzeBut = new Button();
            AnaliseResultDisp = new RichTextBox();
            AnalizeProgressBar = new ProgressBar();
            datasetsManagerPanel1 = new DatasetsManaging.UI.DatasetsManagerPanel();
            networkManagePanel1 = new AI_Training.NeuralNetworks.UI.NetworkManagePanel();
            CheckGuessedDirectionBox = new CheckBox();
            CheckErrorBox = new CheckBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            PredictionsGraphic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            IgnoreCommissinChackBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)PredictionsGraphic).BeginInit();
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
            datasetsManagerPanel1.title = "Datasets manager";
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
            // PredictionsGraphic
            // 
            chartArea1.Name = "ChartArea1";
            PredictionsGraphic.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            PredictionsGraphic.Legends.Add(legend1);
            PredictionsGraphic.Location = new Point(3, 78);
            PredictionsGraphic.Name = "PredictionsGraphic";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            PredictionsGraphic.Series.Add(series1);
            PredictionsGraphic.Size = new Size(474, 185);
            PredictionsGraphic.TabIndex = 11;
            PredictionsGraphic.Text = "chart1";
            // 
            // IgnoreCommissinChackBox
            // 
            IgnoreCommissinChackBox.AutoSize = true;
            IgnoreCommissinChackBox.Location = new Point(233, 28);
            IgnoreCommissinChackBox.Name = "IgnoreCommissinChackBox";
            IgnoreCommissinChackBox.Size = new Size(128, 19);
            IgnoreCommissinChackBox.TabIndex = 12;
            IgnoreCommissinChackBox.Text = "ignore commission";
            IgnoreCommissinChackBox.UseVisualStyleBackColor = true;
            // 
            // DatasetCourseChangeAnalysis
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(842, 519);
            Controls.Add(IgnoreCommissinChackBox);
            Controls.Add(PredictionsGraphic);
            Controls.Add(CheckErrorBox);
            Controls.Add(CheckGuessedDirectionBox);
            Controls.Add(networkManagePanel1);
            Controls.Add(datasetsManagerPanel1);
            Controls.Add(AnalizeProgressBar);
            Controls.Add(AnaliseResultDisp);
            Controls.Add(AnalyzeBut);
            Name = "DatasetCourseChangeAnalysis";
            Text = "DatasetCourseChangeAnalysis";
            ((System.ComponentModel.ISupportInitialize)PredictionsGraphic).EndInit();
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
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataVisualization.Charting.Chart PredictionsGraphic;
        private CheckBox IgnoreCommissinChackBox;
    }
}