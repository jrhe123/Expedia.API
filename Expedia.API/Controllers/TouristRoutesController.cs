using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expedia.API.Dtos;
using Expedia.API.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Text.RegularExpressions;

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
            [FromQuery(Name = "Keyword")] string Keyword,
            [FromQuery(Name = "Rating")] string Rating // lessThan, largerThan, equalTo
            )
        {
            Regex regex = new Regex(@"([A-Za-z0-9\-]+)(\d+)");
            string operatorType = "";
            int ratingValue = -1;
            Match match = regex.Match(Rating);
            if (match.Success)
            {
                operatorType = match.Groups[1].Value;
                ratingValue = Int32.Parse(match.Groups[2].Value);
            }

            var touristRoutesFromRepo =
                this._touristRouteRepository.GetTouristRoutes(
                    Keyword,
                    operatorType,
                    ratingValue
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

