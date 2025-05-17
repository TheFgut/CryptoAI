using CryptoAI_Upgraded.AI_Training.NeuralNetworks;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;

namespace CryptoAI_Upgraded.Datasets.DataWalkers
{
    public class LSTMDataWalker : DataWalkerBase
    {
        public int nnInput { get; protected set; }
        public int expectedOutput { get; protected set; }
        public int windowLen { get; protected set; }
        public int currentStep
        {
            get
            {
                int datasetFragmetsCount = datasets[0].LoadKlinesFromCache().data.Count;
                return currentDatasetIndex * datasetFragmetsCount + localDatasetPos;
            }
        }

        public KLine position
        {
            get
            {
                int localPos = localDatasetPos + windowLen + nnInput - 1;
                var currentDataset = datasets[currentDatasetIndex];
                var currentData = currentDataset.LoadKlinesFromCache().data;
                while (localPos >= currentData.Count)
                {
                    localPos -= currentData.Count;
                    if (currentDatasetIndex + 1 >= datasets.Count) throw new Exception("position is out of bounds");
                    currentDataset = datasets[currentDatasetIndex + 1];
                    currentData = currentDataset.LoadKlinesFromCache().data;
                }
                return currentData[localPos];
            }
        }

        private FeatureType[] features;

        public LSTMDataWalker(List<LocalKlinesDataset> datasets, NNConfigData networkConfig) : base(datasets, networkConfig.inputsLen + networkConfig.outputCount)
        {
            this.nnInput = networkConfig.inputsLen;
            this.expectedOutput = networkConfig.outputCount;
            this.windowLen = networkConfig.window;
            features = networkConfig.features;
        }

        public double[,] Walk(out double[] expectedData)
        {
            int datasetIndexContainer = currentDatasetIndex;
            int datasetPosHolder = localDatasetPos; 
            double[,] input = new double[windowLen, (nnInput * features.Length)];
            expectedData = new double[expectedOutput];


            List<KLine> outputKlines;
            for (int windowPos = 0; windowPos < windowLen; windowPos++)
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
                        input[windowPos, dataNum] = GetFeatureValue(inputKlines[element], feature, (element + 1.0)/ inputKlines.Count);
                        dataNum++;
                    }
                }
                //at the last iteration of filling inputs getting outputs
                if(windowPos == windowLen - 1)
                {
                    for (int i = 0; i < outputKlines.Count; i++)
                    {
                        expectedData[i] = (double)outputKlines[i].ClosePrice;
                    }
                }
            }
            localDatasetPos = datasetPosHolder;
            currentDatasetIndex = datasetIndexContainer;

            MovePositionOneStep();
            finishedWalking = checkIfFinishedWalking(windowLen);
            return input;
        }

        /// <summary>
        /// should be called for data sorted by time one by one!!!!!!
        /// </summary>
        /// <param name="kline"></param>
        /// <param name="feature"></param>
        /// <param name="dataFragmentNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private double GetFeatureValue(KLine kline, FeatureType feature, double dataFragmentNum)
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
                case FeatureType.TradeCount:
                    return (double)kline.TradeCount;
                case FeatureType.QuoteVolume:
                    return (double)kline.QuoteVolume;
                case FeatureType.FragmentNum:
                    return dataFragmentNum;
                case FeatureType.PriceDelta:
                    return (double)((kline.ClosePrice - kline.OpenPrice)/ kline.OpenPrice);
                case FeatureType.CandleType:
                    return kline.ClosePrice > kline.OpenPrice ? 1 : 0;
                case FeatureType.Volatility:
                    return (double)((kline.HighPrice - kline.LowPrice) / kline.OpenPrice);
                case FeatureType.WMA7:
                    return kline.WMA7;
                case FeatureType.WMA25:
                    return kline.WMA25;
                case FeatureType.WMA99:
                    return kline.WMA99;
            }
            throw new Exception($"LSTMDataWalker.GetFeatureValue failed. Unexpected feature type - {feature}");
        }

        public double[,] WalkAt(int index, out double[] expectedData)
        {
            SetPosition(index);
            double[,] data = Walk(out expectedData);
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
