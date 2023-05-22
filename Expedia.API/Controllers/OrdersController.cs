using System;
using System.Security.Claims;
using AutoMapper;
using Expedia.API.Dtos;
using Expedia.API.Models;
using Expedia.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expedia.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITouristRouteRepository _touristRouteRepository;
        private readonly IMapper _mapper;

        public OrdersController(IHttpContextAccessor httpContextAccessor,
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
        public async Task<IActionResult> GetOrders()
        {
            // 1. get user from context
            var userId = _httpContextAccessor.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)
                .Value;

            // 2. get user orders
            var orders = await _touristRouteRepository.GetOrdersByUserIdAsync(userId);
            if (orders == null)
            {
                return NotFound("no orders found");
            }

            return Ok(
                _mapper.Map<IEnumerable<OrderDto>>(orders)
                );
        }

        [HttpGet("{OrderId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetOrderById(
            [FromRoute] Guid orderId
            )
        {
            // 1. get user from context
            var userId = _httpContextAccessor.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)
                .Value;

            // 2. get order
            var orderRepo = await _touristRouteRepository.GetOrderByIdAsync(orderId);
            if (orderRepo == null)
            {
                return NotFound("Order not found");
            }

            // 3. response
            return Ok(
                _mapper.Map<OrderDto>(orderRepo)
                );
        }
    }
}

