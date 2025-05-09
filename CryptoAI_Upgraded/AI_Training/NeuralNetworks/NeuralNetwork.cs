using CryptoAI_Upgraded.Datasets.DataWalkers;
using Keras.Layers;
using Keras.Models;
using Numpy;
using CryptoAI_Upgraded.Datasets;
using System.ComponentModel;
using System.Text.Json.Serialization;
using CryptoAI_Upgraded.DataSaving;
using Newtonsoft.Json.Linq;

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
        public NetworkTrainingsStats trainingStatistics { get; private set; }
        public NNConfigData networkConfig => _neuralData;//to do make clone


        #region creation
        public NeuralNetwork(NNConfigData config)
        {
            _neuralData = config;
            this.model = CreateNetwork(config);
            trainingStatistics = new NetworkTrainingsStats();
        }

        public NeuralNetwork(string loadPath)
        {
            if (string.IsNullOrEmpty(loadPath)) throw new ArgumentNullException("NeuralNetwork.Creation failed. loadPath cant be null or empty");
            model = Sequential.LoadModel(loadPath);
            loader = new LocalLoaderAndSaverBSON<NNConfigData>(loadPath, "config");
            NNConfigData? neuralData = loader.Load();
            if (neuralData == null) throw new Exception("NeuralNetwork.Constructor Network loading failed");
            this._neuralData = neuralData;
            trainingStatistics = new NetworkTrainingsStats(loadPath);
        }

        private Sequential CreateNetwork(NNConfigData config)
        {
            Sequential model = new Sequential();
            NNLayerConfig[] layers = config.networkLayers;
            float dropout = 0;
            //creating input
            if (layers[0].layerType == LayerType.LSTM) model.Add(CreateLSTMLayerFromConfig(layers[0], dropout,
                layers[1].layerType == LayerType.LSTM, new Keras.Shape(timeFragments, (inputCount * inputsFeatures) + 2)));
            else if (layers[0].layerType == LayerType.Dense) model.Add(CreateDenseLayerFromConfig(layers[0],
                new Keras.Shape(layers[0].neuronsCount)));

            for (int i = 1; i < layers.Length; i++)
            {
                if (layers[i].layerType == LayerType.LSTM)model.Add(CreateLSTMLayerFromConfig(layers[i], dropout,
                    layers[i + 1].layerType == LayerType.LSTM));
                else if (layers[i].layerType == LayerType.Dense) model.Add(CreateDenseLayerFromConfig(layers[i]));
            }
            //model.Add(new Dropout(0.2));
            ///loss
            ///"mean_squared_error" - сильнее ошибка - сильнее наказание, но из-за этого предсказание всегда 0
            ///huber_loss - для данных с выбросами
            // Компиляция модели
            var optimizer = new Keras.Optimizers.Nadam(lr: 0.00001f);
            model.Compile("adam",
                          loss: "huber_loss"
                          , metrics: new string[] { "mae" });

            return model;
        }

        private LSTM CreateLSTMLayerFromConfig(NNLayerConfig config, float dropout, bool returnSequences, Keras.Shape? inputShape = null)
        {
            string? activationString = config.activation == ActivationFunc.linear ? null : config.activation.ToString();
            return new LSTM(config.neuronsCount, input_shape: inputShape, activation: activationString, return_sequences: returnSequences
                , bias_initializer: "ones", recurrent_activation: ActivationFunc.sigmoid.ToString(), dropout: dropout);//do not change reccucrent activation to prevent gradient explosion
            //kernel_initializer: "orthogonal",
            //,  recurrent_initializer: "orthogonal",
            //      
        }

        private Dense CreateDenseLayerFromConfig(NNLayerConfig config, Keras.Shape? inputShape = null)
        {
            string? activationString = config.activation == ActivationFunc.linear ? null : config.activation.ToString();
            return new Dense(config.neuronsCount, input_shape: inputShape, activation: activationString,
                bias_initializer: "ones");
               // kernel_initializer: "orthogonal", bias_initializer: "ones");
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

            NNTrainingStats tariningStats = new NNTrainingStats(runsCount, dataWalker.datasetIDs);
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
                        var inputArr = convertToTwoDimArr(input.Select(k => (float)(k.ClosePrice)).ToArray());
                        var outputArr = convertToTwoDimArr(expectedOutput.Select(k => (float)(k.ClosePrice)).ToArray());

                        // Тренируем модель на текущем батче
                        var loss = model.TrainOnBatch(np.array(inputArr), np.array(outputArr));
                        error += loss[0];
                        walksIterations++;
                        await Task.Delay(10); // Задержка для эмуляции асинхронной работы
                    }
                } while (dataWalker.isFinishedWalking());
                dataWalker.ResetDataWalker();
                tariningStats.RecordAwerageError(error/ walksIterations);
                tariningStats.GoNext();
            }

            return tariningStats;
            // Предсказания
            //var predictions = model.Predict(x_train);
            //Console.WriteLine("Predictions:");
            //Console.WriteLine(predictions.repr);
        }

        public async Task TrainLSTMNetwork(LSTMDataWalker trainDataWalker, int batchesCount,
            NNTrainingStats analyticsCollector, Action<float> onProgressChange, TrainingConfigData trainingSettings,
            CancellationToken cancellationToken, LSTMDataWalker? testDataWalker = null, bool testEveryRun = true)
        {
            if (trainDataWalker == null) throw new Exception("NeuralNetwork.Train dataWalker cant be null");
            if (trainingSettings.runsCount <= 0) throw new Exception("NeuralNetwork.Train runsCount should be higher than one");
            NetworkTrainingsStats originalStatistics = trainingStatistics;
            EarlyStopping earlyStopping = new EarlyStopping(trainingSettings.patienceToStop,
                trainingSettings.minErrorDeltaToStop);
            double bestAwerageTestError = double.MaxValue;
            float progress = 0;
            for (int run = 1; run <= trainingSettings.runsCount; run++)
            {
                double errorsSum = 0;
                double minError = double.MaxValue;
                double maxError = double.MinValue;
                // Разбиваем данные на батчи
                int walksIterations = 0;
                do
                {

                    List<double[,]> inputBatches = new List<double[,]>();
                    List<double[]> outputBatches = new List<double[]>();
                    for (int i = 0; i < batchesCount; i++)
                    {
                        double[] expectedOutput;
                        double[,] input = trainDataWalker.Walk(out expectedOutput);
                        
                        //List<double[,]> normalized = Helpers.Normalization.Normalize(out double min, out double max, input);              
                        inputBatches.Add(Helpers.ArrayValuesInjection.InjectValues(input,0,0));
                        outputBatches.Add(expectedOutput);
                        await Task.Delay(10);
                        if (trainDataWalker.isFinishedWalking()) break;
                    }
                    if (inputBatches.Count == 0 || outputBatches.Count == 0) break;

                    double[,] outputArr = Helpers.ConvertListTo2DArray(outputBatches);

                    NDarray<double> inputArr = np.array(Helpers.ConvertListTo3DArray(inputBatches));
                    NDarray<double> expectedOutputArr = np.array(outputArr);
                    var metrics = model.TrainOnBatch(inputArr, expectedOutputArr);
                    errorsSum += metrics[1];
                    minError = metrics[1] < minError ? metrics[1] : minError;
                    maxError = metrics[1] > maxError ? metrics[1] : maxError;

                    walksIterations++;
                    if (cancellationToken.IsCancellationRequested)//cancellation of task
                    {
                        analyticsCollector.RecordAwerageError(errorsSum / walksIterations);
                        analyticsCollector.RecordMinError(minError);
                        analyticsCollector.RecordMaxError(maxError);
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                    await Task.Delay(10);
                    float newProgress = (float)(Math.Floor((((run-1) + trainDataWalker.walkingProgress * 0.8) / 
                        (float)trainingSettings.runsCount) *100)/100);
                    if(newProgress != progress)
                    {
                        progress = newProgress;
                        onProgressChange?.Invoke(progress);
                    }
                } while (!trainDataWalker.isFinishedWalking());
                trainDataWalker.ResetDataWalker();
                double awgError = errorsSum / walksIterations;
                analyticsCollector.RecordAwerageError(awgError);
                analyticsCollector.RecordMinError(minError);
                analyticsCollector.RecordMaxError(maxError);
                if (testEveryRun && testDataWalker != null)
                {
                    try
                    {
                        await TestLSTMNetwork(testDataWalker, analyticsCollector, (progress) =>
                        {
                            float newProgress = (float)(Math.Floor((((run - 1) + 0.8 + progress * 0.2) /
                                (float)trainingSettings.runsCount) * 100) / 100);
                            if (newProgress != progress)
                            {
                                progress = newProgress;
                                onProgressChange?.Invoke(progress);
                            }
                        }, new CancellationToken());
                        testDataWalker?.ResetDataWalker();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Exception during execution of testing {ex.Message}\n" +
                            $" {ex.StackTrace}");
                    }
                }

                double awerageErrorToCheck = analyticsCollector.lastRun.noTestMetrics ? analyticsCollector.lastRun.averageError :
                    analyticsCollector.lastRun.avarageTestError;
                if (double.IsNaN(awerageErrorToCheck))
                {
                    throw new Exception("Something went wrong. Awerage error is now NaN");
                }
                if (trainingSettings.stopWhenErrorRising && 
                    earlyStopping.CheckShouldStop(awerageErrorToCheck))
                {
                    break;
                }
                if (bestAwerageTestError > awerageErrorToCheck)
                {
                    trainingStatistics = (NetworkTrainingsStats)originalStatistics.Clone();
                    trainingStatistics.RecordTrainingData(analyticsCollector);
                    Save($"{DataPaths.networksPath}\\temporarySavedNetwork", true);
                    bestAwerageTestError = awerageErrorToCheck;
                }
                analyticsCollector.GoNext();
            }
            trainingStatistics = originalStatistics;
            trainingStatistics.RecordTrainingData(analyticsCollector);
            // Предсказания
            //var predictions = model.Predict(x_train);
            //Console.WriteLine("Predictions:");
            //Console.WriteLine(predictions.repr);
        }

        public async Task TestLSTMNetwork(LSTMDataWalker dataWalker,
          NNTrainingStats analyticsCollector, Action<float> onProgressChange,
          CancellationToken cancellationToken)
        {
            if (dataWalker == null) throw new Exception("NeuralNetwork.Test dataWalker cant be null");

            double predictionsErrorSum = 0;
            double minError = double.MaxValue;
            double maxError = double.MinValue;

            int walkSteps = 0;
            do
            {

                double[] expectedOutput;
                double[,] input = dataWalker.Walk(out expectedOutput);
                input = Helpers.ArrayValuesInjection.InjectValues(input, 0, 0);

                double[,,] inputArr = Helpers.ConvertArrTo3DArray(input);
                float[] precitions = Predict(inputArr);

                double awgPredictionsError = 0;
                for (int valueNum = 0; valueNum < precitions.Length; valueNum++)
                {
                    double difference = expectedOutput[valueNum] - precitions[valueNum];
                    awgPredictionsError += Math.Abs(difference);
                    minError = Math.Min(minError, difference);
                    maxError = Math.Max(maxError, difference);
                }
                awgPredictionsError /= precitions.Length;
                predictionsErrorSum += awgPredictionsError;

                walkSteps++;
                if (cancellationToken.IsCancellationRequested)//cancellation of task
                {
                    analyticsCollector.RecordTestMetrics(predictionsErrorSum / walkSteps, minError, maxError);
                    cancellationToken.ThrowIfCancellationRequested();
                }
                await Task.Delay(10);
                onProgressChange?.Invoke(dataWalker.walkingProgress);
            } while (!dataWalker.isFinishedWalking());
            analyticsCollector.RecordTestMetrics(predictionsErrorSum / walkSteps, minError, maxError);
        }

        public float[] Predict(double[,,] input)
        {
            NDarray<double> npInput = np.array(input); 
            NDarray prediction = model.Predict(npInput);
            float[] predictions = prediction.GetData<float>();
            for (int i = 0; i < predictions.Length; i++)
            {
                if (predictions[i] == double.NaN || double.IsInfinity(predictions[i])) predictions[i] = 0;//need to handle this somehow
            }
            return predictions;
        }

        public double[] PredictDouble(double[,,] input)
        {
            NDarray<double> npInput = np.array(input);
            NDarray prediction = model.Predict(npInput);
            double[] predictions = prediction.GetData<double>();
            for (int i = 0; i < predictions.Length; i++)
            {
                if (predictions[i] == double.NaN) predictions[i] = 0;//need to handle this somehow
            }
            return predictions;
        }


        private T[,] convertToTwoDimArr<T>(T[] inputArr)
        {
            T[,] transformedArray = new T[1, inputArr.Length];
            for (int i = 0; i < inputArr.Length; i++)
            {
                transformedArray[0, i] = inputArr[i];
            }
            return transformedArray;
        }

        public void Save(string path, bool everwriteIfExist = false)
        {
            if (Directory.Exists(path) && Directory.GetFiles(path).Length != 0)
            {
                if (File.Exists($"{path}\\config.bson") && everwriteIfExist)
                {
                    Directory.Delete(path,true);
                }
                else throw new Exception("There is something exist at the directory to save.");
            }
            model.Save(path);
            loader = new LocalLoaderAndSaverBSON<NNConfigData>(path, "config");
            loader.Save(_neuralData);
            trainingStatistics.Save(path);
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