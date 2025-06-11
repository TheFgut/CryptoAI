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
        public MultiSeriesChartForm()
        {
            InitializeComponent();
            SetupChart();
        }

        private void SetupChart()
        {
            // создаём область построения
            var area = DataChart.ChartAreas["MainArea"];
            // опционально: сетка пунктиром
            area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.Position.Auto = true;
            area.InnerPlotPosition.Auto = true;

            area.CursorX.IsUserEnabled = true;
            area.CursorX.IsUserSelectionEnabled = true;
            area.CursorX.IntervalType = DateTimeIntervalType.Auto;

            area.CursorY.IsUserEnabled = true;
            area.CursorY.IsUserSelectionEnabled = true;

            area.AxisX.ScaleView.Zoomable = true; 
            area.AxisX.ScrollBar.IsPositionedInside = true;
            area.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;

            // Настройка интервала шкалы (необязательно, но полезно):
            // Например, если ваши X-значения — это целочисленные индексы или даты, 
            // можно установить Auto или вручную задать Interval:
            area.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
        }

        internal void PlotData(double[][] dataArrays)
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
                    BorderWidth = 2,
                    Color = palette[i % palette.Length]
                };

                var data = dataArrays[i];
                for (int x = 0; x < data.Length; x++)
                    series.Points.AddXY(x, data[x]);

                DataChart.Series.Add(series);
            }
        }

        internal void PlotLine(double[] dataArray, string name, Color color)
        {
            var series = new Series(name)
            {
                ChartType = SeriesChartType.Line,
                ChartArea = "MainArea",
                BorderWidth = 2,
                Color = color
            };

            for (int x = 0; x < dataArray.Length; x++)
                series.Points.AddXY(x, dataArray[x]);

            DataChart.Series.Add(series);
        }

        internal void PlotLine(Plot plot)
        {
            var series = new Series(plot.name)
            {
                ChartType = SeriesChartType.Line,
                ChartArea = "MainArea",
                BorderWidth = 2,
                Color = plot.color
            };

            for (int x = 0; x < plot.dataArray.Length; x++)
            {
                int pointIndex = series.Points.AddXY(x, plot.dataArray[x]);
                DataPoint currentPoint = series.Points[pointIndex];
                int markerIndex = plot.markers.FindIndex(m => m.index == x);
                if (markerIndex >= 0)
                {
                    Marker thisPointMarker = plot.markers[markerIndex];

                    // Назначаем стиль, цвет и размер маркера именно для этой точки
                    currentPoint.MarkerStyle = thisPointMarker.style;
                    currentPoint.MarkerSize = thisPointMarker.size;
                    currentPoint.MarkerColor = thisPointMarker.color;

                    currentPoint.MarkerBorderColor = Color.Black;
                    currentPoint.MarkerBorderWidth = 2;
                }
            }

                DataChart.Series.Add(series);
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

    public class MultiSeriesChartFormBuilder
    {
        private List<Plot> plots;

        public MultiSeriesChartFormBuilder()
        {
            plots = new();
        }

        public MultiSeriesChartFormBuilder AddPlot(double[] dataArray, string name, Color color)
        {
            plots.Add(new Plot(dataArray, name, color));
            return this;
        }

        public MultiSeriesChartFormBuilder AddMarkersToLastPlot(int[] indexes, Color color, int size = 10, MarkerStyle style = MarkerStyle.Circle)
        {
            Plot? lastPlot = plots.LastOrDefault();
            if (lastPlot == null) throw new Exception("AddMarkersToLastPlot failed. No plots added");
            foreach (var index in indexes)
            {
                lastPlot.markers.Add(new Marker()
                {
                    color = color,
                    size = size,
                    style = style,
                    index = index
                });
            }
            return this;
        }

        public MultiSeriesChartForm Build()
        {
            MultiSeriesChartForm multiSChart = new MultiSeriesChartForm();
            foreach (var plot in plots)
            {
                multiSChart.PlotLine(plot);
            }
            return multiSChart;
        }
    }

    internal class Plot
    {
        //line
        public double[] dataArray { get; set; }
        public string name { get; set; }
        public Color color { get; set; }

        //markers
        public List<Marker> markers { get; set; }

        public Plot(double[] dataArray, string name, Color color)
        {
            this.name = name;
            this.dataArray = dataArray;
            this.color = color;
            markers = new List<Marker>();
        }
    }

    internal struct Marker
    {
        public int index;
        public int size;
        public Color color;
        public MarkerStyle style;
    }
}
