namespace CryptoAI_Upgraded.AI_Prediction
{
    partial class AIPredictorForm
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
            networkManagePanel1 = new AI_Training.NeuralNetworks.UI.NetworkManagePanel();
            datasetsManagerPanel1 = new DatasetsManaging.UI.DatasetsManagerPanel();
            aiPredictionsDatasetWalkerPanel1 = new AIPredictionsDatasetWalkerPanel();
            SuspendLayout();
            // 
            // networkManagePanel1
            // 
            networkManagePanel1.BackColor = SystemColors.ControlDark;
            networkManagePanel1.Location = new Point(680, 12);
            networkManagePanel1.Name = "networkManagePanel1";
            networkManagePanel1.onNetworkChanges = null;
            networkManagePanel1.Size = new Size(278, 300);
            networkManagePanel1.TabIndex = 0;
            // 
            // datasetsManagerPanel1
            // 
            datasetsManagerPanel1.BackColor = SystemColors.ControlDark;
            datasetsManagerPanel1.Location = new Point(680, 318);
            datasetsManagerPanel1.Name = "datasetsManagerPanel1";
            datasetsManagerPanel1.Size = new Size(278, 152);
            datasetsManagerPanel1.TabIndex = 1;
            // 
            // aiPredictionsDatasetWalkerPanel1
            // 
            aiPredictionsDatasetWalkerPanel1.Location = new Point(12, 12);
            aiPredictionsDatasetWalkerPanel1.Name = "aiPredictionsDatasetWalkerPanel1";
            aiPredictionsDatasetWalkerPanel1.Size = new Size(662, 458);
            aiPredictionsDatasetWalkerPanel1.TabIndex = 2;
            // 
            // AIPredictorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(964, 483);
            Controls.Add(aiPredictionsDatasetWalkerPanel1);
            Controls.Add(datasetsManagerPanel1);
            Controls.Add(networkManagePanel1);
            Name = "AIPredictorForm";
            Text = "AIPredictorForm";
            ResumeLayout(false);
        }

        #endregion

        private AI_Training.NeuralNetworks.UI.NetworkManagePanel networkManagePanel1;
        private DatasetsManaging.UI.DatasetsManagerPanel datasetsManagerPanel1;
        private AIPredictionsDatasetWalkerPanel aiPredictionsDatasetWalkerPanel1;
    }
}