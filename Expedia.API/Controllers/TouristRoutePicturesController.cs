using System;
using System.Data;
using AutoMapper;
using Expedia.API.Dtos;
using Expedia.API.Models;
using Expedia.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expedia.API.Controllers
{
    [Route("api/touristRoutes/{TouristRouteId}/pictures")]
    [ApiController]
    public class TouristRoutePicturesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        private readonly IMapper _mapper;

        public TouristRoutePicturesController(
            ITouristRouteRepository touristRouteRepository,
            IMapper mapper
        )
        {
            _touristRouteRepository = touristRouteRepository ??
                throw new ArgumentNullException(nameof(touristRouteRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper)); ;
        }


        // https://localhost:7143/api/touristRoutes/fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1/pictures
        [HttpGet]
        public async Task<IActionResult> GetPictureListForTouristRoute(Guid TouristRouteId)
        {
            if (!(await _touristRouteRepository.TouristRouteExistsAsync(TouristRouteId)))
            {
                return NotFound("tourist route no found");
            }

            var picturesFromRepo = await _touristRouteRepository.GetPicutresByTouristRouteIdAsync(TouristRouteId);
            if (picturesFromRepo == null || picturesFromRepo.Count() == 0)
            {
                return NotFound("tourist route pictures no found");
            }

            return Ok(
                _mapper.Map<IEnumerable<TouristRoutePictureDto>>(picturesFromRepo)
                );
        }

        // https://localhost:7143/api/touristRoutes/fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1/pictures/1
        [HttpGet("{PictureId}", Name = "GetPicture")]
        public async Task<IActionResult> GetPicture(Guid TouristRouteId, int PictureId)
        {
            if (!(await _touristRouteRepository.TouristRouteExistsAsync(TouristRouteId)))
            {
                return NotFound("tourist route no found");
            }

            var pictureFromRepo = await _touristRouteRepository.GetPicutreAsync(PictureId);
            if (pictureFromRepo == null)
            {
                return NotFound($"tourist route picture no found {PictureId}");
            }

            return Ok(
                _mapper.Map<TouristRoutePictureDto>(pictureFromRepo)
                );
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTouristRoutePicture(
            [FromRoute] Guid TouristRouteId,
            [FromBody] TouristRoutePictureForCreatingDto touristRoutePictureForCreatingDto
            )
        {
            if (!(await _touristRouteRepository.TouristRouteExistsAsync(TouristRouteId)))
            {
                return NotFound("tourist route no found");
            }

            var touristRoutePicture = _mapper.Map<TouristRoutePicture>(
                touristRoutePictureForCreatingDto);
            _touristRouteRepository.AddTouristRoutePicture(
                TouristRouteId,
                touristRoutePicture
                );
            await _touristRouteRepository.SaveAsync();

            var touristRoutePictureDto = _mapper.Map<TouristRoutePictureDto>(
                touristRoutePicture);

            return CreatedAtRoute(
                "GetPicture",
                 new {
                     TouristRouteId = touristRoutePicture.TouristRouteId,
                     PictureId = touristRoutePicture.Id,
                     },
                 touristRoutePictureDto
                );
        }


        // https://localhost:7143/api/touristRoutes/fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1/pictures/1
        [HttpDelete("{PictureId}", Name = "DeleteTouristRoutePicture")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTouristRoutePicture(
            [FromRoute] Guid TouristRouteId,
            [FromRoute] int PictureId
            )
        {
            if (!(await _touristRouteRepository.TouristRouteExistsAsync(TouristRouteId)))
            {
                return NotFound("tourist route no found");
            }

            var pictureFromRepo = await _touristRouteRepository.GetPicutreAsync(PictureId);
            if (pictureFromRepo == null)
            {
                return NotFound($"tourist route picture no found {PictureId}");
            }

            _touristRouteRepository.DeleteTouristRoutePicture(pictureFromRepo);
            await _touristRouteRepository.SaveAsync();

            return NoContent();
        }
    }
}

