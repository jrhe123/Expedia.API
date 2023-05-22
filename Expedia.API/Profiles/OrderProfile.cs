using System;
using AutoMapper;
using Expedia.API.Dtos;
using Expedia.API.Models;

namespace Expedia.API.Profiles
{
	public class OrderProfile : Profile
    {
		public OrderProfile()
		{
			CreateMap<Order, OrderDto>()
				.ForMember( // convert enum to string
					dest => dest.State,
					opt =>
					{
						opt.MapFrom(src => src.State.ToString());
					}
				);
        }
	}
}

