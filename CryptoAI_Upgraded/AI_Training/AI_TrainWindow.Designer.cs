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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            StartLearningBut = new Button();
            StopLearningBut = new Button();
            TrainingProgressBar = new ProgressBar();
            errorsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            runsCountBox = new TextBox();
            networkManagePanel1 = new NeuralNetworks.UI.NetworkManagePanel();
            trainingDatasetsManager = new DatasetsManaging.UI.DatasetsManagerPanel();
            trainingResultPanel = new RichTextBox();
            stopWhenErrorRaisesBox = new CheckBox();
            testingDatasetsManager = new DatasetsManaging.UI.DatasetsManagerPanel();
            TrainingETA = new Label();
            ((System.ComponentModel.ISupportInitialize)errorsChart).BeginInit();
            SuspendLayout();
            // 
            // StartLearningBut
            // 
            StartLearningBut.Location = new Point(12, 415);
            StartLearningBut.Name = "StartLearningBut";
            StartLearningBut.Size = new Size(75, 23);
            StartLearningBut.TabIndex = 0;
            StartLearningBut.Text = "Start";
            StartLearningBut.UseVisualStyleBackColor = true;
            StartLearningBut.Click += StartLearningBut_Click;
            // 
            // StopLearningBut
            // 
            StopLearningBut.Location = new Point(93, 415);
            StopLearningBut.Name = "StopLearningBut";
            StopLearningBut.Size = new Size(75, 23);
            StopLearningBut.TabIndex = 1;
            StopLearningBut.Text = "Stop";
            StopLearningBut.UseVisualStyleBackColor = true;
            StopLearningBut.Click += StopLearningBut_Click;
            // 
            // TrainingProgressBar
            // 
            TrainingProgressBar.Location = new Point(12, 444);
            TrainingProgressBar.Name = "TrainingProgressBar";
            TrainingProgressBar.Size = new Size(414, 23);
            TrainingProgressBar.TabIndex = 3;
            // 
            // errorsChart
            // 
            chartArea2.Name = "ChartArea1";
            errorsChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            errorsChart.Legends.Add(legend2);
            errorsChart.Location = new Point(12, 12);
            errorsChart.Name = "errorsChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            errorsChart.Series.Add(series2);
            errorsChart.Size = new Size(454, 134);
            errorsChart.TabIndex = 4;
            errorsChart.Text = "chart1";
            // 
            // runsCountBox
            // 
            runsCountBox.Location = new Point(12, 386);
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
            trainingResultPanel.Location = new Point(12, 152);
            trainingResultPanel.Name = "trainingResultPanel";
            trainingResultPanel.ReadOnly = true;
            trainingResultPanel.Size = new Size(454, 218);
            trainingResultPanel.TabIndex = 9;
            trainingResultPanel.Text = "";
            // 
            // stopWhenErrorRaisesBox
            // 
            stopWhenErrorRaisesBox.AutoSize = true;
            stopWhenErrorRaisesBox.Checked = true;
            stopWhenErrorRaisesBox.CheckState = CheckState.Checked;
            stopWhenErrorRaisesBox.Location = new Point(174, 390);
            stopWhenErrorRaisesBox.Name = "stopWhenErrorRaisesBox";
            stopWhenErrorRaisesBox.Size = new Size(147, 19);
            stopWhenErrorRaisesBox.TabIndex = 10;
            stopWhenErrorRaisesBox.Text = "stop when error raising";
            stopWhenErrorRaisesBox.UseVisualStyleBackColor = true;
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
            TrainingETA.Location = new Point(432, 446);
            TrainingETA.Name = "TrainingETA";
            TrainingETA.Size = new Size(63, 21);
            TrainingETA.TabIndex = 12;
            TrainingETA.Text = "ETA: 0.1";
            // 
            // AI_TrainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(871, 483);
            Controls.Add(TrainingETA);
            Controls.Add(testingDatasetsManager);
            Controls.Add(stopWhenErrorRaisesBox);
            Controls.Add(trainingResultPanel);
            Controls.Add(trainingDatasetsManager);
            Controls.Add(networkManagePanel1);
            Controls.Add(runsCountBox);
            Controls.Add(errorsChart);
            Controls.Add(TrainingProgressBar);
            Controls.Add(StopLearningBut);
            Controls.Add(StartLearningBut);
            Name = "AI_TrainWindow";
            Text = "AI_TrainWindow";
            ((System.ComponentModel.ISupportInitialize)errorsChart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartLearningBut;
        private Button StopLearningBut;
        private ProgressBar TrainingProgressBar;
        private System.Windows.Forms.DataVisualization.Charting.Chart errorsChart;
        private TextBox runsCountBox;
        private NeuralNetworks.UI.NetworkManagePanel networkManagePanel1;
        private DatasetsManaging.UI.DatasetsManagerPanel trainingDatasetsManager;
        private RichTextBox trainingResultPanel;
        private CheckBox stopWhenErrorRaisesBox;
        private DatasetsManaging.UI.DatasetsManagerPanel testingDatasetsManager;
        private Label TrainingETA;
    }
}