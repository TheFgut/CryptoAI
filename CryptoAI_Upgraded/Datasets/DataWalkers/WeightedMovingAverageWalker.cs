using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.Datasets.DataWalkers
{
    internal class WeightedMovingAverageWalker
    {
        private readonly LinkedList<double> buffer;
        private readonly int period;
        private readonly double weightSum;

        /// <summary>
        /// Создаёт WMA-калькулятор с указанным периодом.
        /// </summary>
        /// <param name="period">Число последних значений для расчёта WMA (должно быть ≥1).</param>
        public WeightedMovingAverageWalker(int period)
        {
            if (period < 1)
                throw new ArgumentException("Period must be at least 1", nameof(period));

            this.period = period;
            this.buffer = new LinkedList<double>();
            this.weightSum = period * (period + 1) / 2.0;
        }

        public double Next(double data)
        {
            buffer.AddLast(data);
            if (buffer.Count > period) buffer.RemoveFirst();
            else if (buffer.Count != period) return 0.0;

            int counter = 0;
            double weightedSum = 0;
            foreach (var item in buffer)
            {
                double weight = period - counter;
                weightedSum += item * weight;
                counter++;
            }
            return weightedSum / weightSum;
        }

        public void Reset()
        {
            buffer.Clear();
        }
    }
}
