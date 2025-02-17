namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks.UI
{
    public partial class NetworkManagePanel
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
            CreateNetworkBut = new Button();
            LoadNeworkBut = new Button();
            SaveNetworkBut = new Button();
            NetworkNameChangePanel = new TextBox();
            NNameLabel = new Label();
            NetworkNameLabel = new Label();
            NetworkDetailsPanel = new RichTextBox();
            DetailsBut = new Button();
            SuspendLayout();
            // 
            // CreateNetworkBut
            // 
            CreateNetworkBut.Location = new Point(16, 263);
            CreateNetworkBut.Name = "CreateNetworkBut";
            CreateNetworkBut.Size = new Size(75, 23);
            CreateNetworkBut.TabIndex = 0;
            CreateNetworkBut.Text = "Create";
            CreateNetworkBut.UseVisualStyleBackColor = true;
            CreateNetworkBut.Click += CreateNetworkBut_Click;
            // 
            // LoadNeworkBut
            // 
            LoadNeworkBut.Location = new Point(16, 234);
            LoadNeworkBut.Name = "LoadNeworkBut";
            LoadNeworkBut.Size = new Size(75, 23);
            LoadNeworkBut.TabIndex = 1;
            LoadNeworkBut.Text = "Load";
            LoadNeworkBut.UseVisualStyleBackColor = true;
            LoadNeworkBut.Click += LoadNeworkBut_Click;
            // 
            // SaveNetworkBut
            // 
            SaveNetworkBut.Location = new Point(97, 263);
            SaveNetworkBut.Name = "SaveNetworkBut";
            SaveNetworkBut.Size = new Size(165, 23);
            SaveNetworkBut.TabIndex = 2;
            SaveNetworkBut.Text = "Save";
            SaveNetworkBut.UseVisualStyleBackColor = true;
            SaveNetworkBut.Click += SaveNetworkBut_Click;
            // 
            // NetworkNameChangePanel
            // 
            NetworkNameChangePanel.Location = new Point(142, 234);
            NetworkNameChangePanel.Name = "NetworkNameChangePanel";
            NetworkNameChangePanel.Size = new Size(80, 23);
            NetworkNameChangePanel.TabIndex = 3;
            NetworkNameChangePanel.TextChanged += NetworkNameChangePanel_TextChanged;
            // 
            // NNameLabel
            // 
            NNameLabel.AutoSize = true;
            NNameLabel.Location = new Point(97, 242);
            NNameLabel.Name = "NNameLabel";
            NNameLabel.Size = new Size(39, 15);
            NNameLabel.TabIndex = 4;
            NNameLabel.Text = "Name";
            // 
            // NetworkNameLabel
            // 
            NetworkNameLabel.Font = new Font("Segoe UI", 14F);
            NetworkNameLabel.ForeColor = Color.IndianRed;
            NetworkNameLabel.Location = new Point(16, 12);
            NetworkNameLabel.Name = "NetworkNameLabel";
            NetworkNameLabel.Size = new Size(246, 23);
            NetworkNameLabel.TabIndex = 5;
            NetworkNameLabel.Text = "No loaded networks";
            NetworkNameLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // NetworkDetailsPanel
            // 
            NetworkDetailsPanel.Location = new Point(16, 49);
            NetworkDetailsPanel.Name = "NetworkDetailsPanel";
            NetworkDetailsPanel.ReadOnly = true;
            NetworkDetailsPanel.Size = new Size(246, 179);
            NetworkDetailsPanel.TabIndex = 6;
            NetworkDetailsPanel.Text = "";
            // 
            // DetailsBut
            // 
            DetailsBut.Location = new Point(228, 234);
            DetailsBut.Name = "DetailsBut";
            DetailsBut.Size = new Size(32, 23);
            DetailsBut.TabIndex = 7;
            DetailsBut.Text = "...";
            DetailsBut.UseVisualStyleBackColor = true;
            DetailsBut.Click += DetailsBut_Click;
            // 
            // NetworkManagePanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            Controls.Add(DetailsBut);
            Controls.Add(NetworkDetailsPanel);
            Controls.Add(NetworkNameLabel);
            Controls.Add(NNameLabel);
            Controls.Add(NetworkNameChangePanel);
            Controls.Add(SaveNetworkBut);
            Controls.Add(LoadNeworkBut);
            Controls.Add(CreateNetworkBut);
            Name = "NetworkManagePanel";
            Size = new Size(278, 300);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button CreateNetworkBut;
        private Button LoadNeworkBut;
        private Button SaveNetworkBut;
        private TextBox NetworkNameChangePanel;
        private Label NNameLabel;
        private Label NetworkNameLabel;
        private RichTextBox NetworkDetailsPanel;
        private Button DetailsBut;
    }
}
