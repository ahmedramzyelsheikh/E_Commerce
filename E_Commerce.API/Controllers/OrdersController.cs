using AutoMapper;
using E_Commerce.Application.DTOs.OrderModule;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Models.OrderModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : APIBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto orderDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var address = _mapper.Map<Address>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null) return BadRequest("Problem creating order");

            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderService.GetOrdersForUserAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<OrderDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderByIdForUser(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound();

            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpGet("deliveryMethods")]
        [AllowAnonymous]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var methods = await _orderService.GetDeliveryMethodsAsync();
            return Ok(methods);
        }
    }
}
