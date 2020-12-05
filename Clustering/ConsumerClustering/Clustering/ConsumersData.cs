using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Microsoft.ML.Data;

namespace Clustering
{
    public class ConsumersData
    {
        [LoadColumn(0)]
        public float Hour00;
        [LoadColumn(1)]
        public float Hour01;
        [LoadColumn(2)]
        public float Hour02;
        [LoadColumn(3)]
        public float Hour03;
        [LoadColumn(4)]
        public float Hour04;
        [LoadColumn(5)]
        public float Hour05;
        [LoadColumn(6)]
        public float Hour06;
        [LoadColumn(7)]
        public float Hour07;
        [LoadColumn(8)]
        public float Hour08;
        [LoadColumn(9)]
        public float Hour09;
        [LoadColumn(10)]
        public float Hour10;
        [LoadColumn(11)]
        public float Hour11;
        [LoadColumn(12)]
        public float Hour12;
        [LoadColumn(13)]
        public float Hour13;
        [LoadColumn(14)]
        public float Hour14;
        [LoadColumn(15)]
        public float Hour15;
        [LoadColumn(16)]
        public float Hour16;
        [LoadColumn(17)]
        public float Hour17;
        [LoadColumn(18)]
        public float Hour18;
        [LoadColumn(19)]
        public float Hour19;
        [LoadColumn(20)]
        public float Hour20;
        [LoadColumn(21)]
        public float Hour21;
        [LoadColumn(22)]
        public float Hour22;
        [LoadColumn(23)]
        public float Hour23;
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
