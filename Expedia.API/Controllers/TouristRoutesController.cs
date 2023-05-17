using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expedia.API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Expedia.API.Controllers
{
    [Route("api/[controller]")]
    public class TouristRoutesController : Controller
    {
        private ITouristRouteRepository _touristRouteRepository;

        public TouristRoutesController(ITouristRouteRepository touristRouteRepository)
        {
            _touristRouteRepository = touristRouteRepository;
        }


        // https://localhost:7143/api/touristRoutes
        public IActionResult GetTouristRoutes()
        {
            var routes = this._touristRouteRepository.GetTouristRoutes();
            return Ok(routes);
        }
    }
}

