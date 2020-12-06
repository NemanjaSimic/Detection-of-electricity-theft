using DetectionOfElectirictyTheft.Models;
using System.Collections.Generic;

namespace DetectionOfElectirictyTheft.Engine
{
	public class Engine
	{
		public List<ProcessingResult> GetProcessingResults()
		{
            DataProcessor.DetectOutliers(out string errors, out string results);

            List<int> outlierIndices = GetOutlierIndices(results);

            List<ProcessingResult> processingResults = CSVRetriever.GetProcessingResults(outlierIndices);

            return processingResults;
        }

        private List<int> GetOutlierIndices(string results)
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
