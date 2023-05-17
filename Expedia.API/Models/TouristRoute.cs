using System;
namespace Expedia.API.Models
{
	public class TouristRoute
	{
		public Guid Id { get; set; }

		public string Title { get; set; }
		public string Description { get; set; }
		public decimal OriginalPrice { get; set; }
		public double? DiscountPercent { get; set; } // ? nullable
		public DateTime CreateTime { get; set; }
		public DateTime? UpdateTime { get; set; }
        public DateTime? DepartureTime { get; set; }
		public string Features { get; set; }
		public string Fees { get; set; } // fee description
        public string Notes { get; set; }

		public ICollection<TouristRoutePicture> TouristRoutePictures { get; set; }
    }
}

