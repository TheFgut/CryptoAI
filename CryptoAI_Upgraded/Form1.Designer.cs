namespace CryptoAI_Upgraded
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            OpenLoadDataWindowBut = new Button();
            displayDataBut = new Button();
            courseGraphic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            LoadLocalDatasetsBut = new Button();
            DisplayGraphics = new Button();
            AnalyzeCourseChangeBut = new Button();
            trainAI_But = new Button();
            ((System.ComponentModel.ISupportInitialize)courseGraphic).BeginInit();
            SuspendLayout();
            // 
            // OpenLoadDataWindowBut
            // 
            OpenLoadDataWindowBut.Location = new Point(12, 12);
            OpenLoadDataWindowBut.Name = "OpenLoadDataWindowBut";
            OpenLoadDataWindowBut.Size = new Size(75, 23);
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
            // courseGraphic
            // 
            chartArea1.Name = "ChartArea1";
            courseGraphic.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            courseGraphic.Legends.Add(legend1);
            courseGraphic.Location = new Point(12, 52);
            courseGraphic.Name = "courseGraphic";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            courseGraphic.Series.Add(series1);
            courseGraphic.Size = new Size(776, 345);
            courseGraphic.TabIndex = 2;
            courseGraphic.Text = "chart1";
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(trainAI_But);
            Controls.Add(AnalyzeCourseChangeBut);
            Controls.Add(DisplayGraphics);
            Controls.Add(LoadLocalDatasetsBut);
            Controls.Add(courseGraphic);
            Controls.Add(displayDataBut);
            Controls.Add(OpenLoadDataWindowBut);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)courseGraphic).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button OpenLoadDataWindowBut;
        private Button displayDataBut;
        private System.Windows.Forms.DataVisualization.Charting.Chart courseGraphic;
        private Button LoadLocalDatasetsBut;
        private Button DisplayGraphics;
        private Button AnalyzeCourseChangeBut;
        private Button trainAI_But;
    }
}
