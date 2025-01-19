namespace CryptoAI_Upgraded.DataLocalChoosing
{
    partial class LoadLocalDatasetsForm
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
            LoadDatasetsBut = new Button();
            SelectedDatasetsDisp = new ListBox();
            RemoveSelectedElementBut = new Button();
            RemoveAllElementsBut = new Button();
            countDisp = new TextBox();
            SuspendLayout();
            // 
            // LoadDatasetsBut
            // 
            LoadDatasetsBut.Location = new Point(29, 391);
            LoadDatasetsBut.Name = "LoadDatasetsBut";
            LoadDatasetsBut.Size = new Size(264, 23);
            LoadDatasetsBut.TabIndex = 0;
            LoadDatasetsBut.Text = "Load";
            LoadDatasetsBut.UseVisualStyleBackColor = true;
            LoadDatasetsBut.Click += LoadDatasetsBut_Click;
            // 
            // SelectedDatasetsDisp
            // 
            SelectedDatasetsDisp.FormattingEnabled = true;
            SelectedDatasetsDisp.ItemHeight = 15;
            SelectedDatasetsDisp.Location = new Point(29, 24);
            SelectedDatasetsDisp.Name = "SelectedDatasetsDisp";
            SelectedDatasetsDisp.Size = new Size(264, 334);
            SelectedDatasetsDisp.TabIndex = 1;
            // 
            // RemoveSelectedElementBut
            // 
            RemoveSelectedElementBut.Enabled = false;
            RemoveSelectedElementBut.Location = new Point(29, 362);
            RemoveSelectedElementBut.Name = "RemoveSelectedElementBut";
            RemoveSelectedElementBut.Size = new Size(185, 23);
            RemoveSelectedElementBut.TabIndex = 2;
            RemoveSelectedElementBut.Text = "Remove selected";
            RemoveSelectedElementBut.UseVisualStyleBackColor = true;
            RemoveSelectedElementBut.Click += RemoveSelectedElementBut_Click;
            // 
            // RemoveAllElementsBut
            // 
            RemoveAllElementsBut.Location = new Point(218, 362);
            RemoveAllElementsBut.Name = "RemoveAllElementsBut";
            RemoveAllElementsBut.Size = new Size(75, 23);
            RemoveAllElementsBut.TabIndex = 3;
            RemoveAllElementsBut.Text = "RemoveAll";
            RemoveAllElementsBut.UseVisualStyleBackColor = true;
            RemoveAllElementsBut.Click += RemoveAllElementsBut_Click;
            // 
            // countDisp
            // 
            countDisp.BackColor = SystemColors.ButtonFace;
            countDisp.Location = new Point(29, 420);
            countDisp.Name = "countDisp";
            countDisp.ReadOnly = true;
            countDisp.Size = new Size(264, 23);
            countDisp.TabIndex = 4;
            // 
            // LoadLocalDatasetsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(332, 450);
            Controls.Add(countDisp);
            Controls.Add(RemoveAllElementsBut);
            Controls.Add(RemoveSelectedElementBut);
            Controls.Add(SelectedDatasetsDisp);
            Controls.Add(LoadDatasetsBut);
            Name = "LoadLocalDatasetsForm";
            Text = "LoadLocalDatasetsForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button LoadDatasetsBut;
        private ListBox SelectedDatasetsDisp;
        private Button RemoveSelectedElementBut;
        private Button RemoveAllElementsBut;
        private TextBox countDisp;
    }
}