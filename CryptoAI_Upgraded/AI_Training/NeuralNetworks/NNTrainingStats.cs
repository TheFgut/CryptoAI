using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks
{
    public class NNTrainingStats
    {
        public double[] errorsLoss { get; private set; }
        private int runsCount;

        private int currentRecordingNum;
        public NNTrainingStats(int runsCount) 
        {
            this.runsCount = runsCount;
            errorsLoss = new double[runsCount];
            currentRecordingNum = 0;
        }

        public void RecordError(double error)
        {
            errorsLoss[currentRecordingNum] = error;
        }

        public void GoNext()
        {
            if (currentRecordingNum >= errorsLoss.Length - 1) return;
            currentRecordingNum++;
        }
    }
}
