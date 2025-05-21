namespace CryptoAI_Upgraded.RealtimeTrading.SimpleTrader.Features
{
    public class NetPrediction
    {
        public List<double> currentPredictions { get; set; }
        public List<PredictionHistoryObj> predHistory { get; set; }

        public NetPrediction(List<double> currentPredictions, List<PredictionHistoryObj> predHistory)
        {
            this.currentPredictions = currentPredictions;
            this.predHistory = predHistory;
        }
    }

    public class PredictionHistoryObj
    {
        public double prediction { get; private set; }
        public double realValue { get; private set; }
        public bool directionGuessedCorrectly {  get; private set; }
        public bool upTendention { get; private set; }

        public PredictionHistoryObj(double prevValue, double realValue, double prediction)
        {
            this.prediction = prediction;
            this.realValue = realValue;
            directionGuessedCorrectly = Math.Sign(realValue - prevValue) == Math.Sign(realValue - prediction);
            upTendention = realValue - prevValue > 0;
        }
    }
}
