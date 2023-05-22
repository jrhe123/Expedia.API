using System;
using System.Security.Claims;
using AutoMapper;
using Expedia.API.Dtos;
using Expedia.API.Helpers;
using Expedia.API.Models;
using Expedia.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expedia.API.Controllers
{
    [Route("api/shoppingCart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITouristRouteRepository _touristRouteRepository;
        private readonly IMapper _mapper;

        public ShoppingCartController(
            IHttpContextAccessor httpContextAccessor,
            ITouristRouteRepository touristRouteRepository,
            IMapper mapper
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _touristRouteRepository = touristRouteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetShoppingCart()
        {

            // 1. get user from context
            var userId = _httpContextAccessor.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)
                .Value;

            // 2. get shopping cart by userId
            var shoppingCart = await _touristRouteRepository
                .GetShoppingCartByUserIdAsync(userId);

            return Ok(
                _mapper.Map<ShoppingCartDto>(shoppingCart)
                );
        }

        [HttpPost("items")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddShoppingCartItem(
            [FromBody] AddShoppingCartItemDto addShoppingCartItemDto
            )
        {
            // 1. get user from context
            var userId = _httpContextAccessor.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)
                .Value;

            // 2. get shopping cart by userId
            var shoppingCart = await _touristRouteRepository
                .GetShoppingCartByUserIdAsync(userId);

            // 3. add line item
            var touristRouteRepo = await _touristRouteRepository.GetTouristRouteAsync(
                addShoppingCartItemDto.TouristRouteId
                );
            if (touristRouteRepo == null)
            {
                return NotFound($"Tourist route not found {addShoppingCartItemDto.TouristRouteId}");
            }

            var lineItem = new LineItem()
            {
                TouristRouteId = addShoppingCartItemDto.TouristRouteId,
                ShoppingCartId = shoppingCart.Id,
                OriginalPrice = touristRouteRepo.OriginalPrice,
                DiscountPercent = touristRouteRepo.DiscountPercent
            };
            await _touristRouteRepository.AddShoppingCartItemAsync(lineItem);
            await _touristRouteRepository.SaveAsync();

            return Ok(
                _mapper.Map<ShoppingCartDto>(shoppingCart)
                );
        }


        [HttpDelete("items/{ItemId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteShoppingCartItem(
            [FromRoute] int ItemId
            )
        {
            var lineItem = await _touristRouteRepository
                .GetShoppingCartItemByItemIdAsync(ItemId);
            if (lineItem == null)
            {
                return NotFound($"Line item not found {ItemId}");
            }

            _touristRouteRepository.DeleteShoppingCartItem(lineItem);
            await _touristRouteRepository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("items/({ItemIds})")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteShoppingCartItems(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))][FromRoute] IEnumerable<int> ItemIds
            )
        {
            var lineItems = await _touristRouteRepository
                .GetShoppingCartItemByItemIdListAsync(ItemIds);

            _touristRouteRepository.DeleteShoppingCartItems(lineItems);
            await _touristRouteRepository.SaveAsync();

            return NoContent();
        }


        [HttpPost("checkout")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Checkout()
        {
            // 1. get user from context
            var userId = _httpContextAccessor.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)
                .Value;

            // 2. get shopping cart by userId
            var shoppingCart = await _touristRouteRepository
                .GetShoppingCartByUserIdAsync(userId);
            if (shoppingCart.ShoppingCartItems.Count == 0)
            {
                return BadRequest();
            }
            // 3. create order
            var order = new Order()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                State = OrderStateEnum.Pending,
                OrderItems = shoppingCart.ShoppingCartItems,
                CreateDateUTC = DateTime.UtcNow,
            };
            // 3.1 empty shopping cart
            shoppingCart.ShoppingCartItems = null;
            // 3.2 create order
            await _touristRouteRepository.AddOrderAsync(order);
            await _touristRouteRepository.SaveAsync();

            // 4. response
            return Ok(
                _mapper.Map<OrderDto>(order)
                );
        }

    }

}