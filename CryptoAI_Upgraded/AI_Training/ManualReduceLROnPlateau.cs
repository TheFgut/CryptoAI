using CryptoAI_Upgraded.AI_Training.NeuralNetworks;
using Keras;
using Keras.Callbacks;
using Microsoft.VisualBasic.Devices;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.AI_Training
{
    internal class ManualReduceLROnPlateau
    {
        public double learning_rate {  get; private set; }
        private readonly NeuralNetwork _network;
        private readonly int _patience;
        private readonly double _factor;
        private readonly double _minLr;

        private double _best;
        private int _wait;

        /// <summary>
        /// </summary>
        /// <param name="network">Экземпляр Optimizer из Keras.NET</param>
        /// <param name="patience">Сколько подряд эпох без улучшения ждать перед снижением</param>
        /// <param name="factor">Во сколько раз уменьшать lr (0.5 = вдвое)</param>
        /// <param name="minLr">Минимально допустимый lr</param>
        public ManualReduceLROnPlateau(
            NeuralNetwork network,
            int patience = 3,
            double factor = 0.5,
            double minLr = 1e-6)
        {
            _network = network;
            learning_rate = network.training_speed;
            _patience = patience;
            _factor = factor;
            _minLr = minLr;

            _best = double.PositiveInfinity;
            _wait = 0;
        }

        /// <summary>
        /// Вызывать после каждой эпохи, передавая текущую валидационную потерю.
        /// </summary>
        public void Check(double currentValLoss)
        {
            if (currentValLoss < _best)
            {
                _best = currentValLoss;
                _wait = 0;
                return;
            }

            _wait++;
            if (_wait <= _patience)
                return;

            _network.training_speed = Math.Max(_network.training_speed * _factor, _minLr);
            _wait = 0;
        }
    }
}
