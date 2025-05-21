using CryptoAI_Upgraded.DataSaving;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks
{
    public class NetworkTrainingsStats : ICloneable
    {
        private NetworkTrainingsStatsData data;
        public NetworkRunData? lastTrain => data.runs.Count > 0 ? data.runs[data.runs.Count - 1] : null;
        public NetworkRunData[] allTrains => data.runs.ToArray();

        public NetworkTrainingsStats()
        {
            data = new NetworkTrainingsStatsData();
        }

        protected NetworkTrainingsStats(NetworkTrainingsStatsData data)
        {
            this.data = data;
        }

        public NetworkTrainingsStats(string path, bool throwErrIfNoData = false)
        {
            LocalLoaderAndSaverBSON<NetworkTrainingsStatsData> loader 
                = new LocalLoaderAndSaverBSON<NetworkTrainingsStatsData>(path,"trainStats");

            NetworkTrainingsStatsData? data = null;
            try
            {
                data = loader.Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Network stats load failed due to error:\n{ex.Message}", "Network stats load failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (data == null)
            {
                if (throwErrIfNoData) throw new Exception($"No NetworkTrainingsStatsData at path {path}.\n NetworkTrainingsStats cant be created");
                data = new NetworkTrainingsStatsData();
            }
            this.data = data;
        }

        public void RecordTrainingData(NNTrainingStats analyticsCollector)
        {
            for (int i = 0; i < analyticsCollector.runsPassed; i++)
            {
                data.runs.Add(analyticsCollector.trainingRunsData[i]);
            }
        }

        public void Save(string path)
        {
            LocalLoaderAndSaverBSON<NetworkTrainingsStatsData> loader
                = new LocalLoaderAndSaverBSON<NetworkTrainingsStatsData>(path, "trainStats");
            loader.Save(data);
        }

        public NetworkRunData[] GetTrainHistory()
        {
            return data.runs.ToArray();
        }

        public object Clone()
        {
            NetworkTrainingsStats statistics = new NetworkTrainingsStats(data.Clone());
            return statistics;
        }
    }

    public class NetworkTrainingsStatsData
    {
        public List<NetworkRunData> runs { get; set; }

        public NetworkTrainingsStatsData()
        {
            if (runs == null) runs = new List<NetworkRunData>();
        }

        public NetworkTrainingsStatsData Clone()
        {
            NetworkTrainingsStatsData clone = new NetworkTrainingsStatsData();
            clone.runs = new(runs);
            return clone;
        }
    }

    public class NetworkRunData
    {
        public double averageError { get; set; }
        public double maxError { get; set; }
        public double minError { get; set; }
        public double trainSpeed {  get; set; }
        public List<DatasetID> trainingDatasetIDs { get; set; }
        public double avarageTestError { get; set; }
        public double maxTestError { get; set; }
        public double minTestError { get; set; }
        public bool noTestMetrics { get; set; }

        public NetworkRunData()
        {
            if(trainingDatasetIDs == null) trainingDatasetIDs = new List<DatasetID>();
        }
        public override string ToString()
        {
            return $"D:{trainingDatasetIDs.Count} lrSpeed:{trainSpeed.ToString("F6")} AwgTestErr:{avarageTestError.ToString("F6")}";
        }

        public static NetworkRunData Default()
        {
            NetworkRunData def = new NetworkRunData();
            def.noTestMetrics = true;
            return def;
        }
    }

    public class DatasetID
    {
        public DateTime id { get; set; }

        /// <summary>
        /// for deserialization
        /// </summary>
        public DatasetID()
        {

        }

        public DatasetID(LocalKlinesDataset dataset)
        {
            id = dataset.date;
        }
    }
}
