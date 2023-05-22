using System;
using AutoMapper;
using Expedia.API.Dtos;
using Expedia.API.Models;

namespace Expedia.API.Profiles
{
	public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            //
            CreateMap<ShoppingCart, ShoppingCartDto>();

            CreateMap<LineItem, LineItemDto>();
        }
	}
}

