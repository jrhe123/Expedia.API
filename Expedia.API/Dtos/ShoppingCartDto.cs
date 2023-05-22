using System;
using Expedia.API.Models;

namespace Expedia.API.Dtos
{
	public class ShoppingCartDto
	{
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public ICollection<LineItemDto> ShoppingCartItems { get; set; }
    }
}

