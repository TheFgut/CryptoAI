﻿namespace CryptoAI_Upgraded.RealtimeTrading.SimpleTrader.DecisionRules.Logic
{
    public class LogicORRule : TradingDecisionRule
    {
        private TradingDecisionRule[] rules;
        public LogicORRule(params TradingDecisionRule[] rules)
        {
            this.rules = rules;
        }

        public override bool ChekcRule(Dictionary<DecisionFeature, object> features)
        {
            foreach (var rule in rules)
            {
                if (rule.ChekcRule(features)) return true;
            }
            return false;
        }
    }
}
