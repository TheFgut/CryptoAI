using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks
{
    public class TrainingConfigData
    {
        public int runsCount {  get; set; }
        public bool stopWhenErrorRising {  get; set; }
        public double minErrorDeltaToStop {  get; set; }
        public int patienceToStop { get; set; }
        public TrainingConfigData() { }

        public static TrainingConfigData Default
        {
            get
            {
                TrainingConfigData def = new TrainingConfigData();
                def.stopWhenErrorRising = true;
                def.minErrorDeltaToStop = 0.01;
                def.patienceToStop = 3;
                return def;
            }
        }
    }
}
