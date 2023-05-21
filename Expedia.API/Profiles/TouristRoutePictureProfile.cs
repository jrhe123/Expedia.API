using System;
using AutoMapper;
using Expedia.API.Dtos;
using Expedia.API.Models;

namespace Expedia.API.Profiles
{
	public class TouristRoutePictureProfile : Profile
    {
		public TouristRoutePictureProfile()
		{
            // mapping
            CreateMap<TouristRoutePicture, TouristRoutePictureDto>();

            // create
            CreateMap<TouristRoutePictureForCreatingDto, TouristRoutePicture>();

            // patch update
            CreateMap<TouristRoutePicture, TouristRoutePictureForCreatingDto>();
        }
    }
}

