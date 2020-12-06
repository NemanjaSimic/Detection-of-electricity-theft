using DetectionOfElectirictyTheft.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DetectionOfElectirictyTheft.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CSVController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<ProcessingResult> Get()
		{
			//IEnumerable<ProcessingResult> retVal = new List<ProcessingResult>(1)
			//{
			//	new ProcessingResult()
			//	{
			//		IsAvg = true,
			//		Coordinates = new double[2] {45.239225 , 45.239225 },
			//		StreetNameNum = new string[2] {"Narodnog fronta", "25" },
			//		ValuesByHour = new float[24]
			//		{
			//				81 ,
			//				65 ,
			//				60 ,
			//				75 ,
			//				105 ,
			//				88 ,
			//				48 ,
			//				34 ,
			//				71 ,
			//				55 ,
			//				40 ,
			//				55 ,
			//				105 ,
			//				88 ,
			//				38 ,
			//				54 ,
			//				81 ,
			//				75 ,
			//				80 ,
			//				95 ,
			//				105 ,
			//				38 ,
			//				28 ,
			//				34
			//		}
			//	}
			//};
			Engine.Engine engine = new Engine.Engine();
			return engine.GetProcessingResults();
		}
	}
}
