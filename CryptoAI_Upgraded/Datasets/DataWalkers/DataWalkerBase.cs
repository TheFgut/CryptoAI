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
        private List<LocalKlinesDataset> datasets;
        protected bool finishedWalking;

        protected int currentDatasetIndex = 0;
        protected int currentIndexInDataset = 0;

        public DataWalkerBase(List<LocalKlinesDataset> datasets, int walkSteps)
        {
            if (datasets == null) throw new Exception("DataWalker.Construction failed. datasets cant be null");
            if (datasets.Count == 0) throw new Exception("DataWalker.Construction failed. datasets.Count should be higher than zero");
            if (walkSteps <= 0) throw new Exception("DataWalker.Construction failed. datasets.walkSteps should be higher than zero");
            int datasetLength = datasets[0].LoadKlinesFromCache().data.Count;
            if(walkSteps >= datasetLength * datasets.Count) throw new Exception("DataWalker.Construction failed. datasets.walkSteps cant be higher than dataset total data fragmens");
            this.datasets = datasets;
            this.fullWalkSteps = walkSteps;
        }

        public bool isFinishedWalking() => finishedWalking;

        protected List<KLine> BaseWalk(bool checkIfFinished)
        {
            if (finishedWalking) throw new Exception("DataWalker.Walk error. Cant walk if walking is finished");

            List<KLine> result = new List<KLine>();

            int stepsRemaining = fullWalkSteps;

            while (stepsRemaining > 0 && currentDatasetIndex < datasets.Count)
            {
                var currentDataset = datasets[currentDatasetIndex];
                var currentData = currentDataset.LoadKlinesFromCache().data;

                int elementsLeftInCurrentDataset = currentData.Count - currentIndexInDataset;
                if (elementsLeftInCurrentDataset > 0)
                {
                    // Берем минимум из оставшихся в текущем наборе и нужного количества шагов
                    int takeCount = Math.Min(elementsLeftInCurrentDataset, stepsRemaining);
                    result.AddRange(currentData.GetRange(currentIndexInDataset, takeCount));
                    currentIndexInDataset += takeCount;
                    stepsRemaining -= takeCount;
                }

                // Если текущий набор исчерпан, переходим к следующему
                if (currentIndexInDataset >= currentData.Count)
                {
                    currentIndexInDataset = 0;
                    currentDatasetIndex++;
                }
            }

            if(checkIfFinished) finishedWalking = checkIfFinishedWalking();

            return result;
        }

        protected bool checkIfFinishedWalking(int drag = 0)
        {
            // check if cant walk more
            if (currentDatasetIndex > datasets.Count - 1)//if no more datasets left
            {
                return true;
            }
            else
            {
                int datasetFragmetsCount = datasets[currentDatasetIndex].LoadKlinesFromCache().data.Count;
                int datasetsLeft = currentDatasetIndex - datasets.Count;
                if (datasetsLeft > 1)
                {
                    return datasetsLeft * datasetFragmetsCount - currentIndexInDataset < fullWalkSteps + drag;
                }
                else//we are on the last dataset
                {
                    return datasetFragmetsCount - currentIndexInDataset < fullWalkSteps + drag;
                }
            }
        }

        public virtual void ResetDataWalker()
        {
            currentDatasetIndex = 0;
            currentIndexInDataset = 0;
            finishedWalking = false;
        }
    }
}
