using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.Datasets.DataWalkers
{
    public class DataWalker : DataWalkerBase
    {
        public DataWalker(List<LocalKlinesDataset> datasets, int walkSteps) : base(datasets, walkSteps)
        {
        }

        public List<KLine> Walk() => WalkOneStep(true);

    }
}
