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
		public IEnumerable<CSVModel> Get()
		{
			IEnumerable<CSVModel> retVal = new List<CSVModel>();

			return retVal;
		}
	}
}
