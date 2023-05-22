using System;
using System.ComponentModel;
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

        public async Task<IEnumerable<TouristRoute>> GetTouristRoutesAsync(
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
            return await result.ToListAsync();
        }

        public async Task<TouristRoute> GetTouristRouteAsync(Guid TouristRouteId)
        {
            return await _context.TouristRoutes.Include(
                    item => item.TouristRoutePictures
                ).FirstOrDefaultAsync(
                    item => item.Id == TouristRouteId
                );
        }

        public async Task<bool> TouristRouteExistsAsync(Guid TouristRouteId)
        {
            return await _context.TouristRoutes.AnyAsync(
                    item => item.Id == TouristRouteId
                );
        }

        public async Task<IEnumerable<TouristRoutePicture>> GetPicutresByTouristRouteIdAsync(Guid TouristRouteId)
        {
            return await _context.TouristRoutePictures.Where(
                    item => item.TouristRouteId == TouristRouteId
                ).ToListAsync();
        }

        public async Task<TouristRoutePicture> GetPicutreAsync(int PictureId)
        {
            return await _context.TouristRoutePictures.FirstOrDefaultAsync(
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

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void DeleteTouristRoute(TouristRoute touristRoute)
        {
            _context.TouristRoutes.Remove(touristRoute);
        }

        public void DeleteTouristRoutePicture(TouristRoutePicture touristRoutePicture)
        {
            _context.TouristRoutePictures.Remove(touristRoutePicture);
        }

        public async Task<IEnumerable<TouristRoute>> GetTouristRoutesByIdListAsync(IEnumerable<Guid> ids)
        {
            return await _context.TouristRoutes.Where(
                    item => ids.Contains(item.Id)
                ).ToListAsync();
        }

        public void DeleteTouristRoutes(IEnumerable<TouristRoute> touristRoutes)
        {
            // delete many
            _context.TouristRoutes.RemoveRange(touristRoutes);
        }


        //=============================
        public async Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId)
        {
            return await _context.ShoppingCarts
                .Include(s => s.User)
                .Include(s => s.ShoppingCartItems)
                .ThenInclude(li => li.TouristRoute)
                .Where(s => s.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task CreateShoppingCartAsync(ShoppingCart shoppingCart)
        {
            await _context.ShoppingCarts.AddAsync(shoppingCart);
        }

        public async Task AddShoppingCartItemAsync(LineItem lineItem)
        {
            await _context.LineItems.AddAsync(lineItem);
        }

        public async Task<LineItem> GetShoppingCartItemByItemIdAsync(int lineItemId)
        {
            return await _context.LineItems.Where(item => item.Id == lineItemId)
                    .FirstOrDefaultAsync();
        }

        public void DeleteShoppingCartItem(LineItem lineItem)
        {
            _context.LineItems.Remove(lineItem);
        }

        public async Task<IEnumerable<LineItem>> GetShoppingCartItemByItemIdListAsync(IEnumerable<int> ids)
        {
            return await _context.LineItems
                .Where(item => ids.Contains(item.Id))
                .ToListAsync();
        }

        public void DeleteShoppingCartItems(IEnumerable<LineItem> lineItems)
        {
            _context.LineItems.RemoveRange(lineItems);
        }
    }
}

