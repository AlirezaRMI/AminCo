using Application.DTOs.Carts;

namespace Application.Logics.Intefaces
{
    public interface ICartService
    {
        Task<CartDto> GetOrCreateCartAsync(long userId);
        Task<CartDto> AddToCartAsync(long userId, AddToCartDto dto);
        Task<CartDto> UpdateCartItemAsync(long userId, UpdateCartItemDto dto);
        Task RemoveCartItemAsync(long userId, long cartItemId);
        Task ClearCartAsync(long userId);
        Task<int> GetCartItemCountAsync(long userId);
    }
}