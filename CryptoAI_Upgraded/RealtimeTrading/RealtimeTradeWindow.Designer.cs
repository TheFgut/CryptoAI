namespace CryptoAI_Upgraded.RealtimeTrading
{
    partial class RealtimeTradeWindow
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
            OpenConnectionBut = new Button();
            ResultText = new TextBox();
            Graphic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)Graphic).BeginInit();
            SuspendLayout();
            // 
            // OpenConnectionBut
            // 
            OpenConnectionBut.Location = new Point(12, 415);
            OpenConnectionBut.Name = "OpenConnectionBut";
            OpenConnectionBut.Size = new Size(131, 23);
            OpenConnectionBut.TabIndex = 0;
            OpenConnectionBut.Text = "Open connection";
            OpenConnectionBut.UseVisualStyleBackColor = true;
            OpenConnectionBut.Click += OpenConnectionBut_Click;
            // 
            // ResultText
            // 
            ResultText.Enabled = false;
            ResultText.Location = new Point(12, 12);
            ResultText.Name = "ResultText";
            ResultText.Size = new Size(100, 23);
            ResultText.TabIndex = 1;
            // 
            // Graphic
            // 
            chartArea1.Name = "ChartArea1";
            Graphic.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            Graphic.Legends.Add(legend1);
            Graphic.Location = new Point(12, 41);
            Graphic.Name = "Graphic";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            Graphic.Series.Add(series1);
            Graphic.Size = new Size(776, 300);
            Graphic.TabIndex = 2;
            Graphic.Text = "chart1";
            // 
            // RealtimeTradeWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Graphic);
            Controls.Add(ResultText);
            Controls.Add(OpenConnectionBut);
            Name = "RealtimeTradeWindow";
            Text = "RealtimeTradeWindow";
            FormClosing += RealtimeTradeWindow_FormClosing;
            ((System.ComponentModel.ISupportInitialize)Graphic).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button OpenConnectionBut;
        private TextBox ResultText;
        private System.Windows.Forms.DataVisualization.Charting.Chart Graphic;
    }
}