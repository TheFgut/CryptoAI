namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks.UI
{
    partial class NetworkLearningStatsPanel
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
            PanelTitle = new Label();
            TrainingHistoryDisplay = new RichTextBox();
            SuspendLayout();
            // 
            // PanelTitle
            // 
            PanelTitle.AutoSize = true;
            PanelTitle.Font = new Font("Segoe UI", 14F);
            PanelTitle.Location = new Point(76, 13);
            PanelTitle.Name = "PanelTitle";
            PanelTitle.Size = new Size(123, 25);
            PanelTitle.TabIndex = 0;
            PanelTitle.Text = "Training stats";
            // 
            // TrainingHistoryDisplay
            // 
            TrainingHistoryDisplay.Location = new Point(3, 50);
            TrainingHistoryDisplay.Name = "TrainingHistoryDisplay";
            TrainingHistoryDisplay.Size = new Size(263, 194);
            TrainingHistoryDisplay.TabIndex = 1;
            TrainingHistoryDisplay.Text = "";
            // 
            // NetworkLearningStatsPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TrainingHistoryDisplay);
            Controls.Add(PanelTitle);
            Name = "NetworkLearningStatsPanel";
            Size = new Size(278, 262);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label PanelTitle;
        private RichTextBox TrainingHistoryDisplay;
    }
}
