using System;
using System.ComponentModel.DataAnnotations;
using Expedia.API.Dtos;

namespace Expedia.API.ValidationAttributes
{
	public class TouristRouteTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
	{

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            var touristRouteDto = (TouristRouteForCreatingDto)validationContext.ObjectInstance;

            if (touristRouteDto.Title == touristRouteDto.Description)
            {
                return new ValidationResult(
                    "Title must be different from Description",
                    new[] { "TouristRouteForCreatingDto" }
                    );
            }

            return ValidationResult.Success;
        }
    }
}

