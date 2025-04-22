namespace CryptoAI_Upgraded.AI_Prediction
{
    public partial class AIPredictionsDatasetWalkerPanel
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            details = new RichTextBox();
            GoRightBut = new Button();
            GoLeftBut = new Button();
            predictionsCountBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(3, 3);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(656, 300);
            chart1.TabIndex = 0;
            chart1.Text = "chart1";
            // 
            // details
            // 
            details.Location = new Point(18, 337);
            details.Name = "details";
            details.Size = new Size(628, 68);
            details.TabIndex = 1;
            details.Text = "";
            // 
            // GoRightBut
            // 
            GoRightBut.Location = new Point(546, 309);
            GoRightBut.Name = "GoRightBut";
            GoRightBut.Size = new Size(75, 23);
            GoRightBut.TabIndex = 2;
            GoRightBut.Text = "-->";
            GoRightBut.UseVisualStyleBackColor = true;
            GoRightBut.Click += GoRightBut_Click;
            // 
            // GoLeftBut
            // 
            GoLeftBut.Location = new Point(465, 309);
            GoLeftBut.Name = "GoLeftBut";
            GoLeftBut.Size = new Size(75, 23);
            GoLeftBut.TabIndex = 3;
            GoLeftBut.Text = "<--";
            GoLeftBut.UseVisualStyleBackColor = true;
            GoLeftBut.Click += GoLeftBut_Click;
            // 
            // predictionsCountBox
            // 
            predictionsCountBox.Location = new Point(18, 310);
            predictionsCountBox.Name = "predictionsCountBox";
            predictionsCountBox.Size = new Size(100, 23);
            predictionsCountBox.TabIndex = 4;
            predictionsCountBox.Text = "4";
            // 
            // AIPredictionsDatasetWalkerPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(predictionsCountBox);
            Controls.Add(GoLeftBut);
            Controls.Add(GoRightBut);
            Controls.Add(details);
            Controls.Add(chart1);
            Name = "AIPredictionsDatasetWalkerPanel";
            Size = new Size(662, 429);
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private RichTextBox details;
        private Button GoRightBut;
        private Button GoLeftBut;
        private TextBox predictionsCountBox;
    }
}
