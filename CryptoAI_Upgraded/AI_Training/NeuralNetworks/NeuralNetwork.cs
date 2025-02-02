using CryptoAI_Upgraded.Datasets.DataWalkers;
using Keras.Layers;
using Keras.Models;
using Numpy;
using CryptoAI_Upgraded.Datasets;
using System.Drawing.Text;
using System.Reflection;
using System.ComponentModel;
using System.Text.Json.Serialization;
using CryptoAI_Upgraded.DataSaving;
using System.IO;
using Keras.Optimizers;
using Keras;
using Keras.Initializer;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks
{
    public class NeuralNetwork
    {
        public int timeFragments => 500;
        public int inputsFeatures => 3;
        public int outputCount => neuralData.outputCount;
        public int neuronsCount 
        {
            get 
            {
                int count = 0;
                foreach (var layer in neuralData.networkLayers)
                {
                    count += layer.neuronsCount;
                }
                return count;
            } 
        }
        public int layersCount => neuralData.networkLayers.Length;

        private LocalLoaderAndSaverBSON<NNData>? loader;
        private BaseModel model;
        private NNData neuralData;
        #region creation
        public NeuralNetwork(BindingList<NNLayerConfig> config)
        {
            if(config == null || config.Count < 2) throw new ArgumentNullException("NeuralNetwork.Creation failed. config cant be null or count cant be less than 2");
            this.model = CreateNetwork(config);
            neuralData = new NNData(config);
        }

        public NeuralNetwork(string loadPath)
        {
            if (string.IsNullOrEmpty(loadPath)) throw new ArgumentNullException("NeuralNetwork.Creation failed. loadPath cant be null or empty");
            model = Keras.Models.Sequential.LoadModel(loadPath);
            loader = new LocalLoaderAndSaverBSON<NNData>(loadPath, "config");
            NNData? neuralData = loader.Load();
            if (neuralData == null) throw new Exception("NeuralNetwork.Constructor Network loading failed");
            this.neuralData = neuralData;
        }

        private Sequential CreateNetwork(BindingList<NNLayerConfig> config)
        {
            Sequential model = new Sequential();
            //creating input
            if (config[0].layerType == LayerType.LSTM) model.Add(CreateLSTMLayerFromConfig(config[0], config[1].layerType == LayerType.LSTM,
                new Keras.Shape(timeFragments, inputsFeatures)));
            else if (config[0].layerType == LayerType.Dense) model.Add(CreateDenseLayerFromConfig(config[0],
                new Keras.Shape(config[0].neuronsCount)));
            for (int i = 1; i < config.Count; i++)
            {
                if (config[i].layerType == LayerType.LSTM)model.Add(CreateLSTMLayerFromConfig(config[i], config[i + 1].layerType == LayerType.LSTM));
                else if (config[i].layerType == LayerType.Dense) model.Add(CreateDenseLayerFromConfig(config[i]));
            }
            //model.Add(new Dropout(0.2));
            ///loss
            ///"mean_squared_error" - сильнее ошибка - сильнее наказание, но из-за этого предсказание всегда 0
            ///huber_loss - для данных с выбросами
            // Компиляция модели
            var optimizer = new Keras.Optimizers.Nadam(lr: 0.00001f);
            model.Compile(optimizer: optimizer,
                          loss: "huber_loss",
                          metrics: new string[] { "mae", "mse" });

            return model;
        }

        private LSTM CreateLSTMLayerFromConfig(NNLayerConfig config, bool returnSequences, Keras.Shape? inputShape = null)
        {
            string? activationString = config.activation == ActivationFunc.linear ? null : config.activation.ToString();
            return new LSTM(config.neuronsCount, input_shape: inputShape, kernel_initializer: new GlorotNormal(),
                activation: activationString, return_sequences: returnSequences, recurrent_initializer: "orthogonal",recurrent_activation: "tanh");
        }

        private Dense CreateDenseLayerFromConfig(NNLayerConfig config, Keras.Shape? inputShape = null)
        {
            string? activationString = config.activation == ActivationFunc.linear ? null : config.activation.ToString();
            return new Dense(config.neuronsCount, input_shape: inputShape, activation: activationString,
                kernel_initializer: "he_normal");
        }
        #endregion
        /// <summary>
        /// for dense layered network
        /// </summary>
        /// <param name="dataWalker"></param>
        /// <param name="runsCount"></param>
        /// <param name="analyticsCollector"></param>
        /// <param name="onProgressChange"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<NNTrainingStats> TrainDenseNetwork(TwoOutputsDataWalker dataWalker, int runsCount,
            TrainingprogressAnalyticsCollector analyticsCollector, Action<float> onProgressChange)
        {
            if (dataWalker == null) throw new Exception("NeuralNetwork.Train dataWalker cant be null");
            if (runsCount <= 0) throw new Exception("NeuralNetwork.Train runsCount should be higher than one");

            NNTrainingStats tariningStats = new NNTrainingStats(runsCount);
            for (int run = 1; run <= runsCount; run++)
            {
                onProgressChange?.Invoke(run/(float)runsCount);
                double error = 0;
                // Разбиваем данные на батчи
                int walksIterations = 0;
                do
                {
                    List<KLine> expectedOutput;
                    List<KLine> input = dataWalker.Walk(out expectedOutput);
                    if (input != null)
                    {
                        if (expectedOutput.Count < 1)
                        {
                            throw new Exception("Data walker is ass. Returned 0 as expectedOutput");
                        }
                        if (input.Count < 2)
                        {
                            throw new Exception("Data walker is ass. Returned < 2 as input");
                        }
                        var inputArr = convertToTwoDimArr(input.Select(k => (double)(k.ClosePrice)).ToArray());
                        var outputArr = convertToTwoDimArr(expectedOutput.Select(k => (double)(k.ClosePrice)).ToArray());

                        // Тренируем модель на текущем батче
                        var loss = model.TrainOnBatch(np.array(inputArr), np.array(outputArr));
                        error += loss[0];
                        walksIterations++;
                        await Task.Delay(10); // Задержка для эмуляции асинхронной работы
                    }
                } while (dataWalker.isFinishedWalking());
                dataWalker.ResetDataWalker();
                tariningStats.RecordError(error/ walksIterations);
                tariningStats.GoNext();
            }

            return tariningStats;
            // Предсказания
            //var predictions = model.Predict(x_train);
            //Console.WriteLine("Predictions:");
            //Console.WriteLine(predictions.repr);
        }

        public async Task<NNTrainingStats> TrainLSTMNetwork(LSTMDataWalker dataWalker, int runsCount,int batchesCount,
            TrainingprogressAnalyticsCollector analyticsCollector, Action<float> onProgressChange)
        {
            if (dataWalker == null) throw new Exception("NeuralNetwork.Train dataWalker cant be null");
            if (runsCount <= 0) throw new Exception("NeuralNetwork.Train runsCount should be higher than one");

            NNTrainingStats tariningStats = new NNTrainingStats(runsCount);
            for (int run = 1; run <= runsCount; run++)
            {
                onProgressChange?.Invoke(run / (float)runsCount);
                double error = 0;
                // Разбиваем данные на батчи
                int walksIterations = 0;
                do
                {

                    List<double[,]> inputBatches = new List<double[,]>();
                    List<double[]> outputBatches = new List<double[]>();
                    for (int i = 0; i < batchesCount; i++)
                    {
                        double[] expectedOutput;
                        double[,] input = dataWalker.Walk(out expectedOutput);
                        
                        List<double[,]> normalized = Helpers.Normalization.Normalize(out double min, out double max, input);              
                        inputBatches.Add(Helpers.ArrayValuesInjection.InjectValues(normalized[0],min,max));
                        outputBatches.Add(Helpers.Normalization.Normalize(min, max, expectedOutput));
                        await Task.Delay(10);
                        if (dataWalker.isFinishedWalking()) break;
                    }
                    if (inputBatches.Count == 0 || outputBatches.Count == 0) break;

                    double[,] outputArr = Helpers.ConvertListTo3DArray(outputBatches);
                    var loss = model.TrainOnBatch(np.array(Helpers.ConvertListTo3DArray(inputBatches)),
                        np.array(outputArr)); 
                    error += loss[0];
                    walksIterations++;
                    await Task.Delay(10); 
                } while (!dataWalker.isFinishedWalking());
                dataWalker.ResetDataWalker();
                tariningStats.RecordError(error / walksIterations);
                tariningStats.GoNext();
            }

            return tariningStats;
            // Предсказания
            //var predictions = model.Predict(x_train);
            //Console.WriteLine("Predictions:");
            //Console.WriteLine(predictions.repr);
        }

        public double[] Predict(double[,,] input)
        {
            var npInput = np.array(input); 
            NDarray prediction = model.Predict(npInput);
            return prediction.GetData<double>();
        }


        private double[,] convertToTwoDimArr(double[] inputArr)
        {
            double[,] transformedArray = new double[1, inputArr.Length];
            for (int i = 0; i < inputArr.Length; i++)
            {
                transformedArray[0, i] = inputArr[i];
            }
            return transformedArray;
        }

        public void Save(string path)
        {
            model.Save(path);
            loader = new LocalLoaderAndSaverBSON<NNData>(path, "config");
            loader.Save(neuralData);
        }
    }

    public class NNData
    {
        public NNLayerConfig[] networkLayers { get; set; }
        [JsonIgnore] public int inputCount => networkLayers[0].neuronsCount;
        [JsonIgnore] public int outputCount => networkLayers[networkLayers.Length - 1].neuronsCount;

        /// <summary>
        /// for serialization. Do not use!
        /// </summary>
        public NNData()
        {

        }

        public NNData(BindingList<NNLayerConfig> config)
        {
            networkLayers = config.ToArray();
        }
    }
}