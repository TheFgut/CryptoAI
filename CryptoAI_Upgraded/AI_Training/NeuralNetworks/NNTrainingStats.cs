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
        public NetworkRunData[] trainingRunsData {  get; private set; }
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
            trainingRunsData = new NetworkRunData[runsCount];
            for (int i = 0; i < trainingRunsData.Length; i++)
            {
                trainingRunsData[i] = new NetworkRunData();
            }
            currentRecordingNum = 0;
            noTestMetrics = true;
        }

        public void RecordMinError(double error)
        {
            trainingRunsData[currentRecordingNum].minError = error;
        }
        public void RecordMaxError(double error)
        {
            trainingRunsData[currentRecordingNum].maxError = error;
        }

        public void RecordAwerageError(double error)
        {
            trainingRunsData[currentRecordingNum].averageError = error;
        }

        public void GoNext()
        {
            if (currentRecordingNum >= runsCount - 1) return;
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
                double rate = (trainingRunsData[i].averageError * 2) + trainingRunsData[i].minError + trainingRunsData[i].maxError;
                details.Append($"Rate: {rate.ToString("F5")} awg: {trainingRunsData[i].averageError.ToString("F5")}");
                details.Append($"min: {trainingRunsData[i].minError.ToString("F5")} max: " +
                    $"{trainingRunsData[i].maxError.ToString("F5")}");
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
