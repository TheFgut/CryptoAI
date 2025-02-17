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
            { KlineInterval.FiveMinutes, 5 },
            { KlineInterval.FifteenMinutes, 15 },
            { KlineInterval.ThirtyMinutes, 30 },
            { KlineInterval.OneHour, 60 },
            { KlineInterval.FourHour, 240 }

        };

        private readonly Dictionary<DatasetLength, int> datasetLengthMultiplier = new Dictionary<DatasetLength, int>()
        {
            { DatasetLength.Day, 1},
            { DatasetLength.Month, 5 }
        };

        public KlinesRequester(string pair, KlineInterval interval, DatasetLength datasetLwngth)
        {
            this.pair = pair;
            this.interval = interval;
            binanceClient = new BinanceRestClient();
            if (!intervalMinutesMultiplier.ContainsKey(interval)) 
                throw new Exception($"KlinesRequester.Creation failed. intervalMinutesMultiplier for \"{interval}\" is not assigned");
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
            double totalFMin = Math.Ceiling(difference.TotalMinutes / intervalMinutesMultiplier[interval]);
            return totalFMin < klinesLimit;
        }

        private int getRequestCount(TimeSpan difference)
        {
            double totalTimeFragments = Math.Ceiling(difference.TotalMinutes / intervalMinutesMultiplier[interval]);
            return (int)Math.Ceiling(totalTimeFragments / klinesLimit);
        }

        private DateTime GetToTimeFittedInInterval(DateTime from, DateTime maxTo, KlineInterval interval)
        {
            TimeSpan difference = maxTo - from;
            DateTime result;

            result = from.AddMinutes((klinesLimit - 1) * intervalMinutesMultiplier[interval]);//may be -1 is not good idea


            if (Math.Ceiling((result - from).TotalMinutes/ intervalMinutesMultiplier[interval]) >
                Math.Ceiling(difference.TotalMinutes/ intervalMinutesMultiplier[interval])) return maxTo;
            return result;
        }
    }
}

public enum DatasetLength
{
    Day,
    Month, 
    Year
}