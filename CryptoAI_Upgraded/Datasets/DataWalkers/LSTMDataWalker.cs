using CryptoAI_Upgraded.AI_Training.NeuralNetworks;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using CryptoExchange.Net.CommonObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public int currentStep
        {
            get
            {
                int datasetFragmetsCount = datasets[0].LoadKlinesFromCache().data.Count;
                return currentDatasetIndex * datasetFragmetsCount + localDatasetPos;
            }
        }

        private FeatureType[] features;

        public LSTMDataWalker(List<LocalKlinesDataset> datasets, NNConfigData networkConfig) : base(datasets, networkConfig.inputsLen + networkConfig.outputCount)
        {
            this.nnInput = networkConfig.inputsLen;
            this.expectedOutput = networkConfig.outputCount;
            this.timeFragments = networkConfig.timeFragments;
            features = networkConfig.features;
        }

        public double[,] Walk(out double[] expectedData)
        {
            int datasetIndexContainer = currentDatasetIndex;
            int datasetPosHolder = localDatasetPos; 
            double[,] input = new double[timeFragments, (nnInput * features.Length)];
            expectedData = new double[expectedOutput];


            List<KLine> outputKlines;
            for (int fragment = 0; fragment < timeFragments; fragment++)
            {
                bool walkPerformed;
                List<KLine> inputKlines = WalkFragment(out outputKlines, out walkPerformed);
                if (!walkPerformed)
                {
                    localDatasetPos = datasetPosHolder;
                    currentDatasetIndex = datasetIndexContainer;
                    MovePositionOneStep();
                    finishedWalking = true;
                    //do not return!
                    return input;
                }

                int dataNum = 0;
                for (int element = 0; element < inputKlines.Count; element++)
                {
                    foreach (FeatureType feature in features)
                    {
                        input[fragment, dataNum] = GetFeatureValue(inputKlines[element], feature);
                        dataNum++;
                    }
                }

                if(fragment == timeFragments - expectedOutput)
                {
                    expectedData[0] = (double)outputKlines[0].ClosePrice;
                }
            }
            localDatasetPos = datasetPosHolder;
            currentDatasetIndex = datasetIndexContainer;

            MovePositionOneStep();
            finishedWalking = checkIfFinishedWalking(timeFragments);
            return input;
        }

        private double GetFeatureValue(KLine kline, FeatureType feature)
        {
            switch (feature)
            {
                case FeatureType.OpenPrice:
                    return (double)kline.OpenPrice;
                case FeatureType.ClosePrice:
                    return (double)kline.ClosePrice;
                case FeatureType.HighPrice:
                    return (double)kline.HighPrice;
                case FeatureType.LowPrice:
                    return (double)kline.LowPrice;
                case FeatureType.Volume:
                    return (double)kline.Volume;
                case FeatureType.QuoteVolume:
                    return (double)kline.QuoteVolume;
            }
            throw new Exception($"LSTMDataWalker.GetFeatureValue failed. Unexpected feature type - {feature}");
        }

        public double[,] WalkAt(int index, out double[] expectedData)
        {
            SetPosition(index);
            double[] expected;
            double[,] data = Walk(out expected);
            expectedData = expected;
            return data;
        }

        public void SetPosition(int index)
        {
            int datasetFragmetsCount = datasets[0].LoadKlinesFromCache().data.Count;
            currentDatasetIndex = (int)Math.Floor((float)index / datasetFragmetsCount);
            localDatasetPos = index - (currentDatasetIndex * datasetFragmetsCount);

            if (checkIfFinishedWalking()) throw new Exception("Index is out of range");
        }

        private List<KLine> WalkFragment(out List<KLine> output, out bool walkPerformed)
        {
            List<KLine> walkedElements = WalkOneStep(false);
            if(walkedElements.Count < nnInput + expectedOutput)
            {
                output = null;
                walkPerformed = false;
                return null;
            }
            walkPerformed = true;
            output = walkedElements.GetRange(nnInput, expectedOutput);
            return walkedElements.GetRange(0, nnInput);
        }

    }
}
