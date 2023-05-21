using System;
using Expedia.API.Models;
using System.Threading.Tasks;

namespace Expedia.API.Services
{
	public interface ITouristRouteRepository
	{
		// get list
		Task<IEnumerable<TouristRoute>> GetTouristRoutesAsync(
			string Keyword,
			string RatingOperator,
			int? RatingValue
            );

        // get by id
        Task<TouristRoute> GetTouristRouteAsync(Guid TouristRouteId);

        // get by ids
        Task<IEnumerable<TouristRoute>> GetTouristRoutesByIdListAsync(IEnumerable<Guid> ids);

        // check if exists
        Task<bool> TouristRouteExistsAsync(Guid TouristRouteId);

        // get pictures
        Task<IEnumerable<TouristRoutePicture>> GetPicutresByTouristRouteIdAsync
			(Guid TouristRouteId);

        // get picture by pic id
        Task<TouristRoutePicture> GetPicutreAsync(int PictureId);

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
        Task<bool> SaveAsync();
    }
}

