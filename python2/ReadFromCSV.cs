using LumenWorks.Framework.IO.Csv;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace PythonProcessor
{
    public class CSVRetriever
	{
		public static List<ProcessingResult> GetProcessingResults(List<int> outlierIndices)
		{
            List<ProcessingResult> processingResults = new List<ProcessingResult>(outlierIndices.Count);
            FillValues(processingResults, outlierIndices);
            FillCoordinates(processingResults, outlierIndices);
            FillStreetNameAndNum(processingResults, outlierIndices);
            return processingResults;
        }

        private static void FillValues(List<ProcessingResult> processingResults, List<int> outlierIndices)
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(@"C:\Users\admin\Desktop\python2\CSVFinal.csv")), false))
            {
                csvTable.Load(csvReader);
                List<int> outlierIndicesCopy = new List<int>(outlierIndices) { };
                outlierIndicesCopy.Add(11);
                foreach (int index in outlierIndicesCopy)
                {
                    ProcessingResult processingResult = new ProcessingResult();
                    DataRow row = csvTable.Rows[index];
                    int k = 0;
                    foreach (var data in row.ItemArray)
                    {
                        processingResult.ValuesByHour[k] = float.Parse(data.ToString());
                        k++;
                    }
                    processingResults.Add(processingResult);
                }
                processingResults[11].IsAvg = true;
            }
        }

        private static void FillCoordinates(List<ProcessingResult> processingResults, List<int> outlierIndices)
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(@"C:\Users\admin\Desktop\python2\CSVCoordinates.csv")), false))
            {
                csvTable.Load(csvReader);
                int m = 0;
                foreach (int index in outlierIndices)
                {
                    DataRow row = csvTable.Rows[index];
                    int k = 0;
                    foreach (var data in row.ItemArray)
                    {
                        processingResults[m].Coordinates[k] = float.Parse(data.ToString());
                        k++;
                    }
                    m++;
                }
            }
        }
    
        private static void FillStreetNameAndNum(List<ProcessingResult> processingResults, List<int> outlierIndices)
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(@"C:\Users\admin\Desktop\python2\CSVStreetNameAndNum.csv")), false))
            {
                csvTable.Load(csvReader);
                int m = 0;
                foreach (int index in outlierIndices)
                {
                    DataRow row = csvTable.Rows[index];
                    int k = 0;
                    foreach (var data in row.ItemArray)
                    {
                        processingResults[m].StreetNameNum[k] = data.ToString();
                        k++;
                    }
                    m++;
                }
            }
        }
    
    }
}
