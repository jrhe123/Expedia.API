using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expedia.API.Dtos;
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
            var touristRouteDto = new TouristRouteDto()
            {
                Id= touristRouteFromRepo.Id,
                Title = touristRouteFromRepo.Title,
                Description = touristRouteFromRepo.Description,
                Price = touristRouteFromRepo.OriginalPrice * (decimal)(touristRouteFromRepo.DiscountPercent),
                CreateTime = touristRouteFromRepo.CreateTime,
                UpdateTime = touristRouteFromRepo.UpdateTime,
                Features = touristRouteFromRepo.Features,
                Fees = touristRouteFromRepo.Fees,
                Notes = touristRouteFromRepo.Notes,
                Rating = touristRouteFromRepo.Rating,
                TravelDays = touristRouteFromRepo.TravelDays.ToString(),
                TripType = touristRouteFromRepo.TripType.ToString(),
                DepartureCity = touristRouteFromRepo.DepartureCity.ToString()
            };
            return Ok(touristRouteDto);
        }
    }
}

