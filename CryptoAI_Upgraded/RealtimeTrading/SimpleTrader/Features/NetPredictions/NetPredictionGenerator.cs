namespace CryptoAI_Upgraded.RealtimeTrading.SimpleTrader.Features
{
    public class NetPredictionGenerator
    {
        private List<double> currentPredictions;
        private LinkedList<PredictionHistoryObj> predHistory;
        private int rememberLen;

        public NetPredictionGenerator(int rememberLen)
        {
            this.rememberLen = rememberLen;
            predHistory = new LinkedList<PredictionHistoryObj>();
        }

        public void SetCurrentPredictions(List<double> predictions)
        {
            currentPredictions = new List<double>(predictions);
        }

        public void SetCurrentPredictions(double prediction)
        {
            currentPredictions = new List<double> { prediction };
        }

        public void RecordHistory(double prevValue, double real, double prediction)
        {
            predHistory.AddFirst(new PredictionHistoryObj(prevValue, real, prediction));
            if (predHistory.Count > rememberLen) predHistory.RemoveLast();
        }

        public NetPrediction GenerateData()
        {
            return new NetPrediction(new List<double>(currentPredictions),
                new List<PredictionHistoryObj>(predHistory));
        }
    }
}
