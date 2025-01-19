namespace CryptoAI_Upgraded.DatasetsManaging.UI
{
    partial class DatasetsManagerPanel
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            ModifyDatasetsBut = new Button();
            DatasetsManagerLabel = new Label();
            DatasetsDetailsDisp = new RichTextBox();
            SuspendLayout();
            // 
            // ModifyDatasetsBut
            // 
            ModifyDatasetsBut.Location = new Point(14, 111);
            ModifyDatasetsBut.Name = "ModifyDatasetsBut";
            ModifyDatasetsBut.Size = new Size(176, 23);
            ModifyDatasetsBut.TabIndex = 0;
            ModifyDatasetsBut.Text = "Modify datasets";
            ModifyDatasetsBut.UseVisualStyleBackColor = true;
            ModifyDatasetsBut.Click += ModifyDatasetsBut_Click;
            // 
            // DatasetsManagerLabel
            // 
            DatasetsManagerLabel.Font = new Font("Segoe UI", 12F);
            DatasetsManagerLabel.Location = new Point(14, 12);
            DatasetsManagerLabel.Name = "DatasetsManagerLabel";
            DatasetsManagerLabel.Size = new Size(176, 23);
            DatasetsManagerLabel.TabIndex = 1;
            DatasetsManagerLabel.Text = "Datasets manager";
            DatasetsManagerLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // DatasetsDetailsDisp
            // 
            DatasetsDetailsDisp.Location = new Point(14, 38);
            DatasetsDetailsDisp.Name = "DatasetsDetailsDisp";
            DatasetsDetailsDisp.ReadOnly = true;
            DatasetsDetailsDisp.Size = new Size(176, 67);
            DatasetsDetailsDisp.TabIndex = 2;
            DatasetsDetailsDisp.Text = "";
            // 
            // DatasetsManagerPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            Controls.Add(DatasetsDetailsDisp);
            Controls.Add(DatasetsManagerLabel);
            Controls.Add(ModifyDatasetsBut);
            Name = "DatasetsManagerPanel";
            Size = new Size(205, 152);
            ResumeLayout(false);
        }

        #endregion

        private Button ModifyDatasetsBut;
        private Label DatasetsManagerLabel;
        private RichTextBox DatasetsDetailsDisp;
    }
}
