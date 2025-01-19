using CryptoAI_Upgraded.DataLocalChoosing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.Datasets.DataWalkers
{
    public class LSTMDataWalker : DataWalkerBase
    {
        public int nnInput { get; protected set; }
        public int nnOutput { get; protected set; }
        public int timeFragments { get; protected set; }
        public LSTMDataWalker(List<LocalKlinesDataset> datasets, int nnInput, int nnOutput, int timeFragments) : base(datasets, nnInput + nnOutput)
        {
            if (nnInput <= 0) throw new Exception("LSTMDataWalker.Construction failed. datasets.nnInput should be higher than zero");
            if (nnOutput <= 0) throw new Exception("LSTMDataWalker.Construction failed. datasets.nnOutput should be higher than zero");
            if (checkIfFinishedWalking(timeFragments)) throw new Exception("LSTMDataWalker.Construction failed. datasets length is lower than walker walk distance");
            this.nnInput = nnInput;
            this.nnOutput = nnOutput;
            this.timeFragments = timeFragments;
        }

        public double[,] Walk(out double[,] expectedData)
        {
            int datasetIndexContainer = currentDatasetIndex;
            double[,] input = new double[timeFragments, nnInput];
            expectedData = new double[timeFragments, nnOutput];

            for (int fragment = 0; fragment < timeFragments; fragment++)
            {
                List<KLine> outputKlines;
                List<KLine> inputKlines = WalkFragment(out outputKlines);

                double[] dataI = inputKlines.Select(k => (double)(k.ClosePrice - k.OpenPrice)).ToArray();
                for (int element = 0; element < dataI.Length; element++)
                {
                    input[fragment, element] = dataI[element];
                }

                double[] dataO = outputKlines.Select(k => (double)(k.ClosePrice - k.OpenPrice)).ToArray();
                for (int element = 0; element < dataO.Length; element++)
                {
                    expectedData[fragment, element] = dataO[element];
                }

            }
            List<KLine> walkedElements = BaseWalk(true);

            currentDatasetIndex = datasetIndexContainer;
            finishedWalking = checkIfFinishedWalking(timeFragments);
            return input;
        }

        private List<KLine> WalkFragment(out List<KLine> output)
        {
            List<KLine> walkedElements = BaseWalk(false);
            output = walkedElements.GetRange(nnInput, nnOutput);
            return walkedElements.GetRange(0, nnInput);
        }

    }
}
