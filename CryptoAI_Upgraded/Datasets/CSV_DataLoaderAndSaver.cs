using System.Globalization;
using System.Text;

namespace CryptoAI_Upgraded.Datasets
{
    public class CSV_DataLoaderAndSaver
    {
        private string filePath;
        public CSV_DataLoaderAndSaver(string filePath)
        {

        }


        public void SaveToCsv(List<KLine> klines, string filePath)
        {
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("OpenTime,OpenPrice,ClosePrice,HighPrice,LowPrice,Volume,QuoteVolume,TakerBuyBaseVolume,TakerBuyQuoteVolume,TradeCount");

            foreach (var k in klines)
            {
                csv.AppendLine($"{k.OpenTime:yyyy-MM-dd HH:mm:ss},{k.OpenPrice},{k.ClosePrice},{k.HighPrice},{k.LowPrice},{k.Volume},");
                csv.Append($"{k.QuoteVolume},{k.TakerBuyBaseVolume},{k.TakerBuyQuoteVolume},{k.TradeCount}");
            }

            File.WriteAllText(filePath, csv.ToString());
        }

        public List<KLine> LoadFromCsv(string filePath)
        {
            List<KLine> klines = new List<KLine>();

            using (var reader = new StreamReader(filePath))
            {
                string headerLine = reader.ReadLine(); // Пропускаем заголовок
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(',');

                    if (parts.Length == 6) // Убеждаемся, что формат правильный
                    {
                        klines.Add(new KLine
                        {
                            OpenTime = DateTime.Parse(parts[0]),
                            OpenPrice = decimal.Parse(parts[1], CultureInfo.InvariantCulture),
                            ClosePrice = decimal.Parse(parts[2], CultureInfo.InvariantCulture),
                            HighPrice = decimal.Parse(parts[3], CultureInfo.InvariantCulture),
                            LowPrice = decimal.Parse(parts[4], CultureInfo.InvariantCulture),
                            Volume = decimal.Parse(parts[5], CultureInfo.InvariantCulture),
                            QuoteVolume = decimal.Parse(parts[6], CultureInfo.InvariantCulture),
                            TakerBuyBaseVolume = decimal.Parse(parts[7], CultureInfo.InvariantCulture),
                            TakerBuyQuoteVolume = decimal.Parse(parts[8], CultureInfo.InvariantCulture),
                            TradeCount = decimal.Parse(parts[9], CultureInfo.InvariantCulture)
                        });
                    }
                    else throw new Exception("Invalid data format");
                }
            }

            return klines;
        }
    }
}
