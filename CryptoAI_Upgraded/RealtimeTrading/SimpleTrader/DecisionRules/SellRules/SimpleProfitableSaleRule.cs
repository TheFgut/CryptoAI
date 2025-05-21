using CryptoAI_Upgraded.RealtimeTrading.SimpleTrader.Features;

namespace CryptoAI_Upgraded.RealtimeTrading.SimpleTrader.DecisionRules.SellRules
{
    public class SimpleProfitableSaleRule : TradingDecisionRule
    {
        public double sellTreshold { get; private set; }
        public const int stepsToCheck = 3;
        public SimpleProfitableSaleRule(double sellTreshold)
        {
            this.sellTreshold = sellTreshold;
        }
        public override bool ChekcRule(Dictionary<DecisionFeature, object> features)
        {
            CurrencyHoldingAmount? holdingCurrency = getHoldingCurrency(features);
            if (holdingCurrency == null) throw new Exception("SimpleProfitableSaleRule is selling rule, but not recieved holdingCurrency");
            double currentCourse = getCurrentCourse(features);
            if (isCourseRising(features)) return false;
            return (1 - currentCourse / holdingCurrency.buyPrice) * 100 >= sellTreshold;
        }

        public bool isCourseRising(Dictionary<DecisionFeature, object> features)
        {
            int stepNum = 0;
            NetPrediction netPrediction = getNetPredictionData(features);
            foreach (var prediction in netPrediction.predHistory)
            {
                if (prediction.upTendention) return true;
                if (stepNum >= stepsToCheck) return false;
                stepNum++;
            }
            return false;
        }
    }
}
