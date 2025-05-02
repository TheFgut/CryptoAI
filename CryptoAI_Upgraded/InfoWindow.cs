using CryptoAI_Upgraded.Datasets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoAI_Upgraded
{
    public partial class InfoWindow : Form
    {
        public InfoWindow(string title)
        {
            InitializeComponent();

        }

        public void Show(List<KLine> klines)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 20; i++)
            {
                var kline = klines[i];
                sb.AppendLine(Math.Round(kline.TradeCount, 3).ToString());
                //sb.AppendLine($"{kline.OpenTime}, " +
                //              $"{Math.Round(kline.OpenPrice,3)}, {kline.ClosePrice}, {kline.HighPrice}, {kline.LowPrice}, " +
                //              $"{kline.Volume}, {kline.QuoteVolume}, " +
                //              $"{kline.TakerBuyBaseVolume}, {kline.TakerBuyQuoteVolume}, {kline.TradeCount}");
            }

            infoTextBox.Text = sb.ToString();
        }
    }
}
