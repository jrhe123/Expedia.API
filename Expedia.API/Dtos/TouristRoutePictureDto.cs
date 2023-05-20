using System;

namespace Expedia.API.Dtos
{
	public class TouristRoutePictureDto
	{
        public int Id { get; set; }
        public string Url { get; set; }
        public Guid TouristRouteId { get; set; }
    }
}

