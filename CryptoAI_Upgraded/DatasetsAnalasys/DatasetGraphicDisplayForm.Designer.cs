namespace CryptoAI_Upgraded.DatasetsAnalasys
{
    partial class DatasetGraphicDisplayForm
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
            courseGraphic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            JumpLeftBut = new Button();
            JumpRightBut = new Button();
            GoLeftBut = new Button();
            GoRightBut = new Button();
            dataPagesDisp = new Label();
            ZoomInBut = new Button();
            ZoomOutBut = new Button();
            ((System.ComponentModel.ISupportInitialize)courseGraphic).BeginInit();
            SuspendLayout();
            // 
            // courseGraphic
            // 
            chartArea2.Name = "ChartArea1";
            courseGraphic.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            courseGraphic.Legends.Add(legend2);
            courseGraphic.Location = new Point(12, 12);
            courseGraphic.Name = "courseGraphic";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            courseGraphic.Series.Add(series2);
            courseGraphic.Size = new Size(732, 355);
            courseGraphic.TabIndex = 0;
            courseGraphic.Text = "chart1";
            // 
            // JumpLeftBut
            // 
            JumpLeftBut.Location = new Point(12, 397);
            JumpLeftBut.Name = "JumpLeftBut";
            JumpLeftBut.Size = new Size(120, 23);
            JumpLeftBut.TabIndex = 1;
            JumpLeftBut.Text = "<< Jump Left";
            JumpLeftBut.UseVisualStyleBackColor = true;
            JumpLeftBut.Click += JumpLeftBut_Click;
            // 
            // JumpRightBut
            // 
            JumpRightBut.Location = new Point(624, 397);
            JumpRightBut.Name = "JumpRightBut";
            JumpRightBut.Size = new Size(120, 23);
            JumpRightBut.TabIndex = 2;
            JumpRightBut.Text = "Jump Right >>";
            JumpRightBut.UseVisualStyleBackColor = true;
            JumpRightBut.Click += JumpRightBut_Click;
            // 
            // GoLeftBut
            // 
            GoLeftBut.Location = new Point(138, 397);
            GoLeftBut.Name = "GoLeftBut";
            GoLeftBut.Size = new Size(120, 23);
            GoLeftBut.TabIndex = 3;
            GoLeftBut.Text = "< Go Left";
            GoLeftBut.UseVisualStyleBackColor = true;
            GoLeftBut.Click += GoLeftBut_Click;
            // 
            // GoRightBut
            // 
            GoRightBut.Location = new Point(498, 397);
            GoRightBut.Name = "GoRightBut";
            GoRightBut.Size = new Size(120, 23);
            GoRightBut.TabIndex = 4;
            GoRightBut.Text = "Go Right >";
            GoRightBut.UseVisualStyleBackColor = true;
            GoRightBut.Click += GoRight_Click;
            // 
            // dataPagesDisp
            // 
            dataPagesDisp.AutoSize = true;
            dataPagesDisp.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            dataPagesDisp.Location = new Point(353, 390);
            dataPagesDisp.Name = "dataPagesDisp";
            dataPagesDisp.Size = new Size(43, 30);
            dataPagesDisp.TabIndex = 5;
            dataPagesDisp.Text = "0/0";
            // 
            // ZoomInBut
            // 
            ZoomInBut.Location = new Point(750, 89);
            ZoomInBut.Name = "ZoomInBut";
            ZoomInBut.Size = new Size(75, 23);
            ZoomInBut.TabIndex = 6;
            ZoomInBut.Text = "Zoom In";
            ZoomInBut.UseVisualStyleBackColor = true;
            ZoomInBut.Click += ZoomInBut_Click;
            // 
            // ZoomOutBut
            // 
            ZoomOutBut.Location = new Point(750, 118);
            ZoomOutBut.Name = "ZoomOutBut";
            ZoomOutBut.Size = new Size(75, 23);
            ZoomOutBut.TabIndex = 7;
            ZoomOutBut.Text = "Zoom out";
            ZoomOutBut.UseVisualStyleBackColor = true;
            ZoomOutBut.Click += ZoomOutBut_Click;
            // 
            // DatasetGraphicDisplayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(865, 450);
            Controls.Add(ZoomOutBut);
            Controls.Add(ZoomInBut);
            Controls.Add(dataPagesDisp);
            Controls.Add(GoRightBut);
            Controls.Add(GoLeftBut);
            Controls.Add(JumpRightBut);
            Controls.Add(JumpLeftBut);
            Controls.Add(courseGraphic);
            Name = "DatasetGraphicDisplayForm";
            Text = "DatasetGraphicDisplayForm";
            ((System.ComponentModel.ISupportInitialize)courseGraphic).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart courseGraphic;
        private Button JumpLeftBut;
        private Button JumpRightBut;
        private Button GoLeftBut;
        private Button GoRightBut;
        private Label dataPagesDisp;
        private Button ZoomInBut;
        private Button ZoomOutBut;
    }
}