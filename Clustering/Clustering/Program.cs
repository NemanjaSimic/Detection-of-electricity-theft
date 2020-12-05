using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;

// https://docs.microsoft.com/en-us/dotnet/machine-learning/tutorials/iris-clustering

namespace Clustering
{
	class Program
	{
		static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "iris_dataset.csv");
		static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "IrisClusteringModel.zip");

		static void Main(string[] args)
		{
			// The Microsoft.ML.MLContext class represents the machine learning environment and provides mechanisms for logging and entry points for data loading, model training, prediction, and other tasks. This is comparable conceptually to using DbContext in Entity Framework.
			var mlContext = new MLContext(seed: 0);

			// Set up data loading
			// The generic MLContext.Data.LoadFromTextFile extension method infers the data set schema from the provided IrisData type and returns IDataView which can be used as input for transformers.
			IDataView dataView = mlContext.Data.LoadFromTextFile<IrisData>(_dataPath, hasHeader: false, separatorChar: ',');

			// Create a learning pipeline
			// specifies that the data set should be split in three clusters.
			string featuresColumnName = "Features";
			// the learning pipeline of the clustering task comprises two following steps:
			// concatenate loaded columns into one Features column, which is used by a clustering trainer;
			// use a KMeansTrainer trainer to train the model using the k-means++ clustering algorithm.
			var pipeline = mlContext.Transforms
				.Concatenate(featuresColumnName, "SepalLength", "SepalWidth", "PetalLength", "PetalWidth")
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
			var predictor = mlContext.Model.CreatePredictionEngine<IrisData, ClusterPrediction>(model);

			// find out the cluster to which the specified item belongs to
			var prediction = predictor.Predict(TestIrisData.Setosa);
			Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
			Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");
		}
	}
}
