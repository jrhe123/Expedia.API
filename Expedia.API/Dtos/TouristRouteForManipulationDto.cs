using System;
using Expedia.API.Models;
using System.ComponentModel.DataAnnotations;
using Expedia.API.ValidationAttributes;

namespace Expedia.API.Dtos
{
    [TouristRouteTitleMustBeDifferentFromDescriptionAttribute]
    public abstract class TouristRouteForManipulationDto
	{

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(1500)]
        public virtual string Description { get; set; }
        public decimal OriginalPrice { get; set; }
        public double? DiscountPercent { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string? Features { get; set; }
        public string? Fees { get; set; }
        public string? Notes { get; set; }
        public double? Rating { get; set; }
        public TravelDays? TravelDays { get; set; }
        public TripType? TripType { get; set; }
        public DepartureCity DepartureCity { get; set; }
        public ICollection<TouristRoutePictureForCreatingDto> TouristRoutePictures { get; set; }
            = new List<TouristRoutePictureForCreatingDto>();


    }
}

