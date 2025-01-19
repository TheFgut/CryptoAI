namespace CryptoAI_Upgraded.DatasetsAnalasys
{
    partial class DatasetCourseChangeAnalysis
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
            AnalyzeBut = new Button();
            TargetProfitPercentText = new TextBox();
            label1 = new Label();
            label2 = new Label();
            TargetPredictionIntervalsText = new TextBox();
            AnaliseResultDisp = new RichTextBox();
            AnalizeProgressBar = new ProgressBar();
            SuspendLayout();
            // 
            // AnalyzeBut
            // 
            AnalyzeBut.Location = new Point(12, 415);
            AnalyzeBut.Name = "AnalyzeBut";
            AnalyzeBut.Size = new Size(236, 23);
            AnalyzeBut.TabIndex = 0;
            AnalyzeBut.Text = "Analize";
            AnalyzeBut.UseVisualStyleBackColor = true;
            AnalyzeBut.Click += AnalyzeBut_Click;
            // 
            // TargetProfitPercentText
            // 
            TargetProfitPercentText.Location = new Point(180, 24);
            TargetProfitPercentText.Name = "TargetProfitPercentText";
            TargetProfitPercentText.Size = new Size(100, 23);
            TargetProfitPercentText.TabIndex = 1;
            TargetProfitPercentText.Text = "0.5";
            TargetProfitPercentText.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 27);
            label1.Name = "label1";
            label1.Size = new Size(114, 15);
            label1.TabIndex = 2;
            label1.Text = "Target profit percent";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 59);
            label2.Name = "label2";
            label2.Size = new Size(146, 15);
            label2.TabIndex = 3;
            label2.Text = "Target prediction  intervals";
            // 
            // TargetPredictionIntervalsText
            // 
            TargetPredictionIntervalsText.Location = new Point(180, 59);
            TargetPredictionIntervalsText.Name = "TargetPredictionIntervalsText";
            TargetPredictionIntervalsText.Size = new Size(100, 23);
            TargetPredictionIntervalsText.TabIndex = 4;
            TargetPredictionIntervalsText.Text = "5";
            TargetPredictionIntervalsText.TextAlign = HorizontalAlignment.Center;
            // 
            // AnaliseResultDisp
            // 
            AnaliseResultDisp.Location = new Point(299, 12);
            AnaliseResultDisp.Name = "AnaliseResultDisp";
            AnaliseResultDisp.Size = new Size(489, 396);
            AnaliseResultDisp.TabIndex = 5;
            AnaliseResultDisp.Text = "";
            // 
            // AnalizeProgressBar
            // 
            AnalizeProgressBar.Location = new Point(299, 415);
            AnalizeProgressBar.Name = "AnalizeProgressBar";
            AnalizeProgressBar.Size = new Size(489, 23);
            AnalizeProgressBar.TabIndex = 6;
            // 
            // DatasetCourseChangeAnalysis
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(AnalizeProgressBar);
            Controls.Add(AnaliseResultDisp);
            Controls.Add(TargetPredictionIntervalsText);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(TargetProfitPercentText);
            Controls.Add(AnalyzeBut);
            Name = "DatasetCourseChangeAnalysis";
            Text = "DatasetCourseChangeAnalysis";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button AnalyzeBut;
        private TextBox TargetProfitPercentText;
        private Label label1;
        private Label label2;
        private TextBox TargetPredictionIntervalsText;
        private RichTextBox AnaliseResultDisp;
        private ProgressBar AnalizeProgressBar;
    }
}