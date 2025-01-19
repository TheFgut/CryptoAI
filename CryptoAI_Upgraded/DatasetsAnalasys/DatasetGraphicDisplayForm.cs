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
            else
            {
                if (datasets.Count > 0)
                {
                    displayedGraphicNum = 0;
                    Display(displayedGraphicNum);
                }
            }
        }


        private void Display(float start)
        {
            int startDatasetId = (int)Math.Floor(start);
            if(startDatasetId < start)
            {
                LocalKlinesDataset datasetToDisp1 = datasets[startDatasetId];
                LocalKlinesDataset datasetToDisp2 = datasets[startDatasetId + 1];
                List<KLine> dataToDisp1 = GetFromCacheOrLoad(datasetToDisp1).data;
                List<KLine> dataToDisp2 = GetFromCacheOrLoad(datasetToDisp2).data;

                float dif = start - startDatasetId;
                int countFromArray1 = (int)(dataToDisp1.Count * (1 - dif));
                int countFromArray2 = (int)(dataToDisp2.Count * dif);

                var part1 = dataToDisp1.Skip(dataToDisp1.Count - countFromArray1);
                var part2 = dataToDisp2.Take(countFromArray2);
                var resultArray = part1.Concat(part2).ToArray();
                Display(resultArray.ToList());
            }
            else
            {
                LocalKlinesDataset datasetToDisp = datasets[startDatasetId];
                Display(GetFromCacheOrLoad(datasetToDisp).data);
            }
            dataPagesDisp.Text = $"{Math.Round((double)start, 2)}/{datasets.Count}";
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
            displayedGraphicNum += move;
            if (displayedGraphicNum > datasets.Count - 1) displayedGraphicNum = 0;
            if (displayedGraphicNum < 0) displayedGraphicNum = datasets.Count - 1;
            Display(displayedGraphicNum);
        }
        #endregion
    }
}
