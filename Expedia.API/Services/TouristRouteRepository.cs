using System;
using Expedia.API.Database;
using Expedia.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Expedia.API.Services
{
	public class TouristRouteRepository: ITouristRouteRepository
	{
        private readonly AppDbContext _context;

		public TouristRouteRepository(AppDbContext context)
		{
            _context = context;
        }

        public IEnumerable<TouristRoute> GetTouristRoutes(string Keyword)
        {
            // return _context.TouristRoutes;

            IQueryable<TouristRoute> result = _context.TouristRoutes
                .Include(
                    item => item.TouristRoutePictures
                );
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                Keyword = Keyword.Trim();
                result = result.Where(item => item.Title.Contains(Keyword));
            }
            // include / join
            return result.ToList();
        }

        public TouristRoute GetTouristRoute(Guid TouristRouteId)
        {
            return _context.TouristRoutes.Include(
                item => item.TouristRoutePictures
                ).FirstOrDefault(
                item => item.Id == TouristRouteId
                );
        }

        public bool TouristRouteExists(Guid TouristRouteId)
        {
            return _context.TouristRoutes.Any(
                item => item.Id == TouristRouteId
                );
        }

        public IEnumerable<TouristRoutePicture> GetPicutresByTouristRouteId(Guid TouristRouteId)
        {
            return _context.TouristRoutePictures.Where(
                item => item.TouristRouteId == TouristRouteId
                ).ToList();
        }

        public TouristRoutePicture GetPicutre(int PictureId)
        {
            return _context.TouristRoutePictures.FirstOrDefault(
                item => item.Id == PictureId
                );
        }
    }
}

