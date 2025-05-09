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
        public int runsCount { get; private set; }

        private int currentRecordingNum;
        public int runsPassed => currentRecordingNum + 1;

        public NetworkRunData lastRun => trainingRunsData[currentRecordingNum];
        public NNTrainingStats(int runsCount, List<DatasetID> datasetIds) 
        {
            this.runsCount = runsCount;
            trainingRunsData = new NetworkRunData[runsCount];
            for (int i = 0; i < trainingRunsData.Length; i++)
            {
                trainingRunsData[i] = NetworkRunData.Default();
                trainingRunsData[i].trainingDatasetIDs = datasetIds;
            }
            currentRecordingNum = 0;
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
            trainingRunsData[currentRecordingNum].avarageTestError = awgTestError;
            trainingRunsData[currentRecordingNum].minTestError = minTestError;
            trainingRunsData[currentRecordingNum].maxTestError = maxTestError;
            trainingRunsData[currentRecordingNum].noTestMetrics = false;
        }

        public string ToRichTextString()
        {
            StringBuilder details = new StringBuilder();
            details.AppendLine("{\\rtf1\\ansi");
            details.AppendLine("Learning stats:");
            for (int i = 0; i < currentRecordingNum; i++)
            {
                NetworkRunData run = trainingRunsData[i];
                //training metrics
                double rate = (run.averageError * 2) + run.minError + run.maxError;
                details.Append($"Rate: {rate.ToString("F5")} awg: {run.averageError.ToString("F5")}");
                details.Append($"min: {run.minError.ToString("F5")} max: " +
                    $"{run.maxError.ToString("F5")}");
                details.AppendLine("\\par");
                //testing metrics
                if (i == currentRecordingNum - 1) details.AppendLine("{\\fs25");
                if (!run.noTestMetrics)
                {
                    details.AppendLine("Test results:");
                    details.AppendLine($"Average error: {run.avarageTestError}");
                    details.AppendLine($"Max error: {run.maxTestError}");
                    details.AppendLine($"Min error: {run.minTestError}");
                }
                else
                {
                    details.AppendLine("Ne testing");
                }
                details.AppendLine("\\par");
            }
            details.AppendLine("}}");
            return details.ToString();
        }
    }
}
