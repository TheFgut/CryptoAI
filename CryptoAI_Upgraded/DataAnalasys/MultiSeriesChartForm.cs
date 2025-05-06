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

namespace CryptoAI_Upgraded.DataAnalasys
{
    public partial class MultiSeriesChartForm : Form
    {
        /// <summary>
        /// Конструктор принимает произвольное количество массивов данных.
        /// </summary>
        public MultiSeriesChartForm(params double[][] dataArrays)
        {
            InitializeComponent();
            SetupChart();
            PlotData(dataArrays);
        }

        private void SetupChart()
        {
            // создаём область построения
            var area = new ChartArea("MainArea")
            {
                // включаем возможность выделения для зума
                CursorX = { IsUserEnabled = true, IsUserSelectionEnabled = true },
                CursorY = { IsUserEnabled = true, IsUserSelectionEnabled = true },
                // делаем сам масштабируемым
                AxisX = { ScaleView = { Zoomable = true }, ScrollBar = { IsPositionedInside = true } },
                AxisY = { ScaleView = { Zoomable = true }, ScrollBar = { IsPositionedInside = true } }
            };
            // опционально: сетка пунктиром
            area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            DataChart.ChartAreas.Add(area);
            DataChart.Legends.Add(new Legend("Legend"));
        }

        private void PlotData(double[][] dataArrays)
        {
            DataChart.Series.Clear();
            Color[] palette = {
            Color.Blue, Color.Red, Color.Green, Color.Orange, Color.Purple,
            Color.Brown, Color.Magenta, Color.Cyan, Color.Lime, Color.Sienna
            };

            for (int i = 0; i < dataArrays.Length; i++)
            {
                var series = new Series($"Series {i + 1}")
                {
                    ChartType = SeriesChartType.Line,
                    ChartArea = "MainArea",
                    Legend = "Legend",
                    BorderWidth = 2,
                    Color = palette[i % palette.Length]
                };

                var data = dataArrays[i];
                for (int x = 0; x < data.Length; x++)
                    series.Points.AddXY(x, data[x]);

                DataChart.Series.Add(series);
            }
        }

        private void YZoomUp_Click(object sender, EventArgs e) => ZoomAxisY(1.25);
        private void YZoomDown_Click(object sender, EventArgs e) => ZoomAxisY(0.8);
        private void XZoomUp_Click(object sender, EventArgs e) => ZoomAxisX(1.25);
        private void XZoomDown_Click(object sender, EventArgs e) => ZoomAxisX(0.8);
        
        // Вспомогательные методы
        private void ZoomAxisX(double factor)
        {
            var area = DataChart.ChartAreas["MainArea"];
            // если нет текущего зума, берём полные границы
            double min = area.AxisX.ScaleView.IsZoomed ? area.AxisX.ScaleView.ViewMinimum : area.AxisX.Minimum;
            double max = area.AxisX.ScaleView.IsZoomed ? area.AxisX.ScaleView.ViewMaximum : area.AxisX.Maximum;
            double span = max - min;
            double newSpan = span * factor;
            double center = (min + max) / 2;
            double from = Math.Max(area.AxisX.Minimum, center - newSpan / 2);
            double to = Math.Min(area.AxisX.Maximum, center + newSpan / 2);
            area.AxisX.ScaleView.Zoom(from, to);
        }

        private void ZoomAxisY(double factor)
        {
            var area = DataChart.ChartAreas["MainArea"];
            double min = area.AxisY.ScaleView.IsZoomed ? area.AxisY.ScaleView.ViewMinimum : area.AxisY.Minimum;
            double max = area.AxisY.ScaleView.IsZoomed ? area.AxisY.ScaleView.ViewMaximum : area.AxisY.Maximum;
            double span = max - min;
            double newSpan = span * factor;
            double center = (min + max) / 2;
            double from = Math.Max(area.AxisY.Minimum, center - newSpan / 2);
            double to = Math.Min(area.AxisY.Maximum, center + newSpan / 2);
            area.AxisY.ScaleView.Zoom(from, to);
        }
    }
}
