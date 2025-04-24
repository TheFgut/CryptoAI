namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks.UI
{
    partial class ModelDetailsWindow
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
            DetailsTextBox = new RichTextBox();
            networkLearningStatsPanel = new NetworkLearningStatsPanel();
            SuspendLayout();
            // 
            // DetailsTextBox
            // 
            DetailsTextBox.Location = new Point(12, 12);
            DetailsTextBox.Name = "DetailsTextBox";
            DetailsTextBox.Size = new Size(371, 262);
            DetailsTextBox.TabIndex = 0;
            DetailsTextBox.Text = "";
            // 
            // networkLearningStatsPanel
            // 
            networkLearningStatsPanel.BackColor = SystemColors.AppWorkspace;
            networkLearningStatsPanel.Location = new Point(389, 12);
            networkLearningStatsPanel.Name = "networkLearningStatsPanel";
            networkLearningStatsPanel.Size = new Size(278, 262);
            networkLearningStatsPanel.TabIndex = 1;
            // 
            // ModelDetailsWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(674, 286);
            Controls.Add(networkLearningStatsPanel);
            Controls.Add(DetailsTextBox);
            Name = "ModelDetailsWindow";
            Text = "ModelDetailsWindow";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox DetailsTextBox;
        private NetworkLearningStatsPanel networkLearningStatsPanel;
    }
}