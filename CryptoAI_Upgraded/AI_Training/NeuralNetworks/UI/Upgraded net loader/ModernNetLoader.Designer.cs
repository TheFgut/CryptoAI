namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks.UI.Upgraded_net_loader
{
    partial class ModernNetLoader
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
            AwailableNetSGrid = new DataGridView();
            ReloadBut = new Button();
            ((System.ComponentModel.ISupportInitialize)AwailableNetSGrid).BeginInit();
            SuspendLayout();
            // 
            // AwailableNetSGrid
            // 
            AwailableNetSGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AwailableNetSGrid.Location = new Point(12, 12);
            AwailableNetSGrid.Name = "AwailableNetSGrid";
            AwailableNetSGrid.Size = new Size(438, 400);
            AwailableNetSGrid.TabIndex = 0;
            // 
            // ReloadBut
            // 
            ReloadBut.Location = new Point(12, 418);
            ReloadBut.Name = "ReloadBut";
            ReloadBut.Size = new Size(75, 23);
            ReloadBut.TabIndex = 1;
            ReloadBut.Text = "Reload";
            ReloadBut.UseVisualStyleBackColor = true;
            ReloadBut.Click += ReloadBut_Click;
            // 
            // ModernNetLoader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ReloadBut);
            Controls.Add(AwailableNetSGrid);
            Name = "ModernNetLoader";
            Text = "ModernNetLoader";
            ((System.ComponentModel.ISupportInitialize)AwailableNetSGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView AwailableNetSGrid;
        private Button ReloadBut;
    }
}