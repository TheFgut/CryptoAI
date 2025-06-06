﻿
using System.Windows.Forms.DataVisualization.Charting;

namespace CryptoAI_Upgraded
{
    public static class Helpers
    {
        public static class Normalization
        {
            /// <summary>
            /// converts data values in range between -1 and 1.
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static double[,,] Normalize(out double min, out double max, double[,,] data)
            {
                min = data.Cast<double>().Min(); // Минимум в массиве
                max = data.Cast<double>().Max(); // Максимум в массиве

                int dim0 = data.GetLength(0);
                int dim1 = data.GetLength(1);
                int dim2 = data.GetLength(2);

                double[,,] normalizedData = new double[dim0, dim1, dim2];

                for (int i = 0; i < dim0; i++)
                {
                    for (int j = 0; j < dim1; j++)
                    {
                        for (int k = 0; k < dim2; k++)
                        {
                            normalizedData[i, j, k] = Normalize(data[i, j, k], min, max);
                        }
                    }
                }

                return normalizedData;
            }

            public static List<double[,,]> Normalize(out double min, out double max, params double[][,,] datas)
            {
                min = double.MaxValue; // Минимум в массиве
                max = double.MinValue; // Максимум в массиве

                foreach (var data in datas)
                {
                    double loclMin = data.Cast<double>().Min();
                    double loclMax = data.Cast<double>().Max();
                    if (loclMin < min) min = loclMin;
                    if (loclMax > max) max = loclMax;
                }
                double range = max - min;

                List<double[,,]> normalizedDatas = new List<double[,,]>();
                foreach (var data in datas)
                {
                    int dim0 = data.GetLength(0);
                    int dim1 = data.GetLength(1);
                    int dim2 = data.GetLength(2);

                    double[,,] normalizedData = new double[dim0, dim1, dim2];

                    for (int i = 0; i < dim0; i++)
                    {
                        for (int j = 0; j < dim1; j++)
                        {
                            for (int k = 0; k < dim2; k++)
                            {
                                normalizedData[i, j, k] = Normalize(data[i, j, k], min, max);
                            }
                        }
                    }
                    normalizedDatas.Add(normalizedData);
                }
                return normalizedDatas;
            }

            public static double Normalize(double value, double min, double max)
            {
                //double range = max - min;
                //return value / range;
                if (max == min)
                    return 0;
                return (value - min) / (max - min);//from 0 to 1
                //return 2 * (value - min) / (max - min) - 1;//from -1 to 1
            }
            public static decimal Normalize(decimal value, decimal min, decimal max)
            {
                //double range = max - min;
                //return value / range;
                if (max == min)
                    return 0;
                return (value - min) / (max - min);//from 0 to 1
                //return 2 * (value - min) / (max - min) - 1;//from -1 to 1
            }

            public static double Denormalize(double value, double min, double max)
            {
                //double range = max - min;
                //return value * range;
                return value * (max - min) + min;//denorm from 0 to 1 
                //return ((value + 1) * (max - min) / 2 + min);//denorm from -1 to 1 
            }

            public static List<double[,]> Normalize(out double min, out double max, params double[][,] datas)
            {
                min = double.MaxValue; // Минимум в массиве
                max = double.MinValue; // Максимум в массиве

                foreach (var data in datas)
                {
                    double loclMin = data.Cast<double>().Min();
                    double loclMax = data.Cast<double>().Max();
                    if (loclMin < min) min = loclMin;
                    if (loclMax > max) max = loclMax;
                }
                double range = max - min;

                List<double[,]> normalizedDatas = new List<double[,]>();
                foreach (var data in datas)
                {
                    int dim0 = data.GetLength(0);
                    int dim1 = data.GetLength(1);

                    double[,] normalizedData = new double[dim0, dim1];

                    for (int i = 0; i < dim0; i++)
                    {
                        for (int j = 0; j < dim1; j++)
                        {
                            normalizedData[i, j] = Normalize(data[i, j], min, max);
                        }
                    }
                    normalizedDatas.Add(normalizedData);
                }
                return normalizedDatas;
            }

            public static double[] Normalize(double min, double max, params double[] data)
            {
                double[] normalized = new double[data.Length];
                for (int i = 0; i < normalized.Length;i++)
                {
                    normalized[i] = Normalize(data[i], min, max);
                }
                return normalized;
            }
            /// <summary>
            /// converts data values in range between -1 and 1.
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            public static List<double> Normalize(List<double> data)
            {
                double min = data.Min(); // Минимум в массиве
                double max = data.Max(); // Максимум в массиве
                double range = max - min;

                List<double> newData = new List<double>();

                foreach (var element in data)
                {
                    newData.Add(Normalize(element, min, max)); 
                }

                return newData;
            }
        }

        public static class ArrayValuesInjection
        {
            public static double[,] InjectValues(double[,] array, params double[] values)
            {
                int l1 = array.GetLength(0);
                int l2 = array.GetLength(1);
                double[,] resizedArr = new double[l1, l2 + values.Length];

                for (int i = 0; i < l1;i++)
                {
                    for (int k = 0; k < l2; k++)
                    {
                        resizedArr[i,k] = array[i,k];
                    }
                    for (int k = l2; k < l2 + values.Length; k++)
                    {
                        resizedArr[i, k] = values[k - l2];
                    }
                }

                return resizedArr;
            }

            public static double[,,] InjectValues(double[,,] array, params double[] values)
            {
                int l1 = array.GetLength(0);
                int l2 = array.GetLength(1);
                int l3 = array.GetLength(2);
                double[,,] resizedArr = new double[l1, l2,l3 + values.Length];

                for (int i = 0; i < l1; i++)
                {
                    for (int j = 0; j < l2; j++)
                    {
                        for (int k = 0; k < l3; k++)
                        {
                            resizedArr[i,j, k] = array[i,j, k];
                        }
                        for (int k = l3; k < l3 + values.Length; k++)
                        {
                            resizedArr[i,j, k] = values[k - l3];
                        }
                    }

                }

                return resizedArr;
            }

            public static double[,,] UpdateInput(double[,,] input, double newValue)
            {
                int timeSteps = input.GetLength(1);
                int inputCounts = input.GetLength(2);
                double[,,] newInput = new double[1, timeSteps, inputCounts];

                // Сдвигаем данные
                for (int t = 1; t < timeSteps; t++)
                {
                    for (int n = 1; n < inputCounts; n++)
                    {
                        newInput[0, t - 1, n - 1] = input[0, t - 1, n];
                    }
                    newInput[0, t - 1, inputCounts - 1] = input[0, t, inputCounts - 1];
                }

                // Добавляем новое значение
                newInput[0, timeSteps - 1, inputCounts - 1] = newValue;

                return newInput;
            }
        }

        public static class MovingAverage
        {
            /// <summary>
            /// Простая скользящая средняя (SMA).
            /// Возвращает массив той же длины: до i&lt;period−1 ставит NaN, дальше — среднее за последние period точек.
            /// </summary>
            public static double[] Simple(double[] data, int period)
            {
                double[] sma = new double[data.Length];
                double sum = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    sum += data[i];
                    if (i >= period)
                        sum -= data[i - period];

                    if (i >= period - 1)
                        sma[i] = sum / period;
                    else
                        sma[i] = data[i];
                }
                return sma;
            }

            /// <summary>
            /// Экспоненциальная скользящая средняя (EMA).
            /// alpha = 2 / (period + 1). Начальное значение EMA[0] = data[0].
            /// </summary>
            public static double[] Exponential(double[] data, int period)
            {
                double[] ema = new double[data.Length];
                if (data.Length == 0) return ema;

                double alpha = 2.0 / (period + 1);
                ema[0] = data[0];
                for (int i = 1; i < data.Length; i++)
                {
                    ema[i] = alpha * data[i] + (1 - alpha) * ema[i - 1];
                }
                return ema;
            }

            /// <summary>
            /// Взвешенная скользящая средняя (WMA).
            /// Возвращает массив той же длины: до i &lt; period–1 ставит NaN, дальше — WMA за последние period точек.
            /// </summary>
            public static double[] Weighted(double[] data, int period)
            {
                int n = data.Length;
                double[] wma = new double[n];
                double weightSum = period * (period + 1) / 2.0;

                for (int i = 0; i < n; i++)
                {
                    if (i < period - 1)
                    {
                        wma[i] = double.NaN;
                        continue;
                    }

                    double weightedSum = 0;
                    // j=0 — самая новая точка (i), вес = period;
                    // j=period−1 — самая старая точка (i−period+1), вес = 1
                    for (int j = 0; j < period; j++)
                    {
                        double value = data[i - j];
                        double weight = period - j;
                        weightedSum += value * weight;
                    }

                    wma[i] = weightedSum / weightSum;
                }

                return wma;
            }
        }

        public static class DataPlotting
        {
            public static void PlotMultipleSeries(Chart chart, string[] labels, params double[][] dataArrays)
            {
                if (labels.Length != dataArrays.Length)
                    throw new ArgumentException("Количество меток должно совпадать с количеством массивов данных.");

                // Очистим старые серии и настройки
                chart.Series.Clear();
                chart.ChartAreas.Clear();
                chart.Legends.Clear();

                // Создаем область для построения
                var area = new ChartArea("MainArea");
                chart.ChartAreas.Add(area);

                // Легенда
                var legend = new Legend("Legend");
                chart.Legends.Add(legend);

                // Цвета можно задать заранее или сгенерировать случайно
                Color[] palette = new[]
                {
            Color.Blue, Color.Red, Color.Green, Color.Orange, Color.Purple,
            Color.Brown, Color.Magenta, Color.Cyan, Color.Lime, Color.Sienna
            };

                for (int i = 0; i < dataArrays.Length; i++)
                {
                    var series = new Series(labels[i])
                    {
                        ChartType = SeriesChartType.Line,
                        ChartArea = "MainArea",
                        Legend = "Legend",
                        BorderWidth = 2,
                        Color = palette[i % palette.Length]
                    };

                    // Добавляем точки в серию
                    var data = dataArrays[i];
                    for (int x = 0; x < data.Length; x++)
                    {
                        series.Points.AddXY(x, data[x]);
                    }

                    chart.Series.Add(series);
                }

                // Настройки осей (опционально)
                area.AxisX.Title = "Індекс";
                area.AxisY.Title = "Значення";
                area.AxisY.Minimum = dataArrays.Min(arr => arr.Min());
                area.AxisY.Maximum = dataArrays.Max(arr => arr.Max());
                area.AxisY.LabelStyle.Format = "F2";
                area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            }

            public static void DisplayDataOnChart(Chart chart, double[] data, int count, string name, Color color)
            {
                // Создание и настройка ряда данных
                var series = new Series
                {
                    ChartType = SeriesChartType.Line, // Тип графика: линия
                    Color = color,
                    Name = name,
                    BorderWidth = 2
                };
                chart.Series.Add(series);

                // Добавление точек в график
                for (int i = 0; i < count; i++)
                {
                    series.Points.AddXY(i, data[i]);
                }
            }
            public static void DisplayDataOnChart(Chart chart, double[] data, string name, Color color)
            {
                // Создание и настройка ряда данных
                var series = new Series
                {
                    ChartType = SeriesChartType.Line, // Тип графика: линия
                    Color = color,
                    Name = name,
                    BorderWidth = 2
                };
                chart.Series.Add(series);

                // Добавление точек в график
                for (int i = 0; i < data.Length; i++)
                {
                    series.Points.AddXY(i, data[i]);
                }
            }
        }

        public static double[,,] ConvertListTo3DArray(List<double[,]> list)
        {
            // Получаем размеры
            int depth = list.Count;  // Количество двумерных массивов в списке
            int rows = list[0].GetLength(0);  // Количество строк в каждом двумерном массиве
            int cols = list[0].GetLength(1);  // Количество столбцов в каждом двумерном массиве

            // Создаем новый трехмерный массив
            double[,,] result = new double[depth, rows, cols];

            // Копируем данные из списка в трехмерный массив
            for (int i = 0; i < depth; i++)
            {
                double[,] currentArray = list[i];
                for (int j = 0; j < rows; j++)
                {
                    for (int k = 0; k < cols; k++)
                    {
                        result[i, j, k] = currentArray[j, k];
                    }
                }
            }

            return result;
        }
        public static double[,] ConvertListTo2DArray(List<double[]> list)
        {
            // Получаем размеры
            int depth = list.Count;  // Количество двумерных массивов в списке
            int rows = list[0].GetLength(0);  // Количество строк в каждом двумерном массиве

            // Создаем новый трехмерный массив
            double[,] result = new double[depth, rows];

            // Копируем данные из списка в трехмерный массив
            for (int i = 0; i < depth; i++)
            {
                double[] currentArray = list[i];
                for (int j = 0; j < rows; j++)
                {
                    result[i, j] = currentArray[j];
                }
            }

            return result;
        }
        public static double[,,] ConvertArrTo3DArray(double[,] arr)
        {
            // Получаем размеры
            int rows = arr.GetLength(0);  // Количество строк в каждом двумерном массиве
            int cols = arr.GetLength(1);  // Количество столбцов в каждом двумерном массиве

            // Создаем новый трехмерный массив
            double[,,] result = new double[1, rows, cols];

            // Копируем данные из списка в трехмерный массив
            double[,] currentArray = arr;
            for (int j = 0; j < rows; j++)
            {
                for (int k = 0; k < cols; k++)
                {
                    result[0, j, k] = currentArray[j, k];
                }
            }

            return result;
        }
        public static double GetPercentChange(decimal startPrice, decimal endPrice)
        {
            return (1 - (double)(endPrice / startPrice))*100;
        }

        public static float[,,] ConvertArrToFloat(double[,,] input)
        {
            int x = input.GetLength(0);
            int y = input.GetLength(1);
            int z = input.GetLength(2);

            float[,,] result = new float[x, y, z];

            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    for (int k = 0; k < z; k++)
                        result[i, j, k] = (float)input[i, j, k];

            return result;
        }
        public static float[,] ConvertArrToFloat(double[,] input)
        {
            int x = input.GetLength(0);
            int y = input.GetLength(1);

            float[,] result = new float[x, y];

            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    result[i, j] = (float)input[i, j];

            return result;
        }

        public static string FormatDuration(int totalSeconds)
        {
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            List<string> parts = new List<string>();

            if (hours > 0)
                parts.Add($"{hours}h");
            if (minutes > 0)
                parts.Add($"{minutes}m");
            if (seconds > 0 || parts.Count == 0) // если всё 0, показываем хотя бы "0 сек"
                parts.Add($"{seconds}s");

            return string.Join(" ", parts);
        }
    }
}
