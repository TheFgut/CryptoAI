namespace CryptoAI_Upgraded.AI_Training
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            richTextBox1 = new RichTextBox();
            learningSettings = new Button();
            TestErrorsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            lastPredictionsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            LoadBestNetworkBut = new Button();
            ((System.ComponentModel.ISupportInitialize)lossesChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TestErrorsChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lastPredictionsChart).BeginInit();
            SuspendLayout();
            // 
            // StartLearningBut
            // 
            StartLearningBut.Location = new Point(12, 610);
            StartLearningBut.Name = "StartLearningBut";
            StartLearningBut.Size = new Size(75, 23);
            StartLearningBut.TabIndex = 0;
            StartLearningBut.Text = "Start";
            StartLearningBut.UseVisualStyleBackColor = true;
            StartLearningBut.Click += StartLearningBut_Click;
            // 
            // StopLearningBut
            // 
            StopLearningBut.Location = new Point(93, 610);
            StopLearningBut.Name = "StopLearningBut";
            StopLearningBut.Size = new Size(75, 23);
            StopLearningBut.TabIndex = 1;
            StopLearningBut.Text = "Stop";
            StopLearningBut.UseVisualStyleBackColor = true;
            StopLearningBut.Click += StopLearningBut_Click;
            // 
            // TrainingProgressBar
            // 
            TrainingProgressBar.Location = new Point(12, 639);
            TrainingProgressBar.Name = "TrainingProgressBar";
            TrainingProgressBar.Size = new Size(414, 23);
            TrainingProgressBar.TabIndex = 3;
            // 
            // lossesChart
            // 
            chartArea1.Name = "ChartArea1";
            lossesChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            lossesChart.Legends.Add(legend1);
            lossesChart.Location = new Point(12, 12);
            lossesChart.Name = "lossesChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            lossesChart.Series.Add(series1);
            lossesChart.Size = new Size(454, 134);
            lossesChart.TabIndex = 4;
            lossesChart.Text = "chart1";
            // 
            // runsCountBox
            // 
            runsCountBox.Location = new Point(12, 581);
            runsCountBox.Name = "runsCountBox";
            runsCountBox.Size = new Size(156, 23);
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
            trainingResultPanel.Location = new Point(12, 450);
            trainingResultPanel.Name = "trainingResultPanel";
            trainingResultPanel.ReadOnly = true;
            trainingResultPanel.Size = new Size(414, 115);
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
            TrainingETA.Location = new Point(432, 641);
            TrainingETA.Name = "TrainingETA";
            TrainingETA.Size = new Size(63, 21);
            TrainingETA.TabIndex = 12;
            TrainingETA.Text = "ETA: 0.1";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(174, 581);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(252, 52);
            richTextBox1.TabIndex = 13;
            richTextBox1.Text = "";
            // 
            // learningSettings
            // 
            learningSettings.Location = new Point(432, 581);
            learningSettings.Name = "learningSettings";
            learningSettings.Size = new Size(52, 52);
            learningSettings.TabIndex = 14;
            learningSettings.Text = "setup";
            learningSettings.UseVisualStyleBackColor = true;
            learningSettings.Click += learningSettings_Click;
            // 
            // TestErrorsChart
            // 
            chartArea2.Name = "ChartArea1";
            TestErrorsChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            TestErrorsChart.Legends.Add(legend2);
            TestErrorsChart.Location = new Point(12, 152);
            TestErrorsChart.Name = "TestErrorsChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            TestErrorsChart.Series.Add(series2);
            TestErrorsChart.Size = new Size(454, 138);
            TestErrorsChart.TabIndex = 15;
            TestErrorsChart.Text = "chart1";
            // 
            // lastPredictionsChart
            // 
            chartArea3.Name = "ChartArea1";
            lastPredictionsChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            lastPredictionsChart.Legends.Add(legend3);
            lastPredictionsChart.Location = new Point(12, 296);
            lastPredictionsChart.Name = "lastPredictionsChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            lastPredictionsChart.Series.Add(series3);
            lastPredictionsChart.Size = new Size(454, 148);
            lastPredictionsChart.TabIndex = 16;
            lastPredictionsChart.Text = "chart1";
            // 
            // LoadBestNetworkBut
            // 
            LoadBestNetworkBut.Location = new Point(432, 450);
            LoadBestNetworkBut.Name = "LoadBestNetworkBut";
            LoadBestNetworkBut.Size = new Size(52, 115);
            LoadBestNetworkBut.TabIndex = 17;
            LoadBestNetworkBut.Text = "Load best trained net.";
            LoadBestNetworkBut.UseVisualStyleBackColor = true;
            LoadBestNetworkBut.Click += LoadBestNetworkBut_Click;
            // 
            // AI_TrainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(875, 669);
            Controls.Add(LoadBestNetworkBut);
            Controls.Add(lastPredictionsChart);
            Controls.Add(TestErrorsChart);
            Controls.Add(learningSettings);
            Controls.Add(richTextBox1);
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
            ((System.ComponentModel.ISupportInitialize)lastPredictionsChart).EndInit();
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
        private RichTextBox richTextBox1;
        private Button learningSettings;
        private System.Windows.Forms.DataVisualization.Charting.Chart TestErrorsChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart lastPredictionsChart;
        private Button LoadBestNetworkBut;
    }
}