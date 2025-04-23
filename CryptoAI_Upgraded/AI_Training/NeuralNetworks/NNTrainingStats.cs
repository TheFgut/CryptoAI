using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks
{
    public class NNTrainingStats
    {
        //training
        public double[] awerageLossErrors { get; private set; }
        public double[] maxLossErrors { get; private set; }
        public double[] minLossErrors { get; private set; }
        //testing
        public double avarageTestError { get; private set; }
        public double maxTestError { get; private set; }
        public double minTestError { get; private set; }
        private bool noTestMetrics { get; set; }


        public int runsCount { get; private set; }

        private int currentRecordingNum;
        public int runsPassed => currentRecordingNum;
        public NNTrainingStats(int runsCount) 
        {
            this.runsCount = runsCount;
            awerageLossErrors = new double[runsCount];
            minLossErrors = new double[runsCount];
            maxLossErrors = new double[runsCount];
            currentRecordingNum = 0;
            noTestMetrics = true;
        }

        public void RecordMinError(double error)
        {
            minLossErrors[currentRecordingNum] = error;
        }
        public void RecordMaxError(double error)
        {
            maxLossErrors[currentRecordingNum] = error;
        }

        public void RecordAwerageError(double error)
        {
            awerageLossErrors[currentRecordingNum] = error;
        }

        public void GoNext()
        {
            if (currentRecordingNum >= awerageLossErrors.Length - 1) return;
            currentRecordingNum++;
        }

        public void RecordTestMetrics(double awgTestError, double minTestError, double maxTestError)
        {
            avarageTestError = awgTestError;
            this.minTestError = minTestError;
            this.maxTestError = maxTestError;
            noTestMetrics = false;
        }

        public string ToRichTextString()
        {
            StringBuilder details = new StringBuilder();
            details.AppendLine("{\\rtf1\\ansi");
            details.AppendLine("Learning stats:");
            for (int i = 0; i < currentRecordingNum; i++)
            {
                double rate = (awerageLossErrors[i] * 2) + maxLossErrors[i] + maxLossErrors[i];
                details.Append($"Rate: {rate.ToString("F5")} awg: {awerageLossErrors[i].ToString("F5")}");
                details.Append($"min: {minLossErrors[i].ToString("F5")} max: {maxLossErrors[i].ToString("F5")}");
                details.AppendLine("\\par");
            }
            if (!noTestMetrics)
            {
                details.AppendLine("{\\fs25");
                details.AppendLine("Test results:");
                details.AppendLine($"Average error: {avarageTestError}");
                details.AppendLine($"Max error: {maxTestError}");
                details.AppendLine($"Min error: {minTestError}");
                details.AppendLine("}");
                details.AppendLine("}");
            }
            return details.ToString();
        }
    }
}
