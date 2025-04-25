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
            OpenPriceCheckBox = new CheckBox();
            ClosePriceCheckBox = new CheckBox();
            TradeCountBox = new CheckBox();
            HighPriceCheckBox = new CheckBox();
            LowPriceCheckBox = new CheckBox();
            QuoteVolumeCheckBox = new CheckBox();
            InputsCountBox = new TextBox();
            TimeFragmentsBox = new TextBox();
            IputFeaturesLabel = new Label();
            InpCountLabel = new Label();
            label1 = new Label();
            FragmentNumCheckBox = new CheckBox();
            PriceDeltaCheckBox = new CheckBox();
            CandleTypeCheckBox = new CheckBox();
            VolatilityCheckBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)LayersGrid).BeginInit();
            SuspendLayout();
            // 
            // LayersGrid
            // 
            LayersGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LayersGrid.Location = new Point(12, 12);
            LayersGrid.Name = "LayersGrid";
            LayersGrid.Size = new Size(485, 384);
            LayersGrid.TabIndex = 0;
            // 
            // AddLayerBut
            // 
            AddLayerBut.Location = new Point(12, 402);
            AddLayerBut.Name = "AddLayerBut";
            AddLayerBut.Size = new Size(273, 23);
            AddLayerBut.TabIndex = 1;
            AddLayerBut.Text = "Add layer";
            AddLayerBut.UseVisualStyleBackColor = true;
            AddLayerBut.Click += AddLayerBut_Click;
            // 
            // RemoveLAyersBut
            // 
            RemoveLAyersBut.Location = new Point(291, 402);
            RemoveLAyersBut.Name = "RemoveLAyersBut";
            RemoveLAyersBut.Size = new Size(206, 23);
            RemoveLAyersBut.TabIndex = 2;
            RemoveLAyersBut.Text = "Remove layers";
            RemoveLAyersBut.UseVisualStyleBackColor = true;
            RemoveLAyersBut.Click += RemoveLayersBut_Click;
            // 
            // CreateNetworkBut
            // 
            CreateNetworkBut.Location = new Point(12, 431);
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
            // OpenPriceCheckBox
            // 
            OpenPriceCheckBox.AutoCheck = false;
            OpenPriceCheckBox.AutoSize = true;
            OpenPriceCheckBox.Checked = true;
            OpenPriceCheckBox.CheckState = CheckState.Checked;
            OpenPriceCheckBox.Font = new Font("Segoe UI", 14F);
            OpenPriceCheckBox.Location = new Point(526, 177);
            OpenPriceCheckBox.Name = "OpenPriceCheckBox";
            OpenPriceCheckBox.Size = new Size(124, 29);
            OpenPriceCheckBox.TabIndex = 5;
            OpenPriceCheckBox.Text = "Open price";
            OpenPriceCheckBox.UseVisualStyleBackColor = true;
            // 
            // ClosePriceCheckBox
            // 
            ClosePriceCheckBox.AutoSize = true;
            ClosePriceCheckBox.Font = new Font("Segoe UI", 14F);
            ClosePriceCheckBox.Location = new Point(526, 212);
            ClosePriceCheckBox.Name = "ClosePriceCheckBox";
            ClosePriceCheckBox.Size = new Size(124, 29);
            ClosePriceCheckBox.TabIndex = 6;
            ClosePriceCheckBox.Text = "Close price";
            ClosePriceCheckBox.UseVisualStyleBackColor = true;
            // 
            // TradeCountBox
            // 
            TradeCountBox.AutoSize = true;
            TradeCountBox.Font = new Font("Segoe UI", 14F);
            TradeCountBox.Location = new Point(525, 432);
            TradeCountBox.Name = "TradeCountBox";
            TradeCountBox.Size = new Size(128, 29);
            TradeCountBox.TabIndex = 7;
            TradeCountBox.Text = "TradeCount";
            TradeCountBox.UseVisualStyleBackColor = true;
            // 
            // HighPriceCheckBox
            // 
            HighPriceCheckBox.AutoSize = true;
            HighPriceCheckBox.Font = new Font("Segoe UI", 14F);
            HighPriceCheckBox.Location = new Point(525, 247);
            HighPriceCheckBox.Name = "HighPriceCheckBox";
            HighPriceCheckBox.Size = new Size(118, 29);
            HighPriceCheckBox.TabIndex = 8;
            HighPriceCheckBox.Text = "High price";
            HighPriceCheckBox.UseVisualStyleBackColor = true;
            // 
            // LowPriceCheckBox
            // 
            LowPriceCheckBox.AutoSize = true;
            LowPriceCheckBox.Font = new Font("Segoe UI", 14F);
            LowPriceCheckBox.Location = new Point(526, 282);
            LowPriceCheckBox.Name = "LowPriceCheckBox";
            LowPriceCheckBox.Size = new Size(112, 29);
            LowPriceCheckBox.TabIndex = 9;
            LowPriceCheckBox.Text = "Low price";
            LowPriceCheckBox.UseVisualStyleBackColor = true;
            // 
            // QuoteVolumeCheckBox
            // 
            QuoteVolumeCheckBox.AutoSize = true;
            QuoteVolumeCheckBox.Font = new Font("Segoe UI", 14F);
            QuoteVolumeCheckBox.Location = new Point(525, 397);
            QuoteVolumeCheckBox.Name = "QuoteVolumeCheckBox";
            QuoteVolumeCheckBox.Size = new Size(150, 29);
            QuoteVolumeCheckBox.TabIndex = 10;
            QuoteVolumeCheckBox.Text = "Quote volume";
            QuoteVolumeCheckBox.UseVisualStyleBackColor = true;
            // 
            // InputsCountBox
            // 
            InputsCountBox.Location = new Point(576, 62);
            InputsCountBox.Name = "InputsCountBox";
            InputsCountBox.Size = new Size(100, 23);
            InputsCountBox.TabIndex = 11;
            InputsCountBox.Text = "1";
            // 
            // TimeFragmentsBox
            // 
            TimeFragmentsBox.Location = new Point(576, 91);
            TimeFragmentsBox.Name = "TimeFragmentsBox";
            TimeFragmentsBox.Size = new Size(100, 23);
            TimeFragmentsBox.TabIndex = 12;
            TimeFragmentsBox.Text = "60";
            // 
            // IputFeaturesLabel
            // 
            IputFeaturesLabel.AutoSize = true;
            IputFeaturesLabel.Font = new Font("Segoe UI", 16F);
            IputFeaturesLabel.Location = new Point(555, 135);
            IputFeaturesLabel.Name = "IputFeaturesLabel";
            IputFeaturesLabel.Size = new Size(147, 30);
            IputFeaturesLabel.TabIndex = 13;
            IputFeaturesLabel.Text = "Input features";
            // 
            // InpCountLabel
            // 
            InpCountLabel.AutoSize = true;
            InpCountLabel.Location = new Point(507, 70);
            InpCountLabel.Name = "InpCountLabel";
            InpCountLabel.Size = new Size(61, 15);
            InpCountLabel.TabIndex = 14;
            InpCountLabel.Text = "Inp. count";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(507, 99);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 15;
            label1.Text = "T.F. count";
            // 
            // FragmentNumCheckBox
            // 
            FragmentNumCheckBox.AutoSize = true;
            FragmentNumCheckBox.Checked = true;
            FragmentNumCheckBox.CheckState = CheckState.Checked;
            FragmentNumCheckBox.Font = new Font("Segoe UI", 14F);
            FragmentNumCheckBox.Location = new Point(674, 177);
            FragmentNumCheckBox.Name = "FragmentNumCheckBox";
            FragmentNumCheckBox.Size = new Size(154, 29);
            FragmentNumCheckBox.TabIndex = 16;
            FragmentNumCheckBox.Text = "Fragment num";
            FragmentNumCheckBox.UseVisualStyleBackColor = true;
            // 
            // PriceDeltaCheckBox
            // 
            PriceDeltaCheckBox.AutoSize = true;
            PriceDeltaCheckBox.Checked = true;
            PriceDeltaCheckBox.CheckState = CheckState.Checked;
            PriceDeltaCheckBox.Font = new Font("Segoe UI", 14F);
            PriceDeltaCheckBox.Location = new Point(525, 317);
            PriceDeltaCheckBox.Name = "PriceDeltaCheckBox";
            PriceDeltaCheckBox.Size = new Size(120, 29);
            PriceDeltaCheckBox.TabIndex = 17;
            PriceDeltaCheckBox.Text = "Price delta";
            PriceDeltaCheckBox.UseVisualStyleBackColor = true;
            // 
            // CandleTypeCheckBox
            // 
            CandleTypeCheckBox.AutoSize = true;
            CandleTypeCheckBox.Checked = true;
            CandleTypeCheckBox.CheckState = CheckState.Checked;
            CandleTypeCheckBox.Font = new Font("Segoe UI", 14F);
            CandleTypeCheckBox.Location = new Point(525, 352);
            CandleTypeCheckBox.Name = "CandleTypeCheckBox";
            CandleTypeCheckBox.Size = new Size(131, 29);
            CandleTypeCheckBox.TabIndex = 18;
            CandleTypeCheckBox.Text = "Candle type";
            CandleTypeCheckBox.UseVisualStyleBackColor = true;
            // 
            // VolatilityCheckBox
            // 
            VolatilityCheckBox.AutoSize = true;
            VolatilityCheckBox.Checked = true;
            VolatilityCheckBox.CheckState = CheckState.Checked;
            VolatilityCheckBox.Font = new Font("Segoe UI", 14F);
            VolatilityCheckBox.Location = new Point(674, 212);
            VolatilityCheckBox.Name = "VolatilityCheckBox";
            VolatilityCheckBox.Size = new Size(104, 29);
            VolatilityCheckBox.TabIndex = 19;
            VolatilityCheckBox.Text = "Volatility";
            VolatilityCheckBox.UseVisualStyleBackColor = true;
            // 
            // NeuralNetworkCreatorWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(821, 466);
            Controls.Add(VolatilityCheckBox);
            Controls.Add(CandleTypeCheckBox);
            Controls.Add(PriceDeltaCheckBox);
            Controls.Add(FragmentNumCheckBox);
            Controls.Add(label1);
            Controls.Add(InpCountLabel);
            Controls.Add(IputFeaturesLabel);
            Controls.Add(TimeFragmentsBox);
            Controls.Add(InputsCountBox);
            Controls.Add(QuoteVolumeCheckBox);
            Controls.Add(LowPriceCheckBox);
            Controls.Add(HighPriceCheckBox);
            Controls.Add(TradeCountBox);
            Controls.Add(ClosePriceCheckBox);
            Controls.Add(OpenPriceCheckBox);
            Controls.Add(InputLabel);
            Controls.Add(CreateNetworkBut);
            Controls.Add(RemoveLAyersBut);
            Controls.Add(AddLayerBut);
            Controls.Add(LayersGrid);
            Name = "NeuralNetworkCreatorWindow";
            Text = "NeuralNetworkCreatorWindow";
            FormClosing += NeuralNetworkCreatorWindow_FormClosing;
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
        private CheckBox OpenPriceCheckBox;
        private CheckBox ClosePriceCheckBox;
        private CheckBox TradeCountBox;
        private CheckBox HighPriceCheckBox;
        private CheckBox LowPriceCheckBox;
        private CheckBox QuoteVolumeCheckBox;
        private TextBox InputsCountBox;
        private TextBox TimeFragmentsBox;
        private Label IputFeaturesLabel;
        private Label InpCountLabel;
        private Label label1;
        private CheckBox FragmentNumCheckBox;
        private CheckBox PriceDeltaCheckBox;
        private CheckBox CandleTypeCheckBox;
        private CheckBox VolatilityCheckBox;
    }
}