using System;
using AutoMapper;
using Expedia.API.Dtos;
using Expedia.API.Models;
using Expedia.API.Services;
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
        public IActionResult GetPictureListForTouristRoute(Guid TouristRouteId)
        {
            if (!_touristRouteRepository.TouristRouteExists(TouristRouteId))
            {
                return NotFound("tourist route no found");
            }

            var picturesFromRepo = _touristRouteRepository.GetPicutresByTouristRouteId(TouristRouteId);
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
        public IActionResult GetPicture(Guid TouristRouteId, int PictureId)
        {
            if (!_touristRouteRepository.TouristRouteExists(TouristRouteId))
            {
                return NotFound("tourist route no found");
            }

            var pictureFromRepo = _touristRouteRepository.GetPicutre(PictureId);
            if (pictureFromRepo == null)
            {
                return NotFound($"tourist route picture no found {PictureId}");
            }

            return Ok(
                _mapper.Map<TouristRoutePictureDto>(pictureFromRepo)
                );
        }

        [HttpPost]
        public IActionResult CreateTouristRoutePicture(
            [FromRoute] Guid TouristRouteId,
            [FromBody] TouristRoutePictureForCreatingDto touristRoutePictureForCreatingDto
            )
        {
            if (!_touristRouteRepository.TouristRouteExists(TouristRouteId))
            {
                return NotFound("tourist route no found");
            }

            var touristRoutePicture = _mapper.Map<TouristRoutePicture>(
                touristRoutePictureForCreatingDto);
            _touristRouteRepository.AddTouristRoutePicture(
                TouristRouteId,
                touristRoutePicture
                );
            _touristRouteRepository.Save();

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
    }
}

