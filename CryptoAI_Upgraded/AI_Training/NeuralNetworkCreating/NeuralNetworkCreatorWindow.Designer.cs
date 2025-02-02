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
            InputLabel = new Label();
            CourseCheckBox = new CheckBox();
            checkBox1 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)LayersGrid).BeginInit();
            SuspendLayout();
            // 
            // LayersGrid
            // 
            LayersGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LayersGrid.Location = new Point(12, 12);
            LayersGrid.Name = "LayersGrid";
            LayersGrid.Size = new Size(485, 342);
            LayersGrid.TabIndex = 0;
            // 
            // AddLayerBut
            // 
            AddLayerBut.Location = new Point(12, 360);
            AddLayerBut.Name = "AddLayerBut";
            AddLayerBut.Size = new Size(273, 23);
            AddLayerBut.TabIndex = 1;
            AddLayerBut.Text = "Add layer";
            AddLayerBut.UseVisualStyleBackColor = true;
            AddLayerBut.Click += AddLayerBut_Click;
            // 
            // RemoveLAyersBut
            // 
            RemoveLAyersBut.Location = new Point(291, 360);
            RemoveLAyersBut.Name = "RemoveLAyersBut";
            RemoveLAyersBut.Size = new Size(206, 23);
            RemoveLAyersBut.TabIndex = 2;
            RemoveLAyersBut.Text = "Remove layers";
            RemoveLAyersBut.UseVisualStyleBackColor = true;
            RemoveLAyersBut.Click += RemoveLayersBut_Click;
            // 
            // CreateNetworkBut
            // 
            CreateNetworkBut.Location = new Point(12, 402);
            CreateNetworkBut.Name = "CreateNetworkBut";
            CreateNetworkBut.Size = new Size(485, 23);
            CreateNetworkBut.TabIndex = 3;
            CreateNetworkBut.Text = "Create network";
            CreateNetworkBut.UseVisualStyleBackColor = true;
            CreateNetworkBut.Click += CreateNetworkBut_Click;
            // 
            // InputLabel
            // 
            InputLabel.AutoSize = true;
            InputLabel.Font = new Font("Segoe UI", 19F);
            InputLabel.Location = new Point(574, 12);
            InputLabel.Name = "InputLabel";
            InputLabel.Size = new Size(76, 36);
            InputLabel.TabIndex = 4;
            InputLabel.Text = "Input";
            // 
            // CourseCheckBox
            // 
            CourseCheckBox.AutoCheck = false;
            CourseCheckBox.AutoSize = true;
            CourseCheckBox.Checked = true;
            CourseCheckBox.CheckState = CheckState.Checked;
            CourseCheckBox.Font = new Font("Segoe UI", 14F);
            CourseCheckBox.Location = new Point(533, 68);
            CourseCheckBox.Name = "CourseCheckBox";
            CourseCheckBox.Size = new Size(87, 29);
            CourseCheckBox.TabIndex = 5;
            CourseCheckBox.Text = "course";
            CourseCheckBox.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 14F);
            checkBox1.Location = new Point(533, 103);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(120, 29);
            checkBox1.TabIndex = 6;
            checkBox1.Text = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // NeuralNetworkCreatorWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(723, 450);
            Controls.Add(checkBox1);
            Controls.Add(CourseCheckBox);
            Controls.Add(InputLabel);
            Controls.Add(CreateNetworkBut);
            Controls.Add(RemoveLAyersBut);
            Controls.Add(AddLayerBut);
            Controls.Add(LayersGrid);
            Name = "NeuralNetworkCreatorWindow";
            Text = "NeuralNetworkCreatorWindow";
            ((System.ComponentModel.ISupportInitialize)LayersGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView LayersGrid;
        private Button AddLayerBut;
        private Button RemoveLAyersBut;
        private Button CreateNetworkBut;
        private Label InputLabel;
        private CheckBox CourseCheckBox;
        private CheckBox checkBox1;
    }
}