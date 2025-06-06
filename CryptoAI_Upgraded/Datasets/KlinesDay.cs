﻿using Binance.Net.Enums;
using Binance.Net.Interfaces;

namespace CryptoAI_Upgraded.Datasets
{
    public class KlinesDay
    {
        public List<KLine> data;
        public KlineInterval interval;
        public string pair;
        //normalization
        public NormalizationParams? normalization;
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
        public DateTime OpenTime;

        public decimal OpenPrice;
        public decimal ClosePrice;
        public decimal HighPrice;
        public decimal LowPrice;
        public decimal Volume;
        public decimal QuoteVolume;
        public decimal TakerBuyBaseVolume;
        public decimal TakerBuyQuoteVolume;
        public decimal TradeCount;
        //calculated
        public double WMA7;
        public double WMA25;
        public double WMA99;

        public KLine()
        {

        }
        public KLine(IBinanceKline kline)
        {
            OpenTime = kline.OpenTime; 
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

    public class NormalizationParams
    {
        public Dictionary<string, decimal> normalizedGroupsMin { get; set; }
        public Dictionary<string, decimal> normalizedGroupsMax { get; set; }

        public double Denormalize(double value, string key)
        {
            return Helpers.Normalization.Denormalize(value,
                (double)normalizedGroupsMin[key], (double)normalizedGroupsMax[key]);
        }
    }
}
