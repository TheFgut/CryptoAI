using CryptoAI_Upgraded.RealtimeTrading.SimpleTrader.DecisionRules;
using CryptoAI_Upgraded.RealtimeTrading.SimpleTrader.DecisionRules.BuyRules;
using CryptoAI_Upgraded.RealtimeTrading.SimpleTrader.DecisionRules.SellRules;

namespace CryptoAI_Upgraded.RealtimeTrading.SimpleTrader
{
    public class TradingSimpleDecisionMaker
    {
        private onActionOnStock tryBuy;
        private onActionOnStock trySell;

        internal Action? onBuyCallback;
        internal Action? onSellCallback;

        private TradingDecisionRule[] sellingRules;
        private TradingDecisionRule[] buyingRules;

        private CurrencyHoldingAmount? holdingCurrency;

        public TradingSimpleDecisionMaker(onActionOnStock tryBuy, onActionOnStock trySell) 
        {
            this.tryBuy = tryBuy;
            this.trySell = trySell;
            sellingRules = new TradingDecisionRule[] { new BigLossPrevention(0.25,-0.02,10, this), new SimpleProfitableSaleRule(0.5) };
            buyingRules = new[] { new SimpleBuyRule(3) };
        }

        public void CheckDecisionRules(Dictionary<DecisionFeature, object> features)
        {
            double currentCourse;
            if (features.TryGetValue(DecisionFeature.CurrentCourse, out var courseObj)) currentCourse = (double)courseObj;
            else throw new Exception("Cant make decision without current course");
            features.Add(DecisionFeature.HoldingCurrency, holdingCurrency);
            //selling behaviour
            if (holdingCurrency != null)
            {
                if (CheckRules_OR(sellingRules, features)) Sell();
            }
            else//buying behaviour
            {
                if (CheckRules_OR(buyingRules, features)) Buy(currentCourse);
            }
        }

        /// <summary>
        /// checking rules througth logical or - anything is true -> returning true
        /// </summary>
        /// <param name="rules"></param>
        /// <returns></returns>
        private bool CheckRules_OR(TradingDecisionRule[] rules, Dictionary<DecisionFeature, object> features)
        {
            foreach (var rule in rules)
            {
                if(rule.ChekcRule(features)) return true;
            }
            return false;
        }

        private void Buy(double price)
        {
            if (holdingCurrency != null) throw new Exception("Cant buy before selling previous");
            if (!tryBuy.Invoke()) return;//unsuccessful
            onBuyCallback?.Invoke();
            holdingCurrency = new CurrencyHoldingAmount() { buyPrice = price };
        }

        private void Sell()
        {
            if (holdingCurrency == null) throw new Exception("There is nothing to sell");
            if (!trySell.Invoke()) return;//unsuccessful
            onSellCallback?.Invoke();
            holdingCurrency = null;
        }
    }

    public enum DecisionFeature
    {
        CurrentCourse,
        /// <summary>
        /// object with type NetPrediction
        /// </summary>
        NetPredictions,
        HoldingCurrency
    }

    public delegate bool onActionOnStock();

    public class CurrencyHoldingAmount
    {
        public double buyPrice {  get; set; }
    }
}
