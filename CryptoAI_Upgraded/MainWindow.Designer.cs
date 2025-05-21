namespace CryptoAI_Upgraded
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            OpenLoadDataWindowBut = new Button();
            AnalyzeCourseChangeBut = new Button();
            trainAI_But = new Button();
            AIPredictorBut = new Button();
            NormalizeDatasetBut = new Button();
            CloudServiceBut = new Button();
            RealtimeTradingWindow = new Button();
            SuspendLayout();
            // 
            // OpenLoadDataWindowBut
            // 
            OpenLoadDataWindowBut.Location = new Point(12, 12);
            OpenLoadDataWindowBut.Name = "OpenLoadDataWindowBut";
            OpenLoadDataWindowBut.Size = new Size(140, 23);
            OpenLoadDataWindowBut.TabIndex = 0;
            OpenLoadDataWindowBut.Text = "Load data";
            OpenLoadDataWindowBut.UseVisualStyleBackColor = true;
            OpenLoadDataWindowBut.Click += OpenLoadDataWindowBut_Click;
            // 
            // AnalyzeCourseChangeBut
            // 
            AnalyzeCourseChangeBut.Location = new Point(263, 12);
            AnalyzeCourseChangeBut.Name = "AnalyzeCourseChangeBut";
            AnalyzeCourseChangeBut.Size = new Size(148, 23);
            AnalyzeCourseChangeBut.TabIndex = 5;
            AnalyzeCourseChangeBut.Text = "Analize network";
            AnalyzeCourseChangeBut.UseVisualStyleBackColor = true;
            AnalyzeCourseChangeBut.Click += AnalyzeCourseChangeBut_Click;
            // 
            // trainAI_But
            // 
            trainAI_But.Location = new Point(158, 12);
            trainAI_But.Name = "trainAI_But";
            trainAI_But.Size = new Size(99, 23);
            trainAI_But.TabIndex = 6;
            trainAI_But.Text = "Train AI";
            trainAI_But.UseVisualStyleBackColor = true;
            trainAI_But.Click += trainAI_But_Click;
            // 
            // AIPredictorBut
            // 
            AIPredictorBut.Location = new Point(158, 41);
            AIPredictorBut.Name = "AIPredictorBut";
            AIPredictorBut.Size = new Size(99, 23);
            AIPredictorBut.TabIndex = 7;
            AIPredictorBut.Text = "AI predictor";
            AIPredictorBut.UseVisualStyleBackColor = true;
            AIPredictorBut.Click += AIPredictorBut_Click;
            // 
            // NormalizeDatasetBut
            // 
            NormalizeDatasetBut.Location = new Point(12, 41);
            NormalizeDatasetBut.Name = "NormalizeDatasetBut";
            NormalizeDatasetBut.Size = new Size(140, 23);
            NormalizeDatasetBut.TabIndex = 8;
            NormalizeDatasetBut.Text = "Datasets normalizer";
            NormalizeDatasetBut.UseVisualStyleBackColor = true;
            NormalizeDatasetBut.Click += NormalizeDatasetBut_Click;
            // 
            // CloudServiceBut
            // 
            CloudServiceBut.Location = new Point(263, 41);
            CloudServiceBut.Name = "CloudServiceBut";
            CloudServiceBut.Size = new Size(148, 23);
            CloudServiceBut.TabIndex = 9;
            CloudServiceBut.Text = "Cloud service";
            CloudServiceBut.UseVisualStyleBackColor = true;
            CloudServiceBut.Click += CloudServiceBut_Click;
            // 
            // RealtimeTradingWindow
            // 
            RealtimeTradingWindow.Location = new Point(158, 70);
            RealtimeTradingWindow.Name = "RealtimeTradingWindow";
            RealtimeTradingWindow.Size = new Size(99, 23);
            RealtimeTradingWindow.TabIndex = 10;
            RealtimeTradingWindow.Text = "RealtimeTrading";
            RealtimeTradingWindow.UseVisualStyleBackColor = true;
            RealtimeTradingWindow.Click += RealtimeTradingWindow_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 94);
            Controls.Add(RealtimeTradingWindow);
            Controls.Add(CloudServiceBut);
            Controls.Add(NormalizeDatasetBut);
            Controls.Add(AIPredictorBut);
            Controls.Add(trainAI_But);
            Controls.Add(AnalyzeCourseChangeBut);
            Controls.Add(OpenLoadDataWindowBut);
            Name = "MainWindow";
            Text = "CryptoAnalizer";
            Shown += Form1_Shown;
            ResumeLayout(false);
        }

        #endregion

        private Button OpenLoadDataWindowBut;
        private Button AnalyzeCourseChangeBut;
        private Button trainAI_But;
        private Button AIPredictorBut;
        private Button NormalizeDatasetBut;
        private Button CloudServiceBut;
        private Button RealtimeTradingWindow;
    }
}
