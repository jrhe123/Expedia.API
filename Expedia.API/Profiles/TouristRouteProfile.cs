using System;
using AutoMapper;
using Expedia.API.Dtos;
using Expedia.API.Models;

namespace Expedia.API.Profiles
{
	public class TouristRouteProfile : Profile
    {
		public TouristRouteProfile()
        {
            /**
             *  var touristRouteDto = new TouristRouteDto()
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
             */

            // mapping
            CreateMap<TouristRoute, TouristRouteDto>()
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(
                        src => src.OriginalPrice * (decimal)(src.DiscountPercent ?? 1)
                        )
                )
                .ForMember(
                    dest => dest.TravelDays,
                    opt => opt.MapFrom(
                        src => src.TravelDays.ToString()
                        )
                )
                .ForMember(
                    dest => dest.TripType,
                    opt => opt.MapFrom(
                        src => src.TripType.ToString()
                        )
                )
                .ForMember(
                    dest => dest.DepartureCity,
                    opt => opt.MapFrom(
                        src => src.DepartureCity.ToString()
                        )
                );
        }
    }
}

