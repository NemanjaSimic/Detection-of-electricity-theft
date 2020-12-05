using System;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;

// https://docs.microsoft.com/en-us/dotnet/machine-learning/tutorials/iris-clustering

namespace Clustering
{
	class Program
	{
		static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "TrainingDataset.csv");
		static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "TrainedClusteringModel.zip");

		static void Main(string[] args)
		{
			// The Microsoft.ML.MLContext class represents the machine learning environment and provides mechanisms for logging and entry points for data loading, model training, prediction, and other tasks. This is comparable conceptually to using DbContext in Entity Framework.
			var mlContext = new MLContext(seed: 0);

			// Set up data loading
			// The generic MLContext.Data.LoadFromTextFile extension method infers the data set schema from the provided IrisData type and returns IDataView which can be used as input for transformers.
			IDataView dataView = mlContext.Data.LoadFromTextFile<ConsumersData>(_dataPath, hasHeader: false, separatorChar: ',');

			IDataView dataViewForAnomaly = mlContext.Data.LoadFromTextFile<ConsumersDataForAnomaly>(_dataPath, hasHeader: false, separatorChar: ','); 

			//var feat = dataView.GetColumn<float[]>("HourValues").ToList();

			// Create a learning pipeline
			// specifies that the data set should be split in three clusters.
			string featuresColumnName = "Features";
			// the learning pipeline of the clustering task comprises two following steps:
			// concatenate loaded columns into one Features column, which is used by a clustering trainer;
			// use a KMeansTrainer trainer to train the model using the k-means++ clustering algorithm.
			var pipeline = mlContext.Transforms
				.Concatenate(featuresColumnName,
				"Hour00", "Hour01", "Hour02", "Hour03", "Hour04", "Hour05",
				"Hour06", "Hour07", "Hour08", "Hour09", "Hour10", "Hour11",
				"Hour12", "Hour13", "Hour14", "Hour15", "Hour16", "Hour17",
				"Hour18", "Hour19", "Hour20", "Hour21", "Hour22", "Hour23")
				.Append(mlContext.Clustering.Trainers.KMeans(featuresColumnName, numberOfClusters: 3));

			// Train the model
			var model = pipeline.Fit(dataView);

			// Save the model
			using (var fileStream = new FileStream(_modelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
			{
				mlContext.Model.Save(model, dataView.Schema, fileStream);
			}

			// Use the model for predictions
			// use the PredictionEngine<TSrc,TDst> class that takes instances of the input type through the transformer pipeline and produces instances of the output type
			// The PredictionEngine is a convenience API, which allows you to perform a prediction on a single instance of data. PredictionEngine is not thread-safe. It's acceptable to use in single-threaded or prototype environments. For improved performance and thread safety in production environments, use the PredictionEnginePool service, which creates an ObjectPool of PredictionEngine objects for use throughout your application
			var predictor = mlContext.Model.CreatePredictionEngine<ConsumersData, ClusterPrediction>(model);

			// find out the cluster to which the specified item belongs to
			var prediction = predictor.Predict(TestConsumersData.Setosa);
			Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
			Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");


			var transformedTestData = model.Transform(dataView);

			// Convert IDataView object to a list.
			var predictions = mlContext.Data.CreateEnumerable<ConsumersData>(
				transformedTestData, reuseRowObject: false).ToList();

			var metrics = mlContext.Clustering.Evaluate(
				transformedTestData, "PredictedLabel", "Score", "Features");

			// Print 5 predictions. Note that the label is only used as a comparison
			// with the predicted label. It is not used during training.
			foreach (var p in predictions.Take(2))
			{
			}
		}

		// Obsolete
		public bool LoadModel(MLContext mlContext)
		{
			if (!File.Exists(_modelPath))
			{
				return false;
			}
			DataViewSchema dataView;
			mlContext.Model.Load(_modelPath, out dataView);
			
			return true;
		}
	}
}
