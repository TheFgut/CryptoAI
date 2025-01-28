using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public static double[,,] Normalize(double[,,] data)
            {
                double min = data.Cast<double>().Min(); // Минимум в массиве
                double max = data.Cast<double>().Max(); // Максимум в массиве
                double range = max - min;

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
                            normalizedData[i, j, k] = data[i, j, k] / range;
                        }
                    }
                }

                return normalizedData;
            }

            public static List<double[,,]> Normalize(params double[][,,] datas)
            {
                double min = double.MaxValue; // Минимум в массиве
                double max = double.MinValue; // Максимум в массиве

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
                                normalizedData[i, j, k] = data[i, j, k] / range;
                            }
                        }
                    }
                    normalizedDatas.Add(normalizedData);
                }
                return normalizedDatas;
            }

            public static List<double[,]> Normalize(params double[][,] datas)
            {
                double min = double.MaxValue; // Минимум в массиве
                double max = double.MinValue; // Максимум в массиве

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
                            normalizedData[i, j] = data[i, j] / range;
                        }
                    }
                    normalizedDatas.Add(normalizedData);
                }
                return normalizedDatas;
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
                    newData.Add(element / range);
                }

                return newData;
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
    }
}
