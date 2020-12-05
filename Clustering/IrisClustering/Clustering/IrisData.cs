using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Data;

namespace Clustering
{
    public class IrisData
    {
        [LoadColumn(0)]
        public float SepalLength;

        [LoadColumn(1)]
        public float SepalWidth;

        [LoadColumn(2)]
        public float PetalLength;

        [LoadColumn(3)]
        public float PetalWidth;
    }

    // The ClusterPrediction class represents the output of the clustering model applied to an IrisData instance.Use the ColumnName attribute to bind the PredictedClusterId and Distances fields to the PredictedLabel and Score columns respectively.In case of the clustering task those columns have the following meaning:
    // PredictedLabel column contains the ID of the predicted cluster.
    // Score column contains an array with squared Euclidean distances to the cluster centroids.The array length is equal to the number of clusters.
    public class ClusterPrediction
    {
        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId;

        [ColumnName("Score")]
        public float[] Distances;
    }
}
