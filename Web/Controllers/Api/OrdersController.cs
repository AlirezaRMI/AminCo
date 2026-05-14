using Application.DTOs.Orders;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        private long CurrentUserId => User.GetUserId();

        [HttpPost("create-from-cart")]
        public async Task<ApiResult<OrderDto>> CreateOrderFromCart(CreateOrderDto dto)
            => await orderService.CreateOrderFromCartAsync(CurrentUserId, dto);

        [HttpGet]
        public async Task<IReadOnlyList<OrderDto>> GetMyOrders()
            => await orderService.GetUserOrdersAsync(CurrentUserId);

        [HttpGet("{id}")]
        public async Task<ApiResult<OrderDto>> GetById(long id)
            => await orderService.GetByIdAsync(id);

        [Authorize(Roles = "Admin")]
        [HttpPut("status")]
        public async Task<ApiResult<OrderDto>> UpdateOrderStatus(UpdateOrderStatusDto dto)
            => await orderService.UpdateOrderStatusAsync(dto);

        [HttpPost("cancel/{orderId}")]
        public async Task<ApiResult> CancelOrder(long orderId)
        {
            await orderService.CancelOrderAsync(orderId);
            return new OkResult();
        }
    }
}