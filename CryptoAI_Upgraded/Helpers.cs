using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                return 2 * (value - min) / (max - min) - 1;
            }

            public static double Denormalize(double value, double min, double max)
            {
                //double range = max - min;
                //return value * range;
                return ((value + 1) * (max - min) / 2 + min);
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
        public static double[,] ConvertListTo3DArray(List<double[]> list)
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
            int depth = 1;  // Количество двумерных массивов в списке
            int rows = arr.GetLength(0);  // Количество строк в каждом двумерном массиве
            int cols = arr.GetLength(1);  // Количество столбцов в каждом двумерном массиве

            // Создаем новый трехмерный массив
            double[,,] result = new double[depth, rows, cols];

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
    }
}
