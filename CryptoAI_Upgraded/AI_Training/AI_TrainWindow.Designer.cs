﻿namespace CryptoAI_Upgraded.AI_Training
{
    partial class AI_TrainWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            StartLearningBut = new Button();
            StopLearningBut = new Button();
            TrainingProgressBar = new ProgressBar();
            lossesChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            runsCountBox = new TextBox();
            networkManagePanel1 = new NeuralNetworks.UI.NetworkManagePanel();
            trainingDatasetsManager = new DatasetsManaging.UI.DatasetsManagerPanel();
            trainingResultPanel = new RichTextBox();
            testingDatasetsManager = new DatasetsManaging.UI.DatasetsManagerPanel();
            TrainingETA = new Label();
            learningSettings = new Button();
            TestErrorsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            lestPredictionsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            LoadBestNetworkBut = new Button();
            label1 = new Label();
            batchesCountTextBox = new TextBox();
            label2 = new Label();
            trainPredictionsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            TrainingSpeedTextBox = new TextBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)lossesChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TestErrorsChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lestPredictionsChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trainPredictionsChart).BeginInit();
            SuspendLayout();
            // 
            // StartLearningBut
            // 
            StartLearningBut.Location = new Point(12, 730);
            StartLearningBut.Name = "StartLearningBut";
            StartLearningBut.Size = new Size(108, 23);
            StartLearningBut.TabIndex = 0;
            StartLearningBut.Text = "Start";
            StartLearningBut.UseVisualStyleBackColor = true;
            StartLearningBut.Click += StartLearningBut_Click;
            // 
            // StopLearningBut
            // 
            StopLearningBut.Location = new Point(126, 730);
            StopLearningBut.Name = "StopLearningBut";
            StopLearningBut.Size = new Size(100, 23);
            StopLearningBut.TabIndex = 1;
            StopLearningBut.Text = "Stop";
            StopLearningBut.UseVisualStyleBackColor = true;
            StopLearningBut.Click += StopLearningBut_Click;
            // 
            // TrainingProgressBar
            // 
            TrainingProgressBar.Location = new Point(12, 759);
            TrainingProgressBar.Name = "TrainingProgressBar";
            TrainingProgressBar.Size = new Size(414, 23);
            TrainingProgressBar.TabIndex = 3;
            // 
            // lossesChart
            // 
            chartArea5.Name = "ChartArea1";
            lossesChart.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            lossesChart.Legends.Add(legend5);
            lossesChart.Location = new Point(12, 12);
            lossesChart.Name = "lossesChart";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            lossesChart.Series.Add(series5);
            lossesChart.Size = new Size(472, 122);
            lossesChart.TabIndex = 4;
            lossesChart.Text = "chart1";
            // 
            // runsCountBox
            // 
            runsCountBox.Location = new Point(12, 701);
            runsCountBox.Name = "runsCountBox";
            runsCountBox.Size = new Size(108, 23);
            runsCountBox.TabIndex = 5;
            runsCountBox.Text = "10";
            runsCountBox.Validated += runsCountBox_Validated;
            // 
            // networkManagePanel1
            // 
            networkManagePanel1.BackColor = SystemColors.ControlDark;
            networkManagePanel1.Location = new Point(510, 12);
            networkManagePanel1.Name = "networkManagePanel1";
            networkManagePanel1.onNetworkChanges = null;
            networkManagePanel1.Size = new Size(349, 253);
            networkManagePanel1.TabIndex = 7;
            // 
            // trainingDatasetsManager
            // 
            trainingDatasetsManager.BackColor = SystemColors.ControlDark;
            trainingDatasetsManager.Location = new Point(510, 271);
            trainingDatasetsManager.Name = "trainingDatasetsManager";
            trainingDatasetsManager.onDataChanged = null;
            trainingDatasetsManager.Size = new Size(178, 196);
            trainingDatasetsManager.TabIndex = 8;
            trainingDatasetsManager.title = "Datasets manager";
            // 
            // trainingResultPanel
            // 
            trainingResultPanel.Location = new Point(12, 570);
            trainingResultPanel.Name = "trainingResultPanel";
            trainingResultPanel.ReadOnly = true;
            trainingResultPanel.Size = new Size(414, 101);
            trainingResultPanel.TabIndex = 9;
            trainingResultPanel.Text = "";
            // 
            // testingDatasetsManager
            // 
            testingDatasetsManager.BackColor = SystemColors.ControlDark;
            testingDatasetsManager.Location = new Point(694, 271);
            testingDatasetsManager.Name = "testingDatasetsManager";
            testingDatasetsManager.onDataChanged = null;
            testingDatasetsManager.Size = new Size(165, 196);
            testingDatasetsManager.TabIndex = 11;
            testingDatasetsManager.title = "Datasets manager";
            // 
            // TrainingETA
            // 
            TrainingETA.AutoSize = true;
            TrainingETA.Font = new Font("Segoe UI", 12F);
            TrainingETA.Location = new Point(432, 761);
            TrainingETA.Name = "TrainingETA";
            TrainingETA.Size = new Size(63, 21);
            TrainingETA.TabIndex = 12;
            TrainingETA.Text = "ETA: 0.1";
            // 
            // learningSettings
            // 
            learningSettings.Location = new Point(374, 701);
            learningSettings.Name = "learningSettings";
            learningSettings.Size = new Size(52, 52);
            learningSettings.TabIndex = 14;
            learningSettings.Text = "setup";
            learningSettings.UseVisualStyleBackColor = true;
            learningSettings.Click += learningSettings_Click;
            // 
            // TestErrorsChart
            // 
            chartArea6.Name = "ChartArea1";
            TestErrorsChart.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            TestErrorsChart.Legends.Add(legend6);
            TestErrorsChart.Location = new Point(12, 140);
            TestErrorsChart.Name = "TestErrorsChart";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            TestErrorsChart.Series.Add(series6);
            TestErrorsChart.Size = new Size(472, 125);
            TestErrorsChart.TabIndex = 15;
            TestErrorsChart.Text = "chart1";
            // 
            // lestPredictionsChart
            // 
            chartArea7.Name = "ChartArea1";
            lestPredictionsChart.ChartAreas.Add(chartArea7);
            legend7.Name = "Legend1";
            lestPredictionsChart.Legends.Add(legend7);
            lestPredictionsChart.Location = new Point(12, 409);
            lestPredictionsChart.Name = "lestPredictionsChart";
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            lestPredictionsChart.Series.Add(series7);
            lestPredictionsChart.Size = new Size(472, 135);
            lestPredictionsChart.TabIndex = 16;
            lestPredictionsChart.Text = "chart1";
            // 
            // LoadBestNetworkBut
            // 
            LoadBestNetworkBut.Location = new Point(432, 570);
            LoadBestNetworkBut.Name = "LoadBestNetworkBut";
            LoadBestNetworkBut.Size = new Size(52, 115);
            LoadBestNetworkBut.TabIndex = 17;
            LoadBestNetworkBut.Text = "Load best trained net.";
            LoadBestNetworkBut.UseVisualStyleBackColor = true;
            LoadBestNetworkBut.Click += LoadBestNetworkBut_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 683);
            label1.Name = "label1";
            label1.Size = new Size(73, 15);
            label1.TabIndex = 18;
            label1.Text = "Trains.Count";
            // 
            // batchesCountTextBox
            // 
            batchesCountTextBox.Location = new Point(126, 701);
            batchesCountTextBox.Name = "batchesCountTextBox";
            batchesCountTextBox.Size = new Size(100, 23);
            batchesCountTextBox.TabIndex = 19;
            batchesCountTextBox.Validated += batchesCountTextBox_Validated;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(126, 683);
            label2.Name = "label2";
            label2.Size = new Size(84, 15);
            label2.TabIndex = 20;
            label2.Text = "Batches.Count";
            // 
            // trainPredictionsChart
            // 
            chartArea8.Name = "ChartArea1";
            trainPredictionsChart.ChartAreas.Add(chartArea8);
            legend8.Name = "Legend1";
            trainPredictionsChart.Legends.Add(legend8);
            trainPredictionsChart.Location = new Point(12, 271);
            trainPredictionsChart.Name = "trainPredictionsChart";
            series8.ChartArea = "ChartArea1";
            series8.Legend = "Legend1";
            series8.Name = "Series1";
            trainPredictionsChart.Series.Add(series8);
            trainPredictionsChart.Size = new Size(472, 132);
            trainPredictionsChart.TabIndex = 21;
            trainPredictionsChart.Text = "chart1";
            // 
            // TrainingSpeedTextBox
            // 
            TrainingSpeedTextBox.Location = new Point(232, 701);
            TrainingSpeedTextBox.Name = "TrainingSpeedTextBox";
            TrainingSpeedTextBox.Size = new Size(108, 23);
            TrainingSpeedTextBox.TabIndex = 22;
            TrainingSpeedTextBox.Text = "10";
            TrainingSpeedTextBox.Validated += TrainingSpeedTextBox_Validated;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(232, 683);
            label3.Name = "label3";
            label3.Size = new Size(83, 15);
            label3.TabIndex = 23;
            label3.Text = "Training speed";
            // 
            // AI_TrainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(864, 786);
            Controls.Add(label3);
            Controls.Add(TrainingSpeedTextBox);
            Controls.Add(trainPredictionsChart);
            Controls.Add(label2);
            Controls.Add(batchesCountTextBox);
            Controls.Add(label1);
            Controls.Add(LoadBestNetworkBut);
            Controls.Add(lestPredictionsChart);
            Controls.Add(TestErrorsChart);
            Controls.Add(learningSettings);
            Controls.Add(TrainingETA);
            Controls.Add(testingDatasetsManager);
            Controls.Add(trainingResultPanel);
            Controls.Add(trainingDatasetsManager);
            Controls.Add(networkManagePanel1);
            Controls.Add(runsCountBox);
            Controls.Add(lossesChart);
            Controls.Add(TrainingProgressBar);
            Controls.Add(StopLearningBut);
            Controls.Add(StartLearningBut);
            Name = "AI_TrainWindow";
            Text = "AI_TrainWindow";
            FormClosing += AI_TrainWindow_FormClosing;
            ((System.ComponentModel.ISupportInitialize)lossesChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)TestErrorsChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)lestPredictionsChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)trainPredictionsChart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartLearningBut;
        private Button StopLearningBut;
        private ProgressBar TrainingProgressBar;
        private System.Windows.Forms.DataVisualization.Charting.Chart lossesChart;
        private TextBox runsCountBox;
        private NeuralNetworks.UI.NetworkManagePanel networkManagePanel1;
        private DatasetsManaging.UI.DatasetsManagerPanel trainingDatasetsManager;
        private RichTextBox trainingResultPanel;
        private DatasetsManaging.UI.DatasetsManagerPanel testingDatasetsManager;
        private Label TrainingETA;
        private Button learningSettings;
        private System.Windows.Forms.DataVisualization.Charting.Chart TestErrorsChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart lestPredictionsChart;
        private Button LoadBestNetworkBut;
        private Label label1;
        private TextBox batchesCountTextBox;
        private Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart trainPredictionsChart;
        private TextBox TrainingSpeedTextBox;
        private Label label3;
    }
}