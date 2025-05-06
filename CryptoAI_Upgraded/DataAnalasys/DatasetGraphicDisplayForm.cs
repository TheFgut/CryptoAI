using CryptoAI_Upgraded.DataSaving;
using CryptoAI_Upgraded.Datasets;
using CryptoAI_Upgraded.DatasetsManaging.DataLocalChoosing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CryptoAI_Upgraded.DatasetsAnalasys
{
    public partial class DatasetGraphicDisplayForm : Form
    {
        private List<LocalKlinesDataset> datasets;
        private Dictionary<LocalKlinesDataset, KlinesDay> loadedKlinesCache;
        private float displayedGraphicNum;

        private float scale = 1;
        private const float bigMove = 1;
        private const float smallMove = 0.2f;

        public DatasetGraphicDisplayForm(List<LocalKlinesDataset> datasets)
        {
            if (datasets == null) throw new Exception("DatasetGraphicDisplayForm.Contsruct failed. datasets cant be null");
            this.datasets = datasets;
            loadedKlinesCache = new Dictionary<LocalKlinesDataset, KlinesDay>();
            InitializeComponent();
            if (datasets.Count <= 1)
            {
                JumpLeftBut.Enabled = false;
                GoLeftBut.Enabled = false;
                JumpRightBut.Enabled = false;
                GoRightBut.Enabled = false;
            }
            if (datasets.Count > 0)
            {
                displayedGraphicNum = 0;
                Display(displayedGraphicNum);
            }
            //InfoWindow info = new InfoWindow("");
            //if(datasets.Count != 0) info.Show(datasets[0].LoadKlinesFromCache().data);
            //info.Show();
        }


        private void Display(float start)
        {
            int startDatasetId = (int)Math.Floor(start);
            float fractionalStart = start - startDatasetId;

            List<KLine> resultArray = new List<KLine>();

            // Сколько дней нужно отобразить с учётом масштаба
            float datasetsToDisplay = scale;

            int fullDatasetsToLoad = (int)Math.Ceiling(fractionalStart + datasetsToDisplay);

            for (int i = 0; i < fullDatasetsToLoad; i++)
            {
                int currentId = startDatasetId + i;
                if (currentId >= datasets.Count)
                    break;

                var currentDataset = datasets[currentId];
                var klines = GetFromCacheOrLoad(currentDataset).data;
                resultArray.AddRange(klines);
            }

            // Если есть дробная часть старта, нужно обрезать начало
            if (fractionalStart > 0)
            {
                int totalPoints = resultArray.Count;
                int pointsToSkip = (int)(totalPoints * fractionalStart / fullDatasetsToLoad);
                resultArray = resultArray.Skip(pointsToSkip).ToList();
            }

            // Если загружаем больше, чем scale позволяет, обрезаем
            int expectedPoints = (int)(resultArray.Count * (scale / fullDatasetsToLoad));
            if (resultArray.Count > expectedPoints)
            {
                resultArray = resultArray.Take(expectedPoints).ToList();
            }

            Display(resultArray);

            dataPagesDisp.Text = $"{Math.Round((double)start, 2)}/{datasets.Count} (scale: {scale:F1})";
        }

        /// <summary>
        /// loads graphic from cache or loads from local and saves to cache
        /// </summary>
        /// <returns></returns>
        private KlinesDay GetFromCacheOrLoad(LocalKlinesDataset datasetToLoad)
        {
            KlinesDay? result;
            if (loadedKlinesCache.TryGetValue(datasetToLoad, out result))
            {
                return result;
            }
            result = datasetToLoad.LoadKlinesIndependant();
            loadedKlinesCache.Add(datasetToLoad, result);
            return result;
        }

        public void Display(List<KLine> data)
        {
            Chart chart1 = courseGraphic;

            // Настройка Chart
            chart1.Series.Clear(); // Очистка существующих серий

            // Добавляем серию данных
            var series = new Series
            {
                ChartType = SeriesChartType.Line, // Тип графика - линия
                BorderWidth = 2 // Толщина линии
            };
            chart1.Series.Add(series);

            for (int i = 0; i < data.Count; i++)
            {
                series.Points.AddXY(i, data[i].HighPrice);
            }
            // Найти максимальное и минимальное значение данных
            decimal max = data.Select(obj => obj.HighPrice).Max();
            decimal min = data.Select(obj => obj.HighPrice).Min();

            // Вычисляем среднее значение и симметричный диапазон
            double center = (double)(max + min) / 2.0;
            double range = (double)(max - min) / 2.0;

            // Увеличиваем диапазон для лучшей видимости
            range *= 1.2; // Например, на 20% больше

            // Настройка области построения графика
            var chartArea = chart1.ChartAreas[0];
            chartArea.AxisY.Minimum = center - range;
            chartArea.AxisY.Maximum = center + range;
            chartArea.BorderWidth = 0; // Убираем рамку области построения
            chartArea.BorderColor = System.Drawing.Color.Transparent; // Прозрачный цвет

            // Убираем легенду
            chart1.Legends.Clear();

            // Убираем заголовки
            chart1.Titles.Clear();
        }
        #region controls graphic

        private void JumpLeftBut_Click(object sender, EventArgs e) => MoveGraphic(-bigMove);
        private void JumpRightBut_Click(object sender, EventArgs e) => MoveGraphic(bigMove);
        private void GoLeftBut_Click(object sender, EventArgs e) => MoveGraphic(-smallMove);
        private void GoRight_Click(object sender, EventArgs e) => MoveGraphic(smallMove);

        private void MoveGraphic(float move)
        {
            float newPosition = displayedGraphicNum + move;
            if (newPosition < 0 || newPosition > datasets.Count - scale)
                return;
            displayedGraphicNum = newPosition;
            Display(displayedGraphicNum);
        }
        #endregion

        private void ZoomInBut_Click(object sender, EventArgs e) => ZoomIn();

        private void ZoomOutBut_Click(object sender, EventArgs e) => ZoomOut();

        private void ZoomIn()
        {
            scale /= 1.5f;
            if (scale < 0.2f) scale = 0.2f;
            if (displayedGraphicNum > datasets.Count - scale)
                displayedGraphicNum = datasets.Count - scale;
            Display(displayedGraphicNum);
        }

        private void ZoomOut()
        {
            scale *= 1.5f;
            if (scale > datasets.Count) scale = datasets.Count;
            Display(displayedGraphicNum);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (ModifierKeys.HasFlag(Keys.Control))
            {
                if (e.Delta > 0)
                    ZoomIn();
                else
                    ZoomOut();
            }
            else
            {
                float moveAmount = (e.Delta > 0 ? -smallMove : smallMove);
                MoveGraphic(moveAmount);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    MoveGraphic(-smallMove);
                    return true;
                case Keys.Right:
                    MoveGraphic(smallMove);
                    return true;
                case Keys.PageUp:
                    MoveGraphic(-bigMove);
                    return true;
                case Keys.PageDown:
                    MoveGraphic(bigMove);
                    return true;
                case Keys.OemMinus:
                    ZoomOut();
                    return true;
                case Keys.Oemplus:
                    ZoomIn();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
