using System;
using System.ComponentModel.DataAnnotations;

namespace Expedia.API.Dtos
{
	public class AddShoppingCartItemDto
	{
		[Required]
		public Guid TouristRouteId { get; set; }
	}
}

