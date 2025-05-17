using CryptoAI_Upgraded.AI_Training.NeuralNetworks;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.Datasets.DataWalkers
{
    public abstract class DataWalkerBase
    {
        public int fullWalkSteps { get; private set; }
        public int totalElementsCount { get; private set; }
        /// <summary>
        /// value from 0 to 1 that indicates how close we are to the end of walking
        /// </summary>
        public float walkingProgress { get; private set; }

        protected List<LocalKlinesDataset> datasets;
        protected bool finishedWalking;

        protected int datasetLength;
        protected int currentDatasetIndex = 0;
        protected int localDatasetPos = 0;

        public List<DatasetID> datasetIDs 
        { 
            get 
            {
                List <DatasetID> datasetIDsList = new List <DatasetID>();
                foreach(LocalKlinesDataset dataset in datasets)
                {
                    datasetIDsList.Add(new DatasetID(dataset));
                }
                return null; 
            } 
        }

        public DataWalkerBase(List<LocalKlinesDataset> datasets, int walkSteps)
        {
            if (datasets == null) throw new Exception("DataWalker.Construction failed. datasets cant be null");
            if (datasets.Count == 0) throw new Exception("DataWalker.Construction failed. datasets.Count should be higher than zero");
            if (walkSteps <= 0) throw new Exception("DataWalker.Construction failed. datasets.walkSteps should be higher than zero");
            datasetLength = datasets[0].LoadKlinesFromCache().data.Count;
            if (walkSteps >= datasetLength * datasets.Count) throw new Exception("DataWalker.Construction failed. datasets.walkSteps cant be higher than dataset total data fragmens");
            this.datasets = datasets;
            this.fullWalkSteps = walkSteps;
        }

        public bool isFinishedWalking() => finishedWalking;

        protected List<KLine> WalkOneStep(bool checkIfFinished)
        {
            if (finishedWalking) throw new Exception("DataWalker.Walk error. Cant walk if walking is finished");

            List<KLine> result = getAmountOfFragmentsFromCurrentPosUnsafe(fullWalkSteps);

            MovePositionOneStep();
            if (checkIfFinished) finishedWalking = checkIfFinishedWalking();

            return result;
        }

        public List<KLine> getAmountOfFragmentsFromCurrentPosUnsafe(int count)
        {
            int datasetIndexHolder = currentDatasetIndex;
            int datasetLocalPosHolder = localDatasetPos;

            List<KLine> result = new List<KLine>();

            int stepsRemaining = count;

            while (stepsRemaining > 0 && currentDatasetIndex < datasets.Count)
            {
                var currentDataset = datasets[currentDatasetIndex];
                var currentData = currentDataset.LoadKlinesFromCache().data;

                int elementsLeftInCurrentDataset = currentData.Count - localDatasetPos;
                if (elementsLeftInCurrentDataset > 0)
                {
                    // Берем минимум из оставшихся в текущем наборе и нужного количества шагов
                    int takeCount = Math.Min(elementsLeftInCurrentDataset, stepsRemaining);
                    result.AddRange(currentData.GetRange(localDatasetPos, takeCount));
                    localDatasetPos += takeCount;
                    stepsRemaining -= takeCount;
                }

                // Если текущий набор исчерпан, переходим к следующему
                if (localDatasetPos >= currentData.Count)
                {
                    localDatasetPos = 0;
                    currentDatasetIndex++;
                }
            }
            currentDatasetIndex = datasetIndexHolder;
            localDatasetPos = datasetLocalPosHolder;
            return result;
        }

        protected bool checkIfFinishedWalking(int drag = 0)
        {

            walkingProgress = currentDatasetIndex / (float)datasets.Count;
            // check if cant walk more
            if (currentDatasetIndex >= datasets.Count)//if no more datasets left
            {
                return true;
            }
            else
            {
                int datasetsLeft = datasets.Count - currentDatasetIndex;
                return (datasetsLeft * datasetLength) - localDatasetPos < fullWalkSteps + drag;
            }
        }

        protected void MovePositionOneStep()
        {
            localDatasetPos++;
            if (localDatasetPos >= datasetLength)
            {
                localDatasetPos -= datasetLength;
                currentDatasetIndex++;
            }
        }

        public virtual void ResetDataWalker()
        {
            currentDatasetIndex = 0;
            walkingProgress = 0;
            localDatasetPos = 0;
            finishedWalking = false;
        }
    }
}
