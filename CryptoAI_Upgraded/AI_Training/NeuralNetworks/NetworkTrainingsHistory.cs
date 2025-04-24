using CryptoAI_Upgraded.DataSaving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks
{
    public class NetworkTrainingsStats
    {
        private NetworkTrainingsStatsData data;

        public NetworkTrainingsStats()
        {
            data = new NetworkTrainingsStatsData();
        }

        public NetworkTrainingsStats(string path)
        {
            LocalLoaderAndSaverBSON<NetworkTrainingsStatsData> loader 
                = new LocalLoaderAndSaverBSON<NetworkTrainingsStatsData>(path,"trainStats");
            NetworkTrainingsStatsData? data = loader.Load();
            if (data == null) data = new NetworkTrainingsStatsData();
        }

        public void RecordTrainingData(NNTrainingStats analyticsCollector)
        {
            for (int i = 0; i < analyticsCollector.runsPassed; i++)
            {
                data.runs.Add(analyticsCollector.trainingRunsData[i]);
            }
        }

        public void RecordTestingData(NNTrainingStats analyticsCollector)
        {

        }

        public void Save(string path)
        {
            LocalLoaderAndSaverBSON<NetworkTrainingsStatsData> loader
                = new LocalLoaderAndSaverBSON<NetworkTrainingsStatsData>(path, "trainStats");
            loader.Save(data);
        }
    }

    internal class NetworkTrainingsStatsData
    {
        public List<NetworkRunData> runs { get; set; }
        public Dictionary<int, NetworkRunData> testRuns {  get; set; }

        public NetworkTrainingsStatsData()
        {
            if (runs == null) runs = new List<NetworkRunData>();
            if (testRuns == null) testRuns = new Dictionary<int, NetworkRunData>();
        }
    }

    public class NetworkRunData
    {
        public double averageError { get; set; }
        public double maxError { get; set; }
        public double minError { get; set; }
    }
}
