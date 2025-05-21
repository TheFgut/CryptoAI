using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.RealtimeTrading.SimpleTrader.DecisionRules.SellRules
{
    public class BigLossPrevention : TradingDecisionRule
    {
        public double basicSellTreshold { get; private set; }
        public double minSellTreshold { get; private set; }
        private int stepsToReachMin { get; set; }

        private int stepsCounter;
        public BigLossPrevention(double basicSellTreshold, double minSellTreshold,
            int stepsToReachMin, TradingSimpleDecisionMaker decisionMaker)
        {
            this.basicSellTreshold = basicSellTreshold;
            this.minSellTreshold = minSellTreshold;
            this.stepsToReachMin = stepsToReachMin;
            stepsCounter = stepsToReachMin;
            decisionMaker.onBuyCallback += ResetCounter;
        }
        public override bool ChekcRule(Dictionary<DecisionFeature, object> features)
        {
            CurrencyHoldingAmount? holdingCurrency = getHoldingCurrency(features);
            if (holdingCurrency == null) throw new Exception("BigLossPrevention is selling rule, but not recieved holdingCurrency");

            double sellTreshold = double.Lerp(minSellTreshold, basicSellTreshold, stepsCounter / (double)stepsToReachMin);
            if(stepsCounter > 0) stepsCounter--;
            double currentCourse = getCurrentCourse(features);
            return (1 - holdingCurrency.buyPrice / currentCourse) * 100 >= basicSellTreshold;
        }

        private void ResetCounter()
        {
            stepsCounter = stepsToReachMin;
        }
    }
}
