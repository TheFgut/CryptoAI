using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks
{
    public class EarlyStopping
    {
        private readonly int patience;
        private readonly double minDelta;
        private readonly bool minimize;
        private int waitCount = 0;
        private double? bestMetric = null;

        public EarlyStopping(int patience = 10, double minDelta = 0.0001, bool minimize = true)
        {
            this.patience = patience;
            this.minDelta = minDelta;
            this.minimize = minimize;
        }

        public bool CheckShouldStop(double currentMetric) 
        {
            if (bestMetric == null ||
                (minimize && currentMetric < bestMetric - minDelta) ||
                (!minimize && currentMetric > bestMetric + minDelta))
            {
                bestMetric = currentMetric;
                waitCount = 0;
                return false;
            }

            waitCount++;
            return waitCount >= patience;
        }
    }
}
