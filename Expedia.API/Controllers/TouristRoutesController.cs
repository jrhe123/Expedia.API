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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Expedia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;
        private readonly IPropertyMappingService _propertyMappingService;

        public TouristRoutesController(
            ITouristRouteRepository touristRouteRepository,
            IMapper mapper,

            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            IPropertyMappingService propertyMappingService
        )
        {
            _touristRouteRepository = touristRouteRepository;
            _mapper = mapper;
            //
            _urlHelper = urlHelperFactory.GetUrlHelper(
                actionContextAccessor.ActionContext);
            _propertyMappingService = propertyMappingService;
        }

        private string GenerateTouristRouteResourceURL(
            TouristRouteResourceParameters parameters,
            PaginationResourceParameters parameters2,
            ResourceUriType type
            )
        {
            return type switch
            {
                ResourceUriType.PreviousPage => _urlHelper.Link("GetTouristRoutes",
                    new
                    {
                        fields = parameters.Fields,
                        orderBy = parameters.OrderBy,
                        keyword = parameters.Keyword,
                        rating = parameters.Rating,
                        pageNumber = parameters2.PageNumber - 1,
                        pageSize = parameters2.PageSize,
                    }),
                ResourceUriType.NextPage => _urlHelper.Link("GetTouristRoutes",
                    new
                    {
                        fields = parameters.Fields,
                        orderBy = parameters.OrderBy,
                        keyword = parameters.Keyword,
                        rating = parameters.Rating,
                        pageNumber = parameters2.PageNumber + 1,
                        pageSize = parameters2.PageSize,
                    }),
                _ => _urlHelper.Link("GetTouristRoutes",
                    new
                    {
                        fields = parameters.Fields,
                        orderBy = parameters.OrderBy,
                        keyword = parameters.Keyword,
                        rating = parameters.Rating,
                        pageNumber = parameters2.PageNumber,
                        pageSize = parameters2.PageSize,
                    }),
            };
        }

        // https://localhost:7143/api/touristRoutes?Keyword=xxx&Rating=largerThan3
        [HttpGet(Name = "GetTouristRoutes")]
        [HttpHead]
        public async Task<IActionResult> GetTouristRoutes(
            [FromQuery] TouristRouteResourceParameters parameters,
            [FromQuery] PaginationResourceParameters parameters2

            // move it to "TouristRouteResourceParameters"
            //[FromQuery(Name = "Keyword")] string Keyword,
            //[FromQuery(Name = "Rating")] string Rating // lessThan, largerThan, equalTo
            )
        {
            // validate orderBy string is valid
            if (!_propertyMappingService
                .IsMappingExists<TouristRouteDto, TouristRoute>(parameters.OrderBy))
            {
                return BadRequest("OrderBy is invalid");
            }
            if (!_propertyMappingService
                .IsPropertiesExists<TouristRouteDto>(parameters.Fields))
            {
                return BadRequest("Fields is invalid");
            }

            var touristRoutesFromRepo =
                await this._touristRouteRepository.GetTouristRoutesAsync(
                    parameters.Keyword,
                    parameters.RatingOperator,
                    parameters.RatingValue,
                    parameters2.PageSize,
                    parameters2.PageNumber,
                    parameters.OrderBy
                    );
            if (touristRoutesFromRepo == null ||
                touristRoutesFromRepo.Count() == 0)
            {
                return NotFound("tourist route no found");
            }
            var touristRoutesDto = _mapper.Map<IEnumerable<TouristRouteDto>>(
                touristRoutesFromRepo);

            // Hatoas: prepare for x-pagination into response headers
            var previousPageLink = touristRoutesFromRepo.HasPrevious
                ? GenerateTouristRouteResourceURL(
                    parameters, parameters2, ResourceUriType.PreviousPage
                    ) : null;
            var nextPageLink = touristRoutesFromRepo.HasNext
                ? GenerateTouristRouteResourceURL(
                    parameters, parameters2, ResourceUriType.NextPage
                    ) : null;

            var paginationMetadata = new
            {
                previousPageLink,
                nextPageLink,
                totalCount = touristRoutesFromRepo.TotalCount,
                pageSize = touristRoutesFromRepo.PageSize,
                currentPage = touristRoutesFromRepo.CurrentPage,
                totalPages = touristRoutesFromRepo.TotalPage,
            };
            Response.Headers.Add(
                "x-pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata)
            );
            return Ok(
                touristRoutesDto.ShapeData(parameters.Fields)
                );
        }

        private IEnumerable<LinkDto> CreateLinkForTouristRoute(
            Guid TouristRouteId,
            string? Fields
        )
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(
                    Url.Link("GetTouristRouteById", new { TouristRouteId, Fields }),
                    "self",
                    "GET"
                    )
                );
            // UPDATE
            links.Add(
                new LinkDto(
                    Url.Link("UpdateTouristRoute", new { TouristRouteId }),
                    "update",
                    "PUT"
                    )
                );
            // PARTIAL UPDATE
            links.Add(
                new LinkDto(
                    Url.Link("PartiallyUpdateTouristRoute", new { TouristRouteId }),
                    "partially_update",
                    "PATCH"
                    )
                );
            // DELETE
            links.Add(
                new LinkDto(
                    Url.Link("DeleteTouristRoute", new { TouristRouteId }),
                    "delete",
                    "DELETE"
                    )
                );
            // GET PICTURE
            links.Add(
                new LinkDto(
                    Url.Link("GetPictureListForTouristRoute", new { TouristRouteId }),
                    "get_pictures",
                    "GET"
                    )
                );
            // CREATE PICTURE
            links.Add(
                new LinkDto(
                    Url.Link("CreateTouristRoutePicture", new { TouristRouteId }),
                    "create_picture",
                    "POST"
                    )
                );

            return links;
        }

        // https://localhost:7143/api/touristRoutes/{TouristRouteId}
        [HttpGet("{TouristRouteId:Guid}", Name = "GetTouristRouteById")]
        [HttpHead("{TouristRouteId:Guid}", Name = "GetTouristRouteById")]
        public async Task<IActionResult> GetTouristRouteById(
            Guid TouristRouteId,
            string? Fields
            )
        {
            if (!_propertyMappingService
                .IsPropertiesExists<TouristRouteDto>(Fields))
            {
                return BadRequest("Fields is invalid");
            }

            var touristRouteFromRepo = await this._touristRouteRepository.GetTouristRouteAsync(
                TouristRouteId
                );
            if (touristRouteFromRepo == null)
            {
                return NotFound($"tourist route not found {TouristRouteId}");
            }
            var touristRouteDto = _mapper.Map<TouristRouteDto>(
                touristRouteFromRepo);

            // HATEOAS links
            var linkDtos = CreateLinkForTouristRoute(TouristRouteId, Fields);

            var result = touristRouteDto.ShapeData(Fields)
                as IDictionary<string, object>;
            result.Add("links", linkDtos);

            return Ok(result);
        }

        // https://localhost:7143/api/touristRoutes
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "Admin")]
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

