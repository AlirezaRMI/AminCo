using Application.DTOs.Carts;
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
    public class CartController(ICartService cartService) : ControllerBase
    {
        private long CurrentUserId => User.GetUserId();

        [HttpGet]
        public async Task<ApiResult<CartDto>> GetCart()
            => await cartService.GetOrCreateCartAsync(CurrentUserId);

        [HttpPost("add")]
        public async Task<ApiResult<CartDto>> AddToCart(AddToCartDto dto)
            => await cartService.AddToCartAsync(CurrentUserId, dto);

        [HttpPut("update-item")]
        public async Task<ApiResult<CartDto>> UpdateCartItem(UpdateCartItemDto dto)
            => await cartService.UpdateCartItemAsync(CurrentUserId, dto);

        [HttpDelete("remove-item/{cartItemId}")]
        public async Task<ApiResult> RemoveCartItem(long cartItemId)
        {
            await cartService.RemoveCartItemAsync(CurrentUserId, cartItemId);
            return new OkResult();
        }

        [HttpDelete("clear")]
        public async Task<ApiResult> ClearCart()
        {
            await cartService.ClearCartAsync(CurrentUserId);
            return new OkResult();
        }

        [HttpGet("count")]
        public async Task<int> GetItemCount()
            => await cartService.GetCartItemCountAsync(CurrentUserId);
    }
}