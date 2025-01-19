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
            StartLearningBut = new Button();
            StopLearningBut = new Button();
            CreateNetworkBut = new Button();
            TrainingProgressBar = new ProgressBar();
            errorsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            rundCountBox = new TextBox();
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
            // CreateNetworkBut
            // 
            CreateNetworkBut.Location = new Point(586, 415);
            CreateNetworkBut.Name = "CreateNetworkBut";
            CreateNetworkBut.Size = new Size(202, 23);
            CreateNetworkBut.TabIndex = 2;
            CreateNetworkBut.Text = "Create network";
            CreateNetworkBut.UseVisualStyleBackColor = true;
            CreateNetworkBut.Click += CreateNetworkBut_Click;
            // 
            // TrainingProgressBar
            // 
            TrainingProgressBar.Location = new Point(12, 444);
            TrainingProgressBar.Name = "TrainingProgressBar";
            TrainingProgressBar.Size = new Size(454, 23);
            TrainingProgressBar.TabIndex = 3;
            // 
            // errorsChart
            // 
            chartArea1.Name = "ChartArea1";
            errorsChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            errorsChart.Legends.Add(legend1);
            errorsChart.Location = new Point(12, 12);
            errorsChart.Name = "errorsChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            errorsChart.Series.Add(series1);
            errorsChart.Size = new Size(454, 134);
            errorsChart.TabIndex = 4;
            errorsChart.Text = "chart1";
            // 
            // rundCountBox
            // 
            rundCountBox.Location = new Point(12, 386);
            rundCountBox.Name = "rundCountBox";
            rundCountBox.Size = new Size(156, 23);
            rundCountBox.TabIndex = 5;
            rundCountBox.Text = "100";
            // 
            // AI_TrainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 470);
            Controls.Add(rundCountBox);
            Controls.Add(errorsChart);
            Controls.Add(TrainingProgressBar);
            Controls.Add(CreateNetworkBut);
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
        private Button CreateNetworkBut;
        private ProgressBar TrainingProgressBar;
        private System.Windows.Forms.DataVisualization.Charting.Chart errorsChart;
        private TextBox rundCountBox;
    }
}