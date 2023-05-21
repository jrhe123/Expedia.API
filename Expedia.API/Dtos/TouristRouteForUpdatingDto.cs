using System;
using Expedia.API.Models;
using System.ComponentModel.DataAnnotations;
using Expedia.API.ValidationAttributes;

namespace Expedia.API.Dtos
{
    public class TouristRouteForUpdatingDto : TouristRouteForManipulationDto
    {
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(1500)]
        public override string Description { get; set; }
    }
}

