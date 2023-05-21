using System;
using Expedia.API.Models;

namespace Expedia.API.Services
{
	public interface ITouristRouteRepository
	{
		// list
		IEnumerable<TouristRoute> GetTouristRoutes(
			string Keyword,
			string RatingOperator,
			int? RatingValue
            );

		// get by id
		TouristRoute GetTouristRoute(Guid TouristRouteId);

		// check if exists
		bool TouristRouteExists(Guid TouristRouteId);

		// get pictures
		IEnumerable<TouristRoutePicture> GetPicutresByTouristRouteId
			(Guid TouristRouteId);

        // get picture by pic id
        TouristRoutePicture GetPicutre(int PictureId);

		// create
		void AddTouristRoute(TouristRoute touristRoute);

		// create picture
		void AddTouristRoutePicture(
            Guid TouristRouteId,
            TouristRoutePicture touristRoutePicture
			);

		// delete
		void DeleteTouristRoute(TouristRoute touristRoute);

		// save repo
		bool Save();
    }
}

