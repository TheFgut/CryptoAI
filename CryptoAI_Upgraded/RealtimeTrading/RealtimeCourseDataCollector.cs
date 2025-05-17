using Binance.Net.Clients;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.RealtimeTrading
{
    internal class RealtimeCourseDataCollector
    {
        private BinanceRestClient restClient;
        private string pair;

        private Task<WebCallResult<IBinanceTick>>? tickerSubscriptionResult;
        private CancellationTokenSource? cancellationTokenSrc;
        public RealtimeCourseDataCollector(BinanceRestClient restClient, string pair)
        {
            this.restClient = restClient;
            this.pair = pair;
        }

        public async void OpenConnection(Action<double> onCourseChanged)
        {
            if (tickerSubscriptionResult != null) return;
            cancellationTokenSrc = new CancellationTokenSource();
            var ticker = await restClient.SpotApi.ExchangeData.GetTickerAsync(pair, cancellationTokenSrc.Token);
            onCourseChanged((double)ticker.Data.LastPrice);
        }

        public void CloseConnection()
        {
            if(cancellationTokenSrc == null) return;
            cancellationTokenSrc.Cancel();
            tickerSubscriptionResult = null;
            cancellationTokenSrc = null;
        }
    }
}
