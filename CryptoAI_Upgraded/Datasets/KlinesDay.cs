using Binance.Net.Enums;
using Binance.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.Datasets
{
    public class KlinesDay
    {
        public List<KLine> data;
        public KlineInterval interval;
        public string pair;
        public KlinesDay()
        {

        }

        public KlinesDay(List<IBinanceKline> data, KlineInterval interval, string pair) 
        {
            this.data = data.Select(k => new KLine(k)).ToList();
            this.interval = interval;
            this.pair = pair;
        }
    }

    public class KLine
    {
        public decimal ClosePrice;

        public decimal OpenPrice;
        public decimal LowPrice;
        public decimal HighPrice;
        public decimal QuoteVolume;
        public decimal TakerBuyBaseVolume;
        public decimal TakerBuyQuoteVolume;
        public decimal TradeCount;
        public decimal Volume;
        public KLine()
        {

        }
        public KLine(IBinanceKline kline)
        {
            ClosePrice = kline.ClosePrice;
            OpenPrice = kline.OpenPrice;
            LowPrice = kline.LowPrice;
            HighPrice = kline.HighPrice;
            QuoteVolume = kline.QuoteVolume;
            TakerBuyBaseVolume = kline.TakerBuyBaseVolume;
            TakerBuyQuoteVolume = kline.TakerBuyQuoteVolume;
            TradeCount = kline.TradeCount;
            Volume = kline.Volume;
        }
    }
}
