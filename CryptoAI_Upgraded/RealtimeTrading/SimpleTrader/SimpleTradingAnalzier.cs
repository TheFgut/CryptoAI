using CryptoAI_Upgraded.DataAnalasys;
using CryptoAI_Upgraded.Datasets;
using CryptoAI_Upgraded.RealtimeTrading.SimpleTrader.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.RealtimeTrading.SimpleTrader
{
    internal class SimpleTradingAnalzier
    {
        private TradingSimpleDecisionMaker tradingDecisionMaker;
        private bool ignoreCommision;

        private const float spotTradeBuyComission = 0.001f;//0.1 percent
        private const float spotTradeSellComission = 0.001f;//0.1 percent
        //temporary
        private int tradesFinished;
        private int profitableTrades;
        private double currentPrice;
        private CurrencyHoldingAmount? currencyHolding;
        private double profit;
        public SimpleTradingAnalzier(bool ignoreCommision)
        {
            this.ignoreCommision = ignoreCommision;
            tradingDecisionMaker = new TradingSimpleDecisionMaker(BuyAction, SellAction);
        }

        public string Analize(NetworkAccAnalize analize, NormalizationParams normalization)
        {
            int dataStorePool = 5;
            NetPredictionGenerator predDataGenerator = new NetPredictionGenerator(dataStorePool);
            for (int i = 1; i < analize.analizeStepsAmount; i++)
            {
                //warmup
                if(i < dataStorePool + 1)
                {
                    predDataGenerator.RecordHistory(normalization.Denormalize(analize.real[i - 1], "price"),
                        normalization.Denormalize(analize.real[i], "price"),
                        normalization.Denormalize(analize.predict[i], "price"));
                    continue;
                }
                predDataGenerator.SetCurrentPredictions(normalization.Denormalize(analize.predict[i], "price"));
                currentPrice = normalization.Denormalize(analize.real[i], "price");
                Dictionary<DecisionFeature, object> features = new Dictionary<DecisionFeature, object>() 
                {
                    {DecisionFeature.CurrentCourse, normalization.Denormalize(analize.real[i], "price") },
                    {DecisionFeature.NetPredictions, predDataGenerator.GenerateData() }
                };

                tradingDecisionMaker.CheckDecisionRules(features);

                predDataGenerator.RecordHistory(normalization.Denormalize(analize.real[i - 1], "price"),
                    normalization.Denormalize(analize.real[i], "price"),
                    normalization.Denormalize(analize.predict[i], "price"));
            }
            string commissionStr = ignoreCommision ? "without commission" : "";
            return $"Trading analize:\ntrades:{tradesFinished}\nprofitable trades: {profitableTrades}\nprofit:{profit * 100}% {commissionStr}";
        }

        private bool BuyAction()
        {
            currencyHolding = new CurrencyHoldingAmount() { buyPrice = currentPrice };
            return true;
        }

        private bool SellAction()
        {
            double tradeProfit = 1 - (currentPrice / currencyHolding.buyPrice);
            profitableTrades += tradeProfit > 0 ? 1 : 0;
            profit += tradeProfit;
            if (!ignoreCommision) profit -= spotTradeBuyComission + spotTradeSellComission;//this is crude formula. real commision is a bit lower than that
            currencyHolding = null;
            tradesFinished++;
            return true;
        }
    }
}
