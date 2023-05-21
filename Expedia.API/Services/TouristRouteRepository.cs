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

        public IEnumerable<TouristRoute> GetTouristRoutes(
            string Keyword,
            string RatingOperator,
            int? RatingValue
        )
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
            if (RatingValue >= 0)
            {
                //switch(RatingOperator)
                //{
                //    case "largerThan":
                //        result = result.Where(item => item.Rating >= RatingValue);
                //        break;
                //    case "lessThan":
                //        result = result.Where(item => item.Rating <= RatingValue);
                //        break;
                //    case "equalTo":
                //        result = result.Where(item => item.Rating == RatingValue);
                //        break;
                //}
                result = RatingOperator switch
                {
                    "largerThan" => result.Where(item => item.Rating >= RatingValue),
                    "lessThan" => result.Where(item => item.Rating <= RatingValue),
                    _ => result.Where(item => item.Rating == RatingValue),
                };
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

        public void AddTouristRoute(TouristRoute touristRoute)
        {
            if (touristRoute == null)
            {
                throw new ArgumentNullException(nameof(touristRoute));
            }
            // only save in ram, need to commit it
            _context.TouristRoutes.Add(touristRoute);
        }


        public void AddTouristRoutePicture(Guid TouristRouteId, TouristRoutePicture touristRoutePicture)
        {
            if (TouristRouteId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(TouristRouteId));
            }
            if (touristRoutePicture == null)
            {
                throw new ArgumentNullException(nameof(touristRoutePicture));
            }

            touristRoutePicture.TouristRouteId = TouristRouteId;
            _context.TouristRoutePictures.Add(touristRoutePicture);
        }



        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public void DeleteTouristRoute(TouristRoute touristRoute)
        {
            _context.TouristRoutes.Remove(touristRoute);
        }
    }
}

