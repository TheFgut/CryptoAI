using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net;
using Binance.Net.Clients;
using Binance.Net.Enums;
using CryptoAI_Upgraded.Datasets;
using CryptoExchange.Net.Objects.Sockets;

namespace CryptoAI_Upgraded.DatasetsLoader
{
    internal class BinanceKlineSeries : IDisposable
    {
        private readonly string _symbol;
        private readonly KlineInterval _interval;
        private readonly int _windowSize;
        private readonly Queue<KLine> _candles;       // Очередь для хранения последних N завершённых свечей
        private readonly BinanceSocketClient _socketClient;
        private readonly BinanceRestClient _restClient;
        private readonly object _lock = new object();      // Объект для блокировки доступа к _candles (потокобезопасность)

        /// <summary>Событие, вызываемое при добавлении новой завершённой свечи (закрытии свечи).</summary>
        public event Action<KLine>? CandleClosed;

        /// <summary>
        /// Конструктор принимает торговую пару, интервал свечи и размер окна.
        /// При инициализации загружает исторические данные и подключается к WebSocket-каналу свечей.
        /// </summary>
        /// <param name="symbol">Торговая пара (например, "BTCUSDT").</param>
        /// <param name="interval">Интервал свечей (строка, например "1m", "5m", "1h").</param>
        /// <param name="windowSize">Количество последних завершённых свечей для хранения.</param>
        public BinanceKlineSeries(string symbol, KlineInterval interval, int windowSize)
        {
            _symbol = symbol;
            _interval = interval;
            _windowSize = windowSize;
            _candles = new Queue<KLine>(windowSize);
            _restClient = new BinanceRestClient();
            _socketClient = new BinanceSocketClient();

            // 1. Получаем начальную историю последних N завершённых свечей через REST API
            var historyResult = _restClient.SpotApi.ExchangeData.GetKlinesAsync(
                                    _symbol, _interval, limit: _windowSize).GetAwaiter().GetResult();
            if (historyResult.Success)
            {
                var klines = historyResult.Data;
                // Преобразуем полученные свечи в наш формат CandleData и заполняем очередь
                foreach (var k in klines)
                {
                    // Проверяем флаг завершенности свечи (в REST данные обычно все завершенные)
                    // В Binance REST API текущая последняя свеча может быть незавершённой, игнорируем её если она неполная
                    // Предполагаем, что все возвращённые limit свечей завершены.
                    KLine candle = new KLine
                    {
                        OpenTime = k.OpenTime,
                        //CloseTime = k.CloseTime,
                        OpenPrice = k.OpenPrice,
                        HighPrice = k.HighPrice,
                        LowPrice = k.LowPrice,
                        ClosePrice = k.ClosePrice,
                        Volume = k.Volume,
                        QuoteVolume = k.QuoteVolume,
                        TradeCount = k.TradeCount,
                        TakerBuyBaseVolume = k.TakerBuyBaseVolume,
                        TakerBuyQuoteVolume = k.TakerBuyQuoteVolume
                    };
                    _candles.Enqueue(candle);
                }
            }
            else
            {
                // Если не удалось получить историю (например, проблемы с сетью),
                // можно обработать ошибку или бросить исключение
                throw new Exception($"Не удалось загрузить исторические свечи: {historyResult.Error}");
            }

            // 2. Подписываемся на WebSocket поток обновлений свечей для заданного символа и интервала
            // BinanceSocketClient предоставляет асинхронный метод подписки. Выполняем его и ожидаем результат.
            var subscribeResult = _socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(
                new string[] { _symbol }, new KlineInterval[] { _interval }, HandleKlineUpdate).GetAwaiter().GetResult();
            if (!subscribeResult.Success)
            {
                // Обработка ошибки подписки на веб-сокет (например, выброс исключения)
                throw new Exception($"Ошибка подписки на поток свечей: {subscribeResult.Error}");
            }
        }

        /// <summary>
        /// Обработчик обновлений свечей (вызывается при каждом новом событии по веб-сокету).
        /// Внутри фильтрует завершение свечи и обновляет временной ряд.
        /// </summary>
        private void HandleKlineUpdate(DataEvent<Binance.Net.Interfaces.IBinanceStreamKlineData> data)
        {
            // В IBinanceStreamKlineData есть свойство .Kline
            var k = data.Data.Data;

            // Проверяем, что это именно завершённая свеча
            if (!k.Final)
                return;

            // Собираем наш объект CandleData
            KLine newCandle = new KLine
            {
                OpenTime = k.OpenTime,
                //CloseTime = k.CloseTime,
                OpenPrice = k.OpenPrice,
                HighPrice = k.HighPrice,
                LowPrice = k.LowPrice,
                ClosePrice = k.ClosePrice,
                Volume = k.Volume,
                QuoteVolume = k.QuoteVolume,
                TradeCount = k.TradeCount,
                TakerBuyBaseVolume = k.TakerBuyBaseVolume,
                TakerBuyQuoteVolume = k.TakerBuyQuoteVolume
            };

            lock (_lock)
            {
                if (_candles.Count >= _windowSize)
                    _candles.Dequeue();
                _candles.Enqueue(newCandle);
            }

            CandleClosed?.Invoke(newCandle);
        }


        /// <summary>
        /// Метод для получения копии текущего списка завершённых свечей.
        /// Возвращает новый List, содержащий объекты CandleData, соответствующие всем хранимым свечам.
        /// </summary>
        public List<KLine> GetSeries()
        {
            lock (_lock)
            {
                return _candles.ToList();
            }
            return null;
        }

        /// <summary>
        /// Реализация IDisposable для закрытия подключений при уничтожении объекта.
        /// Вызывает Dispose у BinanceSocketClient и BinanceClient.
        /// </summary>
        public void Dispose()
        {
            _socketClient?.Dispose();
            _restClient?.Dispose();
        }
    }
}
