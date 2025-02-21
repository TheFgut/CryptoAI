using CryptoAI_Upgraded.Datasets.DataWalkers;
using Keras.Layers;
using Keras.Models;
using Numpy;
using CryptoAI_Upgraded.Datasets;
using System.ComponentModel;
using System.Text.Json.Serialization;
using CryptoAI_Upgraded.DataSaving;
using CryptoAI_Upgraded.PythonBridge;
using Python.Runtime;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks
{
    public class NeuralNetwork
    {
        public int timeFragments => _neuralData.timeFragments;

        public int inputCount => _neuralData.inputsLen;//data fragments input at the same time
        public int inputsFeatures => _neuralData.featuresCount;
        public FeatureType[] features => _neuralData.features;
        public int outputCount => _neuralData.outputCount;
        public int neuronsCount 
        {
            get 
            {
                int count = 0;
                foreach (var layer in _neuralData.networkLayers)
                {
                    count += layer.neuronsCount;
                }
                return count;
            } 
        }
        public int layersCount => _neuralData.networkLayers.Length;

        private LocalLoaderAndSaverBSON<NNConfigData>? loader;
        public BaseModel model;//close
        private NNConfigData _neuralData;
        public NNConfigData networkConfig => _neuralData;//to do make clone

        #region creation
        public NeuralNetwork(NNConfigData config)
        {
            _neuralData = config;
            this.model = CreateNetwork(config);
        }

        public NeuralNetwork(string loadPath)
        {
            if (string.IsNullOrEmpty(loadPath)) throw new ArgumentNullException("NeuralNetwork.Creation failed. loadPath cant be null or empty");
            model = Sequential.LoadModel(loadPath);
            loader = new LocalLoaderAndSaverBSON<NNConfigData>(loadPath, "config");
            NNConfigData? neuralData = loader.Load();
            if (neuralData == null) throw new Exception("NeuralNetwork.Constructor Network loading failed");
            this._neuralData = neuralData;

        }

        private Sequential CreateNetwork(NNConfigData config)
        {
            Sequential model = new Sequential();
            NNLayerConfig[] layers = config.networkLayers;
            //creating input
            if (layers[0].layerType == LayerType.LSTM) model.Add(CreateLSTMLayerFromConfig(layers[0], layers[1].layerType == LayerType.LSTM,
                new Keras.Shape(timeFragments, (inputCount * inputsFeatures) + 2)));
            else if (layers[0].layerType == LayerType.Dense) model.Add(CreateDenseLayerFromConfig(layers[0],
                new Keras.Shape(layers[0].neuronsCount)));

            for (int i = 1; i < layers.Length; i++)
            {
                if (layers[i].layerType == LayerType.LSTM)model.Add(CreateLSTMLayerFromConfig(layers[i], layers[i + 1].layerType == LayerType.LSTM));
                else if (layers[i].layerType == LayerType.Dense) model.Add(CreateDenseLayerFromConfig(layers[i]));
            }
            //model.Add(new Dropout(0.2));
            ///loss
            ///"mean_squared_error" - сильнее ошибка - сильнее наказание, но из-за этого предсказание всегда 0
            ///huber_loss - для данных с выбросами
            // Компиляция модели
            var optimizer = new Keras.Optimizers.Nadam(lr: 0.001f);
            model.Compile("adam",
                          loss: "mean_squared_error"
                          , metrics: new string[] { "mae" });

            return model;
        }

        private LSTM CreateLSTMLayerFromConfig(NNLayerConfig config, bool returnSequences, Keras.Shape? inputShape = null)
        {
            string? activationString = config.activation == ActivationFunc.linear ? null : config.activation.ToString();
            return new LSTM(config.neuronsCount, input_shape: inputShape, kernel_initializer: "orthogonal",
                activation: activationString, return_sequences: returnSequences, recurrent_initializer: "orthogonal",
                bias_initializer: "ones");
        }

        private Dense CreateDenseLayerFromConfig(NNLayerConfig config, Keras.Shape? inputShape = null)
        {
            string? activationString = config.activation == ActivationFunc.linear ? null : config.activation.ToString();
            return new Dense(config.neuronsCount, input_shape: inputShape, activation: activationString,
                kernel_initializer: "orthogonal", bias_initializer: "ones");
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
                        var loss = model.TrainOnBatch(np.array<double>(inputArr), np.array<double>(outputArr));
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

        public async Task TrainLSTMNetwork(LSTMDataWalker dataWalker, int runsCount,int batchesCount,
            NNTrainingStats analyticsCollector, Action<float> onProgressChange, CancellationToken cancellationToken)
        {
            if (dataWalker == null) throw new Exception("NeuralNetwork.Train dataWalker cant be null");
            if (runsCount <= 0) throw new Exception("NeuralNetwork.Train runsCount should be higher than one");

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
                        
                        //List<double[,]> normalized = Helpers.Normalization.Normalize(out double min, out double max, input);              
                        inputBatches.Add(Helpers.ArrayValuesInjection.InjectValues(input,0,0));
                        outputBatches.Add(expectedOutput);
                        await Task.Delay(10);
                        if (dataWalker.isFinishedWalking()) break;
                    }
                    if (inputBatches.Count == 0 || outputBatches.Count == 0) break;

                    double[,] outputArr = Helpers.ConvertListTo3DArray(outputBatches);

                    PyObject inputArr = np.array<double>(Helpers.ConvertListTo3DArray(inputBatches));
                    PyObject expectedOutputArr = np.array<double>(outputArr);
                    var loss = model.TrainOnBatch(inputArr, expectedOutputArr);
                    error += loss[0];
                    walksIterations++;
                    if (cancellationToken.IsCancellationRequested)//cancellation of task
                    {
                        analyticsCollector.RecordError(error / walksIterations);
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                    await Task.Delay(10); 
                } while (!dataWalker.isFinishedWalking());
                dataWalker.ResetDataWalker();
                analyticsCollector.RecordError(error / walksIterations);
                analyticsCollector.GoNext();
            }
            // Предсказания
            //var predictions = model.Predict(x_train);
            //Console.WriteLine("Predictions:");
            //Console.WriteLine(predictions.repr);
        }

        public double[] Predict(double[,,] input)
        {
            var npInput = np.array<double>(input); 
            NDarray prediction = model.Predict(npInput);
            double[] predictions = prediction.GetData<double>();
            for (int i = 0; i < predictions.Length; i++)
            {
                if (predictions[i] == double.NaN) predictions[i] = 0;//need to handle this somehow
            }
            return predictions;
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
            loader = new LocalLoaderAndSaverBSON<NNConfigData>(path, "config");
            loader.Save(_neuralData);
        }
    }

    public class NNConfigData
    {
        public int timeFragments {  get; set; }
        public NNLayerConfig[] networkLayers { get; set; }
        /// <summary>
        /// count of time elements to input
        /// </summary>
        public int inputsLen {  get; set; }
        public FeatureType[] features { get; set; }

        [JsonIgnore] public int featuresCount => features.Length;
        [JsonIgnore] public int outputCount => networkLayers[networkLayers.Length - 1].neuronsCount;

        /// <summary>
        /// for serialization. Do not use!
        /// </summary>
        public NNConfigData()
        {

        }

        public NNConfigData(BindingList<NNLayerConfig> config, FeatureType[] features, int timeFragments, int inputsLen)
        {
            if (config == null || config.Count < 2) throw new ArgumentNullException("NNConfigData.Creation failed. config cant be null or count cant be less than 2");
            this.timeFragments = timeFragments;
            this.inputsLen = inputsLen;
            this.features = features;
            networkLayers = config.ToArray();
        }
    }
}