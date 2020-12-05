using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DetectionOfElectirictyTheft.Models
{
	public class CSVModel
	{
		public string Name { get; set; }
		public DateTime Time { get; set; }
		public double Consumption { get; set; }
	}
}
