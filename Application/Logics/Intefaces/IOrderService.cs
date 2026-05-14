using Application.DTOs.Orders;

namespace Application.Logics.Intefaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderFromCartAsync(long userId, CreateOrderDto dto);
        Task<OrderDto> GetByIdAsync(long id);
        Task<IReadOnlyList<OrderDto>> GetUserOrdersAsync(long userId);
        Task<OrderDto> UpdateOrderStatusAsync(UpdateOrderStatusDto dto);
        Task CancelOrderAsync(long orderId);
        
        Task<List<OrderDto>> GetAllAsync();
    }
}