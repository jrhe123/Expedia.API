using System;
using Expedia.API.Models;

namespace Expedia.API.Services
{
	public interface ITouristRouteRepository
	{
		// list
		IEnumerable<TouristRoute> GetTouristRoutes(string Keyword);

		// get by id
		TouristRoute GetTouristRoute(Guid TouristRouteId);

		// check if exists
		bool TouristRouteExists(Guid TouristRouteId);

		// get pictures
		IEnumerable<TouristRoutePicture> GetPicutresByTouristRouteId
			(Guid TouristRouteId);

        // get picture by pic id
        TouristRoutePicture GetPicutre(int PictureId);
    }
}

