namespace CryptoAI_Upgraded.Datasets.NromalizationAndConvertion
{
    partial class DatasetConvertorAndNormalizerWindow
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
            datasetsManagerPanel1 = new DatasetsManaging.UI.DatasetsManagerPanel();
            savePathBox = new TextBox();
            ChoosePathBut = new Button();
            NormalizeDatasetBut = new Button();
            SuspendLayout();
            // 
            // datasetsManagerPanel1
            // 
            datasetsManagerPanel1.BackColor = SystemColors.ControlDark;
            datasetsManagerPanel1.Location = new Point(14, 41);
            datasetsManagerPanel1.Name = "datasetsManagerPanel1";
            datasetsManagerPanel1.onDataChanged = null;
            datasetsManagerPanel1.Size = new Size(218, 152);
            datasetsManagerPanel1.TabIndex = 0;
            // 
            // savePathBox
            // 
            savePathBox.Location = new Point(238, 12);
            savePathBox.Name = "savePathBox";
            savePathBox.ReadOnly = true;
            savePathBox.Size = new Size(339, 23);
            savePathBox.TabIndex = 1;
            savePathBox.Text = "none";
            // 
            // ChoosePathBut
            // 
            ChoosePathBut.Location = new Point(12, 12);
            ChoosePathBut.Name = "ChoosePathBut";
            ChoosePathBut.Size = new Size(220, 23);
            ChoosePathBut.TabIndex = 2;
            ChoosePathBut.Text = "Choose save path";
            ChoosePathBut.UseVisualStyleBackColor = true;
            ChoosePathBut.Click += ChoosePathBut_Click;
            // 
            // NormalizeDatasetBut
            // 
            NormalizeDatasetBut.Location = new Point(12, 218);
            NormalizeDatasetBut.Name = "NormalizeDatasetBut";
            NormalizeDatasetBut.Size = new Size(563, 23);
            NormalizeDatasetBut.TabIndex = 3;
            NormalizeDatasetBut.Text = "Normalize";
            NormalizeDatasetBut.UseVisualStyleBackColor = true;
            NormalizeDatasetBut.Click += NormalizeDatasetBut_Click;
            // 
            // DatasetConvertorAndNormalizerWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(588, 258);
            Controls.Add(NormalizeDatasetBut);
            Controls.Add(ChoosePathBut);
            Controls.Add(savePathBox);
            Controls.Add(datasetsManagerPanel1);
            Name = "DatasetConvertorAndNormalizerWindow";
            Text = "DatasetConvertorAndNormalizerWindow";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DatasetsManaging.UI.DatasetsManagerPanel datasetsManagerPanel1;
        private TextBox savePathBox;
        private Button ChoosePathBut;
        private Button NormalizeDatasetBut;
    }
}