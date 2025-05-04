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
            displayDataBut = new Button();
            LoadLocalDatasetsBut = new Button();
            DisplayGraphics = new Button();
            AnalyzeCourseChangeBut = new Button();
            trainAI_But = new Button();
            AIPredictorBut = new Button();
            NormalizeDatasetBut = new Button();
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
            // displayDataBut
            // 
            displayDataBut.Location = new Point(12, 403);
            displayDataBut.Name = "displayDataBut";
            displayDataBut.Size = new Size(140, 23);
            displayDataBut.TabIndex = 1;
            displayDataBut.Text = "display data";
            displayDataBut.UseVisualStyleBackColor = true;
            displayDataBut.Click += displayDataBut_Click;
            // 
            // LoadLocalDatasetsBut
            // 
            LoadLocalDatasetsBut.Location = new Point(633, 12);
            LoadLocalDatasetsBut.Name = "LoadLocalDatasetsBut";
            LoadLocalDatasetsBut.Size = new Size(133, 23);
            LoadLocalDatasetsBut.TabIndex = 3;
            LoadLocalDatasetsBut.Text = "Load local datasets";
            LoadLocalDatasetsBut.UseVisualStyleBackColor = true;
            LoadLocalDatasetsBut.Click += LoadLocalDatasetsBut_Click;
            // 
            // DisplayGraphics
            // 
            DisplayGraphics.Location = new Point(524, 12);
            DisplayGraphics.Name = "DisplayGraphics";
            DisplayGraphics.Size = new Size(103, 23);
            DisplayGraphics.TabIndex = 4;
            DisplayGraphics.Text = "DisplayGraphics";
            DisplayGraphics.UseVisualStyleBackColor = true;
            DisplayGraphics.Click += DisplayGraphics_Click;
            // 
            // AnalyzeCourseChangeBut
            // 
            AnalyzeCourseChangeBut.Location = new Point(370, 12);
            AnalyzeCourseChangeBut.Name = "AnalyzeCourseChangeBut";
            AnalyzeCourseChangeBut.Size = new Size(148, 23);
            AnalyzeCourseChangeBut.TabIndex = 5;
            AnalyzeCourseChangeBut.Text = "AnalizeCourseChange";
            AnalyzeCourseChangeBut.UseVisualStyleBackColor = true;
            AnalyzeCourseChangeBut.Click += AnalyzeCourseChangeBut_Click;
            // 
            // trainAI_But
            // 
            trainAI_But.Location = new Point(265, 12);
            trainAI_But.Name = "trainAI_But";
            trainAI_But.Size = new Size(99, 23);
            trainAI_But.TabIndex = 6;
            trainAI_But.Text = "Train AI";
            trainAI_But.UseVisualStyleBackColor = true;
            trainAI_But.Click += trainAI_But_Click;
            // 
            // AIPredictorBut
            // 
            AIPredictorBut.Location = new Point(265, 41);
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
            NormalizeDatasetBut.Text = "DatasetsNormalization";
            NormalizeDatasetBut.UseVisualStyleBackColor = true;
            NormalizeDatasetBut.Click += NormalizeDatasetBut_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(NormalizeDatasetBut);
            Controls.Add(AIPredictorBut);
            Controls.Add(trainAI_But);
            Controls.Add(AnalyzeCourseChangeBut);
            Controls.Add(DisplayGraphics);
            Controls.Add(LoadLocalDatasetsBut);
            Controls.Add(displayDataBut);
            Controls.Add(OpenLoadDataWindowBut);
            Name = "Form1";
            Text = "Form1";
            Shown += Form1_Shown;
            ResumeLayout(false);
        }

        #endregion

        private Button OpenLoadDataWindowBut;
        private Button displayDataBut;
        private Button LoadLocalDatasetsBut;
        private Button DisplayGraphics;
        private Button AnalyzeCourseChangeBut;
        private Button trainAI_But;
        private Button AIPredictorBut;
        private Button NormalizeDatasetBut;
    }
}
