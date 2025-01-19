namespace CryptoAI_Upgraded.AI_Training.NeuralNetworkCreating
{
    partial class NeuralNetworkCreatorWindow
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
            LayersGrid = new DataGridView();
            AddLayerBut = new Button();
            RemoveLAyersBut = new Button();
            CreateNetworkBut = new Button();
            ((System.ComponentModel.ISupportInitialize)LayersGrid).BeginInit();
            SuspendLayout();
            // 
            // LayersGrid
            // 
            LayersGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LayersGrid.Location = new Point(12, 12);
            LayersGrid.Name = "LayersGrid";
            LayersGrid.Size = new Size(600, 342);
            LayersGrid.TabIndex = 0;
            // 
            // AddLayerBut
            // 
            AddLayerBut.Location = new Point(12, 360);
            AddLayerBut.Name = "AddLayerBut";
            AddLayerBut.Size = new Size(388, 23);
            AddLayerBut.TabIndex = 1;
            AddLayerBut.Text = "Add layer";
            AddLayerBut.UseVisualStyleBackColor = true;
            AddLayerBut.Click += AddLayerBut_Click;
            // 
            // RemoveLAyersBut
            // 
            RemoveLAyersBut.Location = new Point(406, 360);
            RemoveLAyersBut.Name = "RemoveLAyersBut";
            RemoveLAyersBut.Size = new Size(206, 23);
            RemoveLAyersBut.TabIndex = 2;
            RemoveLAyersBut.Text = "Remove layers";
            RemoveLAyersBut.UseVisualStyleBackColor = true;
            RemoveLAyersBut.Click += RemoveLayersBut_Click;
            // 
            // CreateNetworkBut
            // 
            CreateNetworkBut.Location = new Point(12, 405);
            CreateNetworkBut.Name = "CreateNetworkBut";
            CreateNetworkBut.Size = new Size(600, 23);
            CreateNetworkBut.TabIndex = 3;
            CreateNetworkBut.Text = "Create network";
            CreateNetworkBut.UseVisualStyleBackColor = true;
            CreateNetworkBut.Click += CreateNetworkBut_Click;
            // 
            // NeuralNetworkCreatorWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(628, 450);
            Controls.Add(CreateNetworkBut);
            Controls.Add(RemoveLAyersBut);
            Controls.Add(AddLayerBut);
            Controls.Add(LayersGrid);
            Name = "NeuralNetworkCreatorWindow";
            Text = "NeuralNetworkCreatorWindow";
            ((System.ComponentModel.ISupportInitialize)LayersGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView LayersGrid;
        private Button AddLayerBut;
        private Button RemoveLAyersBut;
        private Button CreateNetworkBut;
    }
}