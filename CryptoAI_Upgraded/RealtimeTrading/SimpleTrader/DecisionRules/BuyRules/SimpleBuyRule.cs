using CryptoAI_Upgraded.RealtimeTrading.SimpleTrader.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.RealtimeTrading.SimpleTrader.DecisionRules.BuyRules
{
    internal class SimpleBuyRule : TradingDecisionRule
    {
        private int stepsToCheck;
        public SimpleBuyRule(int stepsToCheck)
        {
            this.stepsToCheck = stepsToCheck;
        }

        public override bool ChekcRule(Dictionary<DecisionFeature, object> features)
        {
            int stepNum = 0;
            NetPrediction netPrediction = getNetPredictionData(features);
            foreach(var prediction in netPrediction.predHistory)
            {
                if (!prediction.directionGuessedCorrectly && !prediction.upTendention) return false;
                if (stepNum >= stepsToCheck) return true;
                stepNum++;
            }
            throw new Exception("SimpleBuyRule. recorded prediction steps is not enougth to make decision");
        }
    }
}
