using System;
using System.ComponentModel.DataAnnotations;

namespace Expedia.API.Models
{
    public enum OrderStateEnum
    {
        Pending,
        Processing,
        Completed,
        Declined,
        Cancelled,
        Refund,
    }

	public class Order
	{
        [Key]
        public Guid Id { get; set; }


        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        public ICollection<LineItem> OrderItems { get; set; }

        public OrderStateEnum State { get; set; }

        public DateTime CreateDateUTC { get; set; }

        public string TransactionMetadata { get; set; }
    }
}

