using System;
using Expedia.API.Models;

namespace Expedia.API.Services
{
	public interface ITouristRouteRepository
	{
		// list
		IEnumerable<TouristRoute> GetTouristRoutes();

		// get by id
		TouristRoute GetTouristRoute(Guid TouristRouteId);
    }
}

