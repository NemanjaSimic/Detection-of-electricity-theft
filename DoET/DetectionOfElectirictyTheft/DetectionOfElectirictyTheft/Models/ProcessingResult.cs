using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DetectionOfElectirictyTheft.Models
{
	public class ProcessingResult
	{
		public float[] ValuesByHour { get; set; }

		public double[] Coordinates { get; set; }

		public string[] StreetNameNum { get; set; }

		public bool IsAvg { get; set; }

		public ProcessingResult()
		{
			ValuesByHour = new float[24];
			Coordinates = new double[2];
			StreetNameNum = new string[2];
			IsAvg = false;
		}
	}
}
