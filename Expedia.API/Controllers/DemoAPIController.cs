using System;
using Microsoft.AspNetCore.Mvc;

namespace Expedia.API.Controllers
{
	[Route("api/demo")]
	// 1. Controller decorator
	//[Controller]
	//public class DemoAPI

	// 2. naming ends with "Controller"
	//public class DemoAPIController

	// 3. extends Controller class
	public class DemoAPIController : Controller
    {
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "1", "2", "3" };
		}
	}
}

