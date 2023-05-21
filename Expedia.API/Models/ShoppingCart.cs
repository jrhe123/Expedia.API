using System;
using System.ComponentModel.DataAnnotations;

namespace Expedia.API.Models
{
	public class ShoppingCart
	{
		[Key]
		public Guid Id { get; set; }


		public string UserId { get; set; }
		public ApplicationUser User { get; set; }


		public ICollection<LineItem> ShoppingCartItems { get; set; }
	}
}

