using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.Datasets.DataWalkers
{
    public class TwoOutputsDataWalker : DataWalkerBase
    {
        public int outputOne { get; protected set; }
        public int outputTwo { get; protected set; }
        public TwoOutputsDataWalker(List<LocalKlinesDataset> datasets, int outputOne, int outputTwo) : base(datasets, outputOne + outputTwo)
        {
            if (outputOne <= 0) throw new Exception("TwoOutputsDataWalker.Construction failed. datasets.outputOne should be higher than zero");
            if (outputTwo <= 0) throw new Exception("TwoOutputsDataWalker.Construction failed. datasets.outputTwo should be higher than zero");
            this.outputOne = outputOne;
            this.outputTwo = outputTwo;
        }

        public List<KLine> Walk(out List<KLine> output)
        {
            List<KLine> walkedElements = BaseWalk(true);
            output = walkedElements.GetRange(outputOne, outputTwo);
            return walkedElements.GetRange(0, outputOne);
        }
    }
}
