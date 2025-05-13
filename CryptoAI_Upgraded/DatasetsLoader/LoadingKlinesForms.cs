using Binance.Net.Clients;
using Binance.Net.Enums;
using CryptoAI_Upgraded.DataSaving;
using CryptoAI_Upgraded.Datasets;

namespace CryptoAI_Upgraded
{
    public partial class LoadingKlinesForms : Form
    {
        public LoadingKlinesForms()
        {
            InitializeComponent();
            display.Text = Application.CommonAppDataPath;
            TimeIntervalBox.DataSource = Enum.GetValues(typeof(KlineInterval));
            TimeIntervalBox.SelectedIndex = 1;
            PairComboBox.DataSource = Enum.GetValues(typeof(Pair));
        }

        private async void LoadBut_Click(object sender, EventArgs e)
        {
            DateTime fromDateData = fromDate.Value;
            DateTime toDateData = toDate.Value;

            TimeSpan difference = toDateData - fromDateData;
            if(difference.TotalDays < 0)
            {
                MessageBox.Show("From -> to date is incorrect", "Input field is incorrect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Pair? selectedPair = (Pair?)PairComboBox.SelectedItem;
            await LoadDataset(fromDateData, toDateData, selectedPair == null ? Pair.ETHUSDT.ToString() :
                selectedPair.ToString(), (KlineInterval)TimeIntervalBox.SelectedItem);
        }

        private async Task LoadDataset(DateTime from, DateTime to, string pair, KlineInterval interval)
        {
            TimeSpan difference = to - from;
            display.Text = "Start loading";
            display.Text += $"Progress: 0%";
            try
            {
                KlinesRequester requester = new KlinesRequester(pair, interval, DatasetLength.Month);
                var binanceClient = new BinanceRestClient();

                DateTime loadingFrom = from;
                double dataFragments = difference.TotalDays;
                for (int i = 0; i < dataFragments;i++)
                {
                    try
                    {
                        DateTime loadingTo = loadingFrom.AddDays(1);
                        //display.Text += $"From {loadingFrom} To {loadingTo}%";
                        await LoadAndPackFragment(requester, loadingFrom, loadingTo, pair, interval);
                        display.Text += $"Progress: {Math.Round(((i + 1) / dataFragments) * 100, 1)}%";
                        loadingFrom = loadingTo;
                    }
                    catch (Exception ex)
                    {
                        display.Text += $"error: {ex.Message}";
                    }
                }
            }
            catch (Exception ex)
            {
                display.Text += $"Critical fail: {ex.Message}";
            }
        }

        private async Task LoadAndPackFragment(KlinesRequester requester, DateTime from,
            DateTime to, string pair, KlineInterval interval)
        {
            var result = await requester.LoadKlinesAsync(from, to);
            display.Text += $"Loaded: {result.Count}";
            if (result != null)
            {
                string name = $"{pair}_{interval}_{from.Month}.{from.Day}.{from.Year}";
                LocalLoaderAndSaverBSON<KlinesDay> saver = new LocalLoaderAndSaverBSON<KlinesDay>(DataPaths.datasetsPath, name);
                KlinesDay dataPacked = new KlinesDay(result, interval, pair);
                saver.Save(dataPacked);
            }
            else
            {
                display.Text += "Failed";
            }

        }
    }
}

public enum Pair
{
    ETHUSDT,
    BTCUSDT
}