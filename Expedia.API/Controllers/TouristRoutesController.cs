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
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;

        public TouristRoutesController(
            ITouristRouteRepository touristRouteRepository
        )
        {
            _touristRouteRepository = touristRouteRepository;
        }


        // https://localhost:7143/api/touristRoutes
        [HttpGet]
        public IActionResult GetTouristRoutes()
        {
            var touristRoutesFromRepo =
                this._touristRouteRepository.GetTouristRoutes();
            if (touristRoutesFromRepo == null ||
                touristRoutesFromRepo.Count() == 0)
            {
                return NotFound("no found");
            }
            return Ok(touristRoutesFromRepo);
        }

        // https://localhost:7143/api/touristRoutes/{TouristRouteId}
        [HttpGet("{TouristRouteId:Guid}")]
        public IActionResult GetTouristRouteById(Guid TouristRouteId)
        {
            var touristRouteFromRepo = this._touristRouteRepository.GetTouristRoute(
                TouristRouteId
                );
            if (touristRouteFromRepo == null)
            {
                return NotFound($"not found {TouristRouteId}");
            }
            return Ok(touristRouteFromRepo);
        }
    }
}

