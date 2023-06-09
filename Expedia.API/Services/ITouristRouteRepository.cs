﻿using System;
using Expedia.API.Models;
using System.Threading.Tasks;
using Expedia.API.Helpers;

namespace Expedia.API.Services
{
	public interface ITouristRouteRepository
	{
		// get list
		Task<PaginationList<TouristRoute>> GetTouristRoutesAsync(
			string Keyword,
			string RatingOperator,
			int? RatingValue,
            int PageSize,
            int PageNumber,
            string OrderBy
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

        //=============================
        // get shopping cart by userId
        Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId);

        // create shopping cart
        Task CreateShoppingCartAsync(ShoppingCart shoppingCart);

        // add shopping cart item
        Task AddShoppingCartItemAsync(LineItem lineItem);

        // get shopping cart item by id
        Task<LineItem> GetShoppingCartItemByItemIdAsync(int lineItemId);

        // delete shopping cart item
        void DeleteShoppingCartItem(LineItem lineItem);

        // get shopping cart items by ids
        Task<IEnumerable<LineItem>> GetShoppingCartItemByItemIdListAsync
            (IEnumerable<int> ids);

        // delete shopping cart items
        void DeleteShoppingCartItems(IEnumerable<LineItem> lineItems);

        // add order
        Task AddOrderAsync(Order order);

        // get orders by user id
        Task<PaginationList<Order>> GetOrdersByUserIdAsync(
            string userId,
            int pageSize,
            int pageNumber
        );

        // get order by order id
        Task<Order> GetOrderByIdAsync(Guid orderId);
    }
}

