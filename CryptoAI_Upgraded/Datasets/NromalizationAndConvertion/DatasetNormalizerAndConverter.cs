using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.Datasets.NromalizationAndConvertion
{
    public class DatasetNormalizerAndConverter
    {
        public void Convert(List<LocalKlinesDataset> datasets, string savePath)
        {
            if (datasets == null || datasets.Count == 0)
                throw new ArgumentException("DatasetNormalizerAndConverter.Convert datasets cant be null or count cant be 0");


            decimal lowestPrice = decimal.MaxValue;
            decimal highestPrice = decimal.MinValue;
            foreach (var dataset in datasets)
            {
                KlinesDay data = dataset.LoadKlinesFromCache();
                IEnumerable<decimal> closePrices = data.data.Select(kline => kline.ClosePrice);
                IEnumerable<decimal> openPrices = data.data.Select(kline => kline.OpenPrice);
                IEnumerable<decimal> lowPrices = data.data.Select(kline => kline.LowPrice);
                IEnumerable<decimal> highPrices = data.data.Select(kline => kline.HighPrice);

                lowestPrice = GetLowestValue(lowestPrice, closePrices.Min(), openPrices.Min(), lowPrices.Min(), highPrices.Min());
                highestPrice = GetHighestValue(highestPrice, closePrices.Max(), openPrices.Max(), lowPrices.Max(), highPrices.Max());
            }

            NormalizationParams normalization = new NormalizationParams
            {
                normalizedGroupsMin = new Dictionary<string, decimal>
                {
                    {  "price" , lowestPrice}
                },
                normalizedGroupsMax = new Dictionary<string, decimal>
                {
                    {  "price" , highestPrice}
                }
            };

            //assigning normalization and saving to another path
            foreach (var dataset in datasets)
            {
                KlinesDay datasetDay = dataset.LoadKlinesFromCache();
                datasetDay.normalization = normalization;
                foreach (var data in datasetDay.data)
                {
                    data.OpenPrice = Helpers.Normalization.Normalize(data.OpenPrice, lowestPrice, highestPrice);
                    data.ClosePrice = Helpers.Normalization.Normalize(data.ClosePrice, lowestPrice, highestPrice);
                    data.LowPrice = Helpers.Normalization.Normalize(data.LowPrice, lowestPrice, highestPrice);
                    data.HighPrice = Helpers.Normalization.Normalize(data.HighPrice, lowestPrice, highestPrice);
                }
                string fileSavePath = $"{savePath}\\{dataset.fileName}.bson";
                dataset.SaveToAnotherLocation(fileSavePath);
            }
        }

        private decimal GetLowestValue(params decimal[] prices)
        {
            decimal lowest = decimal.MaxValue;
            foreach (var price in prices)
            {
                if (price < lowest) lowest = price;
            }
            return lowest;
        }

        private decimal GetHighestValue(params decimal[] prices)
        {
            decimal highest = decimal.MinValue;
            foreach (var price in prices)
            {
                if (price > highest) highest = price;
            }
            return highest;
        }
    }
}
