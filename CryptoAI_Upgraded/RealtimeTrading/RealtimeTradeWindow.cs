using Binance.Net.Clients;
using CryptoAI_Upgraded.Datasets;
using CryptoAI_Upgraded.DatasetsLoader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoAI_Upgraded.RealtimeTrading
{
    public partial class RealtimeTradeWindow : Form
    {
        private RealtimeCourseDataCollector dataCollector;
        private BinanceRestClient client;
        private BinanceKlineSeries series;
        public RealtimeTradeWindow()
        {
            InitializeComponent();
            client = new BinanceRestClient();
            dataCollector = new RealtimeCourseDataCollector(client, "ETHBUSD");
        }

        private void OpenConnectionBut_Click(object sender, EventArgs e)
        {
            // Создаём объект для BTC/USDT с интервалом 1 минута, храним последние 50 свечей
            series = new BinanceKlineSeries("BTCUSDT", Binance.Net.Enums.KlineInterval.OneSecond, 60);
            series.CandleClosed += candle =>
            {
                List<KLine> data = series.GetSeries();
                Invoke(
                () =>
                {
                    ResultText.Text = candle.ClosePrice.ToString();
                    Graphic.Series.Clear();
                    var area = Graphic.ChartAreas.First();
                    double[] closePrices = data.Select(d => (double)d.ClosePrice).ToArray();
                    double minY = closePrices.Min();
                    double maxY = closePrices.Max();

                    // 3) Добавляем небольшой отступ сверху и снизу (скажем, по 5% от диапазона)
                    double delta = maxY - minY;
                    double margin = delta * 0.05;

                    // 4) Устанавливаем ось чуть ниже минимума и чуть выше максимума
                    area.AxisY.Minimum = Math.Floor(minY - margin);
                    area.AxisY.Maximum = Math.Ceiling(maxY + margin);
                    // 1) Форматируем метки по Y как целые числа
                    area.AxisY.LabelStyle.Format = "0";
                    Helpers.DataPlotting.DisplayDataOnChart(Graphic, closePrices,
                        "course", Color.Green);
                }

                );
            };
            //dataCollector.OpenConnection((newW) => ResultText.Text = newW.ToString());
            OpenConnectionBut.Enabled = false;
        }

        private void RealtimeTradeWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(series != null) series.Dispose();
            //dataCollector.CloseConnection();
        }
    }
}
