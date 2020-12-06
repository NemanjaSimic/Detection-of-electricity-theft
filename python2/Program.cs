
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using LumenWorks.Framework.IO.Csv;

namespace PythonProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            DataProcessor.DetectOutliers(out string errors, out string results);

            List<int> outlierIndices = GetOutlierIndices(results);

            List<ProcessingResult> processingResults = CSVRetriever.GetProcessingResults(outlierIndices);

            Console.WriteLine("Processing done.");
            Console.WriteLine(results);
            Console.ReadKey();
        }

        private static List<int> GetOutlierIndices(string results)
        {
            results = results.Replace("[", "");
            results = results.Replace("]", "");

            string[] numbers = results.Split(' ');
            List<int> parsedNums = new List<int>();

            foreach (var number in numbers)
            {
                if (int.TryParse(number, out int num))
                {
                    parsedNums.Add(num);
                }
            }
            int i = 0;
            List<int> outlierIndices = new List<int>();
            foreach (int parsedNum in parsedNums)
            {
                if (parsedNum == -1)
                {
                    outlierIndices.Add(i);
                }
                i++;
            }
            return outlierIndices;
        }
    }
}
