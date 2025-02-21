using Binance.Net.Enums;
using CryptoAI_Upgraded.DataSaving;
using CryptoAI_Upgraded.Datasets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing
{
    public class LocalKlinesDataset
    {
        public string pair { get; private set; }
        public KlineInterval interval { get; private set; }
        public DateTime date { get; private set; }
        public string filePath { get; private set; }
        public string fileName { get; private set; }

        private KlinesDay? cachedKlines;
        public LocalKlinesDataset(string filePath)
        {
            GetAllNecessaryDataFromFilepath(filePath);
        }

        public KlinesDay LoadKlinesFromCache()
        {
            if (cachedKlines != null) return cachedKlines;
            LocalLoaderAndSaverBSON<KlinesDay> loader = new LocalLoaderAndSaverBSON<KlinesDay>(filePath);
            KlinesDay? dayData = loader.Load();
            if (dayData == null) throw new Exception($"LocalKlinesDataset LoadKlines failed from path {loader.fullPath}");
            cachedKlines = dayData;
            return dayData;
        }

        public void SaveToAnotherLocation(string filePath)
        {
            if (cachedKlines == null) throw new Exception("LocalKlinesDataset.Unable to save to another location because dataset is not loaded to cache");
            GetAllNecessaryDataFromFilepath(filePath);
            LocalLoaderAndSaverBSON<KlinesDay> loader = new LocalLoaderAndSaverBSON<KlinesDay>(filePath);
            loader.Save(cachedKlines);
        }

        /// <summary>
        /// load klines and not saving it in cache
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public KlinesDay LoadKlinesIndependant()
        {
            LocalLoaderAndSaverBSON<KlinesDay> loader = new LocalLoaderAndSaverBSON<KlinesDay>(filePath);
            KlinesDay? dayData = loader.Load();
            if (dayData == null) throw new Exception("LocalKlinesDataset LoadKlines failed");
            return dayData;
        }

        private void GetAllNecessaryDataFromFilepath(string filePath)
        {
            this.filePath = filePath;
            string[] elements = filePath.Split('\\').Last().Split('_');
            pair = elements[0];
            if (!Enum.TryParse(elements[1], out KlineInterval interval))
                throw new Exception("LocalKlinesDataset.Construction interval parse failed");
            this.interval = interval;

            int lastDotIndex = elements[2].LastIndexOf('.');
            string dateWithoutExt = lastDotIndex > 0 ? elements[2].Substring(0, lastDotIndex) : elements[2];
            string[] formats = { "M.d.yyyy", "MM.dd.yyyy" };
            if (!DateTime.TryParseExact(dateWithoutExt, formats, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime date))
                throw new Exception($"LocalKlinesDataset.Construction date \"{dateWithoutExt}\" parse failed");
            fileName = Path.GetFileNameWithoutExtension(filePath);
            this.date = date;
        }
    }
}
