using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expedia.API.Dtos;
using Expedia.API.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

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


        // https://localhost:7143/api/touristRoutes?Keyword=xxx
        [HttpGet]
        [HttpHead]
        public IActionResult GetTouristRoutes(
            [FromQuery(Name = "Keyword")] string Keyword
            )
        {
            var touristRoutesFromRepo =
                this._touristRouteRepository.GetTouristRoutes(Keyword);
            if (touristRoutesFromRepo == null ||
                touristRoutesFromRepo.Count() == 0)
            {
                return NotFound("tourist route no found");
            }
            var touristRoutesDto = _mapper.Map<IEnumerable<TouristRouteDto>>(
                touristRoutesFromRepo);
            return Ok(touristRoutesDto);
        }


        // https://localhost:7143/api/touristRoutes/{TouristRouteId}
        [HttpGet("{TouristRouteId:Guid}")]
        [HttpHead("{TouristRouteId:Guid}")]
        public IActionResult GetTouristRouteById(Guid TouristRouteId)
        {
            var touristRouteFromRepo = this._touristRouteRepository.GetTouristRoute(
                TouristRouteId
                );
            if (touristRouteFromRepo == null)
            {
                return NotFound($"tourist route not found {TouristRouteId}");
            }
            var touristRouteDto = _mapper.Map<TouristRouteDto>(
                touristRouteFromRepo);
            return Ok(touristRouteDto);
        }
    }
}

