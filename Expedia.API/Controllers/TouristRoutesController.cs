using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expedia.API.Dtos;
using Expedia.API.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Expedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        private readonly IMapper _mapper;

        public TouristRoutesController(
            ITouristRouteRepository touristRouteRepository,
            IMapper mapper
        )
        {
            _touristRouteRepository = touristRouteRepository;
            _mapper = mapper;
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
            var touristRoutesDto = _mapper.Map<IEnumerable<TouristRouteDto>>(
                touristRoutesFromRepo);
            return Ok(touristRoutesDto);
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
            var touristRouteDto = _mapper.Map<TouristRouteDto>(
                touristRouteFromRepo);
            return Ok(touristRouteDto);
        }
    }
}

