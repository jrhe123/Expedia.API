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
using Microsoft.AspNetCore.JsonPatch;
using Expedia.API.Helpers;

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
        public async Task<IActionResult> GetTouristRoutes(
            [FromQuery] TouristRouteResourceParameters parameters

            // move it to "TouristRouteResourceParameters"
            //[FromQuery(Name = "Keyword")] string Keyword,
            //[FromQuery(Name = "Rating")] string Rating // lessThan, largerThan, equalTo
            )
        {
            var touristRoutesFromRepo =
                await this._touristRouteRepository.GetTouristRoutesAsync(
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
        public async Task<IActionResult> GetTouristRouteById(Guid TouristRouteId)
        {
            var touristRouteFromRepo = await this._touristRouteRepository.GetTouristRouteAsync(
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
        public async Task<IActionResult> CreateTouristRoute(
            [FromBody] TouristRouteForCreatingDto touristRouteForCreatingDto
            )
        {
            var touristRoute = _mapper.Map<TouristRoute>(
                touristRouteForCreatingDto);

            _touristRouteRepository.AddTouristRoute(touristRoute);
            bool savedRepo = await _touristRouteRepository.SaveAsync();

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
        public async Task<IActionResult> UpdateTouristRoute(
            [FromRoute] Guid TouristRouteId,
            [FromBody] TouristRouteForUpdatingDto touristRouteForUpdatingDto
            )
        {
            if (!(await _touristRouteRepository.TouristRouteExistsAsync(TouristRouteId)))
            {
                return NotFound($"Tourist route not found {TouristRouteId}");
            }

            var touristRouteRepo = await _touristRouteRepository.GetTouristRouteAsync(TouristRouteId);
            // 1. repo -> dto
            // 2. update dto
            // 3. dto -> repo (context updated, just need to commit)
            _mapper.Map(touristRouteForUpdatingDto, touristRouteRepo);
            // update
            await _touristRouteRepository.SaveAsync();

            return NoContent();
        }

        // https://localhost:7143/api/touristRoutes/{TouristRouteId}
        [HttpPatch("{TouristRouteId:Guid}", Name = "PartialUpdateTouristRoute")]
        public async Task<IActionResult> PartialUpdateTouristRoute(
            [FromRoute] Guid TouristRouteId,
            [FromBody] JsonPatchDocument<TouristRouteForUpdatingDto> PatchDocument
            )
        {
            if (!(await _touristRouteRepository.TouristRouteExistsAsync(TouristRouteId)))
            {
                return NotFound($"Tourist route not found {TouristRouteId}");
            }

            var touristRouteRepo = await _touristRouteRepository.GetTouristRouteAsync(
                TouristRouteId);
            var touristRouteForUpdatingDto = _mapper.Map<TouristRouteForUpdatingDto>(
                touristRouteRepo);
            // partial updates
            PatchDocument.ApplyTo(touristRouteForUpdatingDto, ModelState);

            // b/c of JsonPatchDocument cannot validate data for "TouristRouteForUpdatingDto"
            // we check the data validate now (ModelState)
            if (!TryValidateModel(touristRouteForUpdatingDto))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(touristRouteForUpdatingDto, touristRouteRepo);
            await _touristRouteRepository.SaveAsync();

            return NoContent();
        }


        [HttpDelete("{TouristRouteId:Guid}", Name = "DeleteTouristRoute")]
        public async Task<IActionResult> DeleteTouristRoute(
            [FromRoute] Guid TouristRouteId
            )
        {
            if (!(await _touristRouteRepository.TouristRouteExistsAsync(TouristRouteId)))
            {
                return NotFound($"Tourist route not found {TouristRouteId}");
            }

            var touristRouteRepo = await _touristRouteRepository.GetTouristRouteAsync(
                TouristRouteId);
            _touristRouteRepository.DeleteTouristRoute(touristRouteRepo);
            await _touristRouteRepository.SaveAsync();

            return NoContent();
        }

        // https://localhost:7143/api/touristRoutes/{TouristRouteIds}
        [HttpDelete("({TouristRouteIds})")]
        public async Task<IActionResult> DeleteByIds(
            // convert "guids" to array of guids
            [ModelBinder(BinderType = typeof(ArrayModelBinder))][FromRoute] IEnumerable<Guid> TouristRouteIds
            )
        {
            if (TouristRouteIds == null)
            {
                return BadRequest();
            }

            var touristRoutesFromRepo = await _touristRouteRepository.GetTouristRoutesByIdListAsync(
                TouristRouteIds
                );
            _touristRouteRepository.DeleteTouristRoutes(touristRoutesFromRepo);
            await _touristRouteRepository.SaveAsync();

            return NoContent();
        }
    }
}

