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

		// get by ids
		IEnumerable<TouristRoute> GetTouristRoutesByIdList(IEnumerable<Guid> ids);

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

		// delete tourist route(s)
		void DeleteTouristRoutes(IEnumerable<TouristRoute> touristRoutes);

        // delete picture
        void DeleteTouristRoutePicture(TouristRoutePicture touristRoutePicture);

        // save repo
        bool Save();
    }
}

