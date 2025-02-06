using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CryptoAI_Upgraded.Datasets.DataWalkers
{
    public class LSTMDataWalker : DataWalkerBase
    {
        public int nnInput { get; protected set; }
        public int expectedOutput { get; protected set; }
        public int timeFragments { get; protected set; }
        public LSTMDataWalker(List<LocalKlinesDataset> datasets, int nnInput, int expectedOutput, int timeFragments) : base(datasets, nnInput + expectedOutput)
        {
            if (nnInput <= 0) throw new Exception("LSTMDataWalker.Construction failed. datasets.nnInput should be higher than zero");
            if (expectedOutput <= 0) throw new Exception("LSTMDataWalker.Construction failed. datasets.nnOutput should be higher than zero");
            if (checkIfFinishedWalking(0)) throw new Exception("LSTMDataWalker.Construction failed. datasets length is lower than walker walk distance");
            this.nnInput = nnInput;
            this.expectedOutput = expectedOutput;
            this.timeFragments = timeFragments;
        }

        public double[,] Walk(out double[] expectedData)
        {
            int datasetIndexContainer = currentDatasetIndex;
            int datasetPosHolder = localDatasetPos; 
            double[,] input = new double[timeFragments, nnInput];
            expectedData = new double[expectedOutput];

            List<KLine> outputKlines;
            for (int fragment = 0; fragment < timeFragments; fragment++)
            {
                List<KLine> inputKlines = WalkFragment(out outputKlines);

                for (int element = 0; element < inputKlines.Count; element++)
                {
                    input[fragment, element] = Helpers.GetPercentChange(inputKlines[element].OpenPrice, inputKlines[element].ClosePrice);
                }

                if(fragment == timeFragments - expectedOutput)
                {
                    expectedData[0] = Helpers.GetPercentChange(outputKlines[0].OpenPrice, outputKlines[0].ClosePrice);
                }
            }
            localDatasetPos = datasetPosHolder;
            currentDatasetIndex = datasetIndexContainer;

            MovePositionOneStep();

            finishedWalking = checkIfFinishedWalking(timeFragments);
            return input;
        }

        public double[,] WalkAt(int index, out double[] expectedData)
        {
            localDatasetPos = index;
            int datasetFragmetsCount = datasets[currentDatasetIndex].LoadKlinesFromCache().data.Count;
            currentDatasetIndex = (int)Math.Floor((decimal)localDatasetPos / datasetFragmetsCount);
            if (checkIfFinishedWalking()) throw new Exception("Index is out of range");
            double[] expected;
            double[,] data = Walk(out expected);
            expectedData = expected;
            return data;
        }

        private List<KLine> WalkFragment(out List<KLine> output)
        {
            List<KLine> walkedElements = WalkOneStep(false);
            output = walkedElements.GetRange(nnInput, expectedOutput);
            return walkedElements.GetRange(0, nnInput);
        }

    }
}
