using System;
using Expedia.API.Models;
using Stateless;

namespace Expedia.API.Dtos
{
	public class OrderDto
	{
        public Guid Id { get; set; }


        public string UserId { get; set; }

        public ICollection<LineItemDto> OrderItems { get; set; }

        public string State { get; set; }

        public DateTime CreateDateUTC { get; set; }

        public string TransactionMetadata { get; set; }

    }
}

