using CryptoAI_Upgraded.RealtimeTrading.SimpleTrader.Features;

namespace CryptoAI_Upgraded.RealtimeTrading.SimpleTrader.DecisionRules
{
    public abstract class TradingDecisionRule
    {
        public virtual string ruleName => GetType().Name;

        public abstract bool ChekcRule(Dictionary<DecisionFeature, object> features);

        protected double getCurrentCourse(Dictionary<DecisionFeature, object> features)
        {
            double currentCourse;
            if (features.TryGetValue(DecisionFeature.CurrentCourse, out var courseObj)) currentCourse = (double)courseObj;
            else throw new Exception($"Rule \"{ruleName}\" cant make decision without current course");
            return currentCourse;
        }

        protected CurrencyHoldingAmount? getHoldingCurrency(Dictionary<DecisionFeature, object> features)
        {
            CurrencyHoldingAmount holdingCurrency;
            if (features.TryGetValue(DecisionFeature.HoldingCurrency, out var courseObj)) holdingCurrency = (CurrencyHoldingAmount)courseObj;
            else throw new Exception($"Rule \"{ruleName}\" cant make decision without holding currency info");
            return holdingCurrency;
        }

        protected NetPrediction getNetPredictionData(Dictionary<DecisionFeature, object> features)
        {
            NetPrediction netPrediction;
            if (features.TryGetValue(DecisionFeature.NetPredictions, out var courseObj)) netPrediction = (NetPrediction)courseObj;
            else throw new Exception($"Rule \"{ruleName}\" cant make decision without net prediction data");
            return netPrediction;
        }
    }
}
