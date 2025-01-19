using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using System.Diagnostics;

namespace CryptoAI_Upgraded
{
    public class KlinesRequester
    {
        private BinanceRestClient binanceClient;
        private KlineInterval interval;
        private string pair;

        private const int klinesLimit = 1000;
        private const int requestInterval = 250;//4 requests per min for safe. But possible to send 20

        private readonly Dictionary<KlineInterval, int> intervalMinutesMultiplier = new Dictionary<KlineInterval, int>()
        {           
            { KlineInterval.OneMinute, 1},
            { KlineInterval.OneHour, 60 }
        };

        public KlinesRequester(string pair, KlineInterval interval)
        {
            this.pair = pair;
            this.interval = interval;
            binanceClient = new BinanceRestClient();
        }

        public async Task<List<IBinanceKline>> LoadKlinesAsync(DateTime from, DateTime to)
        {
            if (from > to) throw new Exception("KlinesRequester.LoadKlinesAsync  time interval is incorrect");
            TimeSpan difference = to - from;

            List<IBinanceKline> result = new List<IBinanceKline>();
            DateTime fromFragment = from;
            int requestsCount = getRequestCount(difference);
            do
            {
                DateTime toFragment = GetToTimeFittedInInterval(fromFragment, to ,interval);
                IEnumerable<IBinanceKline> klinesFragment = await LoadThousandKlinesAsync(fromFragment, toFragment);
                result.AddRange(klinesFragment);
                requestsCount--;
                fromFragment = toFragment.AddMinutes(intervalMinutesMultiplier[interval]);
                await Task.Delay(requestInterval);
            } while (requestsCount > 0);
            return result;
        }


        public async Task<IEnumerable<IBinanceKline>> LoadThousandKlinesAsync(DateTime from, DateTime to)
        {
            if (!CheckIfRequestFitInThousendKlines(from, to, interval)) throw new Exception($"KlinesRequester.LoadThousandKlinesAsync time interval" +
                $" is for more than {klinesLimit} klines");
            var result = await binanceClient.SpotApi.ExchangeData.GetKlinesAsync(pair, interval, from, to);

            if (result.Success) return result.Data;
            else throw new Exception("KlinesRequester.LoadThousandKlinesAsync request failed");
        }

        private bool CheckIfRequestFitInThousendKlines(DateTime from, DateTime to, KlineInterval interval)
        {
            TimeSpan difference = to - from;
            switch (interval)
            {
                case KlineInterval.OneMinute:
                    double totalMinutes = difference.TotalMinutes;
                    return totalMinutes < klinesLimit;

                case KlineInterval.OneHour:
                    double totalHours = difference.TotalHours;
                    return totalHours < klinesLimit;

                default:
                    throw new NotImplementedException($"KlinesRequester.CheckIfRequestFitInThousendKlines interval {interval} logic is not implemented");
            }
        }

        private int getRequestCount(TimeSpan difference)
        {
            switch (interval)
            {
                case KlineInterval.OneMinute:
                    double totalMinutes = difference.TotalMinutes;
                    return (int)Math.Ceiling(totalMinutes / klinesLimit);

                case KlineInterval.OneHour:
                    double totalHours = difference.TotalHours;
                    return (int)Math.Ceiling(totalHours / klinesLimit);

                default:
                    throw new NotImplementedException($"KlinesRequester.CheckIfRequestFitInThousendKlines interval {interval} logic is not implemented");
            }
        }

        private DateTime GetToTimeFittedInInterval(DateTime from, DateTime maxTo, KlineInterval interval)
        {
            TimeSpan difference = maxTo - from;
            DateTime result;
            switch (interval)
            {
                case KlineInterval.OneMinute:
                    result = from.AddMinutes(klinesLimit-1);
                    if ((result - from).TotalMinutes > difference.TotalMinutes) return maxTo;
                    return result;

                case KlineInterval.OneHour:
                    result = from.AddHours(klinesLimit-1);
                    if ((result - from).TotalHours > difference.TotalHours) return maxTo;
                    return result;

                default:
                    throw new NotImplementedException($"KlinesRequester.GetRequestDate interval {interval} logic is not implemented");
            }
        }
    }
}
