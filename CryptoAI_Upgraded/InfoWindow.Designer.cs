namespace CryptoAI_Upgraded
{
    partial class InfoWindow
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
            infoTextBox = new RichTextBox();
            SuspendLayout();
            // 
            // infoTextBox
            // 
            infoTextBox.Location = new Point(12, 12);
            infoTextBox.Name = "infoTextBox";
            infoTextBox.Size = new Size(776, 426);
            infoTextBox.TabIndex = 0;
            infoTextBox.Text = "";
            // 
            // InfoWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(infoTextBox);
            Name = "InfoWindow";
            Text = "InfoWindow";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox infoTextBox;
    }
}