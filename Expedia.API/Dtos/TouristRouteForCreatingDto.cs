using System;
using System.ComponentModel.DataAnnotations;
using Expedia.API.Models;
using Expedia.API.ValidationAttributes;

namespace Expedia.API.Dtos
{
    
    public class TouristRouteForCreatingDto : TouristRouteForManipulationDto//: IValidatableObject
    {
        // move to "TouristRouteTitleMustBeDifferentFromDescriptionAttribute"
        //public IEnumerable<ValidationResult> Validate(
        //    ValidationContext validationContext)
        //{
        //}
    }
}

