namespace CryptoAI_Upgraded.DataAnalasys
{
    partial class MultiSeriesChartForm
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
            DataChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            YZoomUp = new Button();
            YZoomDown = new Button();
            XZoomUp = new Button();
            XZoomDown = new Button();
            ((System.ComponentModel.ISupportInitialize)DataChart).BeginInit();
            SuspendLayout();
            // 
            // DataChart
            // 
            DataChart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartArea1.Name = "MainArea";
            DataChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            DataChart.Legends.Add(legend1);
            DataChart.Location = new Point(12, 12);
            DataChart.Name = "DataChart";
            series1.ChartArea = "MainArea";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            DataChart.Series.Add(series1);
            DataChart.Size = new Size(631, 410);
            DataChart.TabIndex = 0;
            DataChart.Text = "chart1";
            // 
            // YZoomUp
            // 
            YZoomUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            YZoomUp.Location = new Point(649, 12);
            YZoomUp.Name = "YZoomUp";
            YZoomUp.Size = new Size(148, 23);
            YZoomUp.TabIndex = 1;
            YZoomUp.Text = "Y zoom up";
            YZoomUp.UseVisualStyleBackColor = true;
            YZoomUp.Click += YZoomUp_Click;
            // 
            // YZoomDown
            // 
            YZoomDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            YZoomDown.Location = new Point(649, 41);
            YZoomDown.Name = "YZoomDown";
            YZoomDown.Size = new Size(148, 23);
            YZoomDown.TabIndex = 2;
            YZoomDown.Text = "Y zoom down";
            YZoomDown.UseVisualStyleBackColor = true;
            YZoomDown.Click += YZoomDown_Click;
            // 
            // XZoomUp
            // 
            XZoomUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            XZoomUp.Location = new Point(649, 70);
            XZoomUp.Name = "XZoomUp";
            XZoomUp.Size = new Size(148, 23);
            XZoomUp.TabIndex = 3;
            XZoomUp.Text = "X zoom up";
            XZoomUp.UseVisualStyleBackColor = true;
            XZoomUp.Click += XZoomUp_Click;
            // 
            // XZoomDown
            // 
            XZoomDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            XZoomDown.Location = new Point(649, 99);
            XZoomDown.Name = "XZoomDown";
            XZoomDown.Size = new Size(148, 23);
            XZoomDown.TabIndex = 4;
            XZoomDown.Text = "X zoom down";
            XZoomDown.UseVisualStyleBackColor = true;
            XZoomDown.Click += XZoomDown_Click;
            // 
            // MultiSeriesChartForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 435);
            Controls.Add(XZoomDown);
            Controls.Add(XZoomUp);
            Controls.Add(YZoomDown);
            Controls.Add(YZoomUp);
            Controls.Add(DataChart);
            Name = "MultiSeriesChartForm";
            Text = "MultiSeriesChartForm";
            ((System.ComponentModel.ISupportInitialize)DataChart).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart DataChart;
        private Button YZoomUp;
        private Button YZoomDown;
        private Button XZoomUp;
        private Button XZoomDown;
    }
}