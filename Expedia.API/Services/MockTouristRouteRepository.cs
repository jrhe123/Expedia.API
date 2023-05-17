using System;
using Expedia.API.Models;

namespace Expedia.API.Services
{
    public class MockTouristRouteRepository : ITouristRouteRepository
    {
        private List<TouristRoute> _routes;

        public MockTouristRouteRepository()
        {
            if (_routes == null)
            {
                InitializeTouristRoutes();
            }
        }

        private void InitializeTouristRoutes()
        {
            _routes = new List<TouristRoute>
            {
                new TouristRoute
                {
                    Id = Guid.NewGuid(),
                    Title = "Toronto",
                    Description = "GTA is funnnn",
                    OriginalPrice = 1299,
                    Features = "<p>one day tour</p>",
                    Fees = "<p>transportation fee</p>",
                    Notes = "<p>biggest city in CA</p>",
                },
                new TouristRoute
                {
                    Id = Guid.NewGuid(),
                    Title = "Montreal",
                    Description = "we speak french",
                    OriginalPrice = 1499,
                    Features = "<p>five day tour</p>",
                    Fees = "<p>transportation fee</p>",
                    Notes = "<p>biggest french city in CA</p>",
                },
            };
        }

        public IEnumerable<TouristRoute> GetTouristRoutes()
        {
            return _routes;
        }

        public TouristRoute GetTouristRoute(Guid TouristRouteId)
        {
            // linq
            return _routes.FirstOrDefault(item => item.Id == TouristRouteId);
        }

        
    }
}

