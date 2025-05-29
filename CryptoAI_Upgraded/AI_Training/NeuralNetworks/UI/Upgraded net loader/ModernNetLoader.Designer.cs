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
            UserText = new Label();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            label2 = new Label();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)AwailableNetSGrid).BeginInit();
            SuspendLayout();
            // 
            // AwailableNetSGrid
            // 
            AwailableNetSGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AwailableNetSGrid.Location = new Point(12, 31);
            AwailableNetSGrid.Name = "AwailableNetSGrid";
            AwailableNetSGrid.Size = new Size(670, 381);
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
            // UserText
            // 
            UserText.AutoSize = true;
            UserText.Font = new Font("Segoe UI", 12F);
            UserText.Location = new Point(488, 7);
            UserText.Name = "UserText";
            UserText.Size = new Size(93, 21);
            UserText.TabIndex = 2;
            UserText.Text = "User: admin";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.Font = new Font("Segoe UI", 12F);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(587, 7);
            label1.Name = "label1";
            label1.Size = new Size(95, 21);
            label1.TabIndex = 3;
            label1.Text = "Unauthorize";
            // 
            // button1
            // 
            button1.Location = new Point(547, 418);
            button1.Name = "button1";
            button1.Size = new Size(135, 23);
            button1.TabIndex = 4;
            button1.Text = "Upload network";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(399, 418);
            button2.Name = "button2";
            button2.Size = new Size(142, 23);
            button2.TabIndex = 5;
            button2.Text = "Load selected";
            button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 8);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 6;
            label2.Text = "Search";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(65, 5);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 7;
            // 
            // ModernNetLoader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(694, 450);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(UserText);
            Controls.Add(ReloadBut);
            Controls.Add(AwailableNetSGrid);
            Name = "ModernNetLoader";
            Text = "ModernNetLoader";
            ((System.ComponentModel.ISupportInitialize)AwailableNetSGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView AwailableNetSGrid;
        private Button ReloadBut;
        private Label UserText;
        private Label label1;
        private Button button1;
        private Button button2;
        private Label label2;
        private TextBox textBox1;
    }
}