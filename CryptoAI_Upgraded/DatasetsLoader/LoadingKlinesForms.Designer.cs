namespace CryptoAI_Upgraded
{
    partial class LoadingKlinesForms
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
            LoadBut = new Button();
            display = new RichTextBox();
            fromDate = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            toDate = new DateTimePicker();
            TimeIntervalBox = new ComboBox();
            PairComboBox = new ComboBox();
            SuspendLayout();
            // 
            // LoadBut
            // 
            LoadBut.Location = new Point(22, 246);
            LoadBut.Name = "LoadBut";
            LoadBut.Size = new Size(200, 23);
            LoadBut.TabIndex = 0;
            LoadBut.Text = "Load";
            LoadBut.UseVisualStyleBackColor = true;
            LoadBut.Click += LoadBut_Click;
            // 
            // display
            // 
            display.Location = new Point(243, 42);
            display.Name = "display";
            display.Size = new Size(224, 236);
            display.TabIndex = 1;
            display.Text = "";
            // 
            // fromDate
            // 
            fromDate.Location = new Point(22, 46);
            fromDate.Name = "fromDate";
            fromDate.Size = new Size(200, 23);
            fromDate.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 28);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 5;
            label1.Text = "From";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 72);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 6;
            label2.Text = "To";
            // 
            // toDate
            // 
            toDate.Location = new Point(22, 100);
            toDate.Name = "toDate";
            toDate.Size = new Size(200, 23);
            toDate.TabIndex = 7;
            // 
            // TimeIntervalBox
            // 
            TimeIntervalBox.FormattingEnabled = true;
            TimeIntervalBox.Location = new Point(22, 143);
            TimeIntervalBox.Name = "TimeIntervalBox";
            TimeIntervalBox.Size = new Size(200, 23);
            TimeIntervalBox.TabIndex = 8;
            // 
            // PairComboBox
            // 
            PairComboBox.FormattingEnabled = true;
            PairComboBox.Location = new Point(22, 187);
            PairComboBox.Name = "PairComboBox";
            PairComboBox.Size = new Size(200, 23);
            PairComboBox.TabIndex = 9;
            // 
            // LoadingKlinesForms
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(479, 290);
            Controls.Add(PairComboBox);
            Controls.Add(TimeIntervalBox);
            Controls.Add(toDate);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(fromDate);
            Controls.Add(display);
            Controls.Add(LoadBut);
            Name = "LoadingKlinesForms";
            Text = "LoadingKlinesForms";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button LoadBut;
        private RichTextBox display;
        private DateTimePicker fromDate;
        private Label label1;
        private Label label2;
        private DateTimePicker toDate;
        private ComboBox TimeIntervalBox;
        private ComboBox PairComboBox;
    }
}