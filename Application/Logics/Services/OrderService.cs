using Application.DTOs.DiscountCodes;
using Application.DTOs.Orders;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class OrderService(
        IAsyncRepository<Order, long> orderRepo,
        IAsyncRepository<OrderItem, long> orderItemRepo,
        IMapper mapper,
        ILogger<OrderService> logger,
        ICartService cartService,
        IProductService productService,
        IDiscountCodeService discountService,
        IInvoiceService invoiceService)
        : IOrderService
    {
        public async Task<OrderDto> CreateOrderFromCartAsync(long userId, CreateOrderDto dto)
        {
            var cart = await cartService.GetOrCreateCartAsync(userId);
            if (cart.Items == null || !cart.Items.Any())
                throw new BadRequestException("سبد خرید خالی است.");

            decimal subTotal = 0;
            var orderItems = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                var product = await productService.GetByIdAsync(item.ProductId);
                if (product.StockQuantity < item.Quantity)
                    throw new BadRequestException($"موجودی محصول {product.Name} کافی نیست.");
                var unitPrice = product.EffectivePrice;
                subTotal += unitPrice * item.Quantity;
                orderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice
                });
            }

            decimal discountAmount = 0;
            DiscountCodeDto? discount = null;
            if (!string.IsNullOrEmpty(dto.DiscountCode))
            {
                discount = await discountService.ValidateCodeAsync(dto.DiscountCode, subTotal);
                if (discount != null)
                {
                    if (discount.Type == DiscountType.Percentage)
                        discountAmount = subTotal * discount.Value / 100;
                    else
                        discountAmount = discount.Value;
                    discountAmount = Math.Min(discountAmount, subTotal);
                }
            }

            decimal taxAmount = subTotal * 0.09m;
            decimal totalAmount = subTotal - discountAmount + taxAmount;

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                SubTotal = subTotal,
                DiscountAmount = discountAmount,
                TaxAmount = taxAmount,
                TotalAmount = totalAmount,
                DiscountCode = dto.DiscountCode,
                ShippingAddress = dto.ShippingAddress,
                PaymentMethod = dto.PaymentMethod,
                IsPaid = false
            };
            await orderRepo.AddEntity(order);
            await orderRepo.SaveChangesAsync();

            foreach (var item in orderItems)
            {
                item.OrderId = order.Id;
                await orderItemRepo.AddEntity(item);
            }

            await orderItemRepo.SaveChangesAsync();

            foreach (var item in cart.Items)
            {
                // فراخوانی سرویس برای کاهش موجودی
            }

            await cartService.ClearCartAsync(userId);

            if (discount != null)
                await discountService.IncrementUsageAsync(discount.Id);

            await invoiceService.CreateProformaInvoiceAsync(order.Id);

            return await GetByIdAsync(order.Id);
        }

        public async Task<OrderDto> GetByIdAsync(long id)
        {
            var query = orderRepo.QueryWithIncludes(asNoTracking: true, includes: x => x.OrderItems);
            var order = query.SingleOrDefault(o => o.Id == id && !o.IsDeleted);
            if (order == null) throw new NotFoundException("سفارش یافت نشد.");
            var dto = mapper.Map<OrderDto>(order);
            foreach (var item in dto.OrderItems)
            {
                var product = await productService.GetByIdAsync(item.ProductId);
                item.ProductName = product.Name;
            }

            return dto;
        }

        public async Task<IReadOnlyList<OrderDto>> GetUserOrdersAsync(long userId)
        {
            var query = orderRepo.QueryWithIncludes(asNoTracking: true, includes: x => x.OrderItems);
            var orders = query.Where(o => o.UserId == userId && !o.IsDeleted)
                .OrderByDescending(o => o.OrderDate).ToList();
            var dtos = mapper.Map<IReadOnlyList<OrderDto>>(orders);
            foreach (var dto in dtos)
            {
                foreach (var item in dto.OrderItems)
                {
                    var product = await productService.GetByIdAsync(item.ProductId);
                    item.ProductName = product.Name;
                }
            }

            return dtos;
        }

        public async Task<OrderDto> UpdateOrderStatusAsync(UpdateOrderStatusDto dto)
        {
            var order = await orderRepo.GetByIdAsync(dto.OrderId);
            if (order == null) throw new NotFoundException("سفارش یافت نشد.");
            order.Status = dto.Status;
            await orderRepo.UpdateEntity(order);
            await orderRepo.SaveChangesAsync();
            return await GetByIdAsync(order.Id);
        }

        public async Task CancelOrderAsync(long orderId)
        {
            var order = await orderRepo.GetByIdAsync(orderId);
            if (order == null) throw new NotFoundException("سفارش یافت نشد.");
            if (order.Status != OrderStatus.Pending && order.Status != OrderStatus.Processing)
                throw new BadRequestException("قابل لغو نیست.");
            order.Status = OrderStatus.Cancelled;
            await orderRepo.UpdateEntity(order);
            await orderRepo.SaveChangesAsync();
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            logger.LogInformation("getting all orders.");
            var orders = await orderRepo.GetAllAsync();
            return mapper.Map<List<OrderDto>>(orders);
        }
    }
}