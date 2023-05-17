using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expedia.API.Models
{
	public class TouristRoute
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Title { get; set; }
		[Required]
		[MaxLength(1500)]
		public string Description { get; set; }
		[Column(TypeName ="decimal(18, 2)")]
		public decimal OriginalPrice { get; set; }
		[Range(0.0, 1.0)]
		public double? DiscountPercent { get; set; } // ? nullable
		public DateTime CreateTime { get; set; }
		public DateTime? UpdateTime { get; set; }
        public DateTime? DepartureTime { get; set; }
		[MaxLength]
		public string Features { get; set; }
        [MaxLength]
        public string Fees { get; set; } // fee description
        [MaxLength]
        public string Notes { get; set; }

		public ICollection<TouristRoutePicture> TouristRoutePictures { get; set; }
			= new List<TouristRoutePicture>();
    }
}

