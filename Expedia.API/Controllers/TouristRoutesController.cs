using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expedia.API.Dtos;
using Expedia.API.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Text.RegularExpressions;
using Expedia.API.ResourceParameters;
using Expedia.API.Models;

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


        // https://localhost:7143/api/touristRoutes?Keyword=xxx&Rating=largerThan3
        [HttpGet]
        [HttpHead]
        public IActionResult GetTouristRoutes(
            [FromQuery] TouristRouteResourceParameters parameters

            // move it to "TouristRouteResourceParameters"
            //[FromQuery(Name = "Keyword")] string Keyword,
            //[FromQuery(Name = "Rating")] string Rating // lessThan, largerThan, equalTo
            )
        {
            var touristRoutesFromRepo =
                this._touristRouteRepository.GetTouristRoutes(
                    parameters.Keyword,
                    parameters.RatingOperator,
                    parameters.RatingValue
                    );
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
        [HttpGet("{TouristRouteId:Guid}", Name = "GetTouristRouteById")]
        [HttpHead("{TouristRouteId:Guid}", Name = "GetTouristRouteById")]
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

        // https://localhost:7143/api/touristRoutes
        [HttpPost]
        public IActionResult CreateTouristRoute(
            [FromBody] TouristRouteForCreatingDto touristRouteForCreatingDto
            )
        {
            var touristRoute = _mapper.Map<TouristRoute>(
                touristRouteForCreatingDto);

            _touristRouteRepository.AddTouristRoute(touristRoute);
            bool savedRepo = _touristRouteRepository.Save();

            var touristRouteDto = _mapper.Map<TouristRouteDto>(touristRoute);
            // Hatoas: in response header "Location", created resource in "GET"
            // e.g., https://localhost:7143/api/TouristRoutes/af4164ea-b21a-4af9-9e6a-861544bc24ff
            return CreatedAtRoute(
                "GetTouristRouteById",
                new { TouristRouteId = touristRouteDto.Id },
                touristRouteDto
                );
        }

        // https://localhost:7143/api/touristRoutes/{TouristRouteId}
        [HttpPut("{TouristRouteId:Guid}", Name = "UpdateTouristRoute")]
        public IActionResult UpdateTouristRoute(
            [FromRoute] Guid TouristRouteId,
            [FromBody] TouristRouteForUpdatingDto touristRouteForUpdatingDto
            )
        {
            if (!_touristRouteRepository.TouristRouteExists(TouristRouteId))
            {
                return NotFound($"Tourist route not found {TouristRouteId}");
            }

            var touristRouteRepo = _touristRouteRepository.GetTouristRoute(TouristRouteId);
            // 1. repo -> dto
            // 2. update dto
            // 3. dto -> repo (context updated, just need to commit)
            _mapper.Map(touristRouteForUpdatingDto, touristRouteRepo);
            // update
            _touristRouteRepository.Save();

            return NoContent();
        }
    }
}

