using Application.DTOs.Carts;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class CartService(
        IAsyncRepository<Cart, long> cartRepo,
        IAsyncRepository<CartItem, long> cartItemRepo,
        IMapper mapper,
        ILogger<CartService> logger,
        IProductService productService)
        : ICartService
    {
        public async Task<CartDto> GetOrCreateCartAsync(long userId)
        {
            var cart = await cartRepo.GetSingleAsync(c => c.UserId == userId && !c.IsDeleted);
            if (cart == null)
            {
                cart = new Cart {UserId = userId, IsActive = true};
                await cartRepo.AddEntity(cart);
                await cartRepo.SaveChangesAsync();
            }

            return await LoadCartDto(cart.Id);
        }

        public async Task<CartDto> AddToCartAsync(long userId, AddToCartDto dto)
        {
            var product = await productService.GetByIdAsync(dto.ProductId);
            if (product.StockQuantity < dto.Quantity)
                throw new BadRequestException("موجودی کافی نیست.");

            var cart = await GetOrCreateCartAsync(userId);
            var existingItem = await cartItemRepo.GetSingleAsync(ci =>
                ci.CartId == cart.Id && ci.ProductId == dto.ProductId && !ci.IsDeleted);
            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
                await cartItemRepo.UpdateEntity(existingItem);
            }
            else
            {
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    UnitPrice = product.Price,
                };
                await cartItemRepo.AddEntity(newItem);
            }

            await cartItemRepo.SaveChangesAsync();
            return await LoadCartDto(cart.Id);
        }

        public async Task<CartDto> UpdateCartItemAsync(long userId, UpdateCartItemDto dto)
        {
            var item = await cartItemRepo.GetSingleAsync(ci => ci.Id == dto.CartItemId && !ci.IsDeleted);
            if (item == null) throw new NotFoundException("آیتم یافت نشد.");
            var cart = await cartRepo.GetSingleAsync(c => c.Id == item.CartId && c.UserId == userId && !c.IsDeleted);
            if (cart == null) throw new UnauthorizedAccessException("دسترسی ندارید.");

            if (dto.Quantity <= 0)
            {
                await RemoveCartItemAsync(userId, item.Id);
            }
            else
            {
                item.Quantity = dto.Quantity;
                await cartItemRepo.UpdateEntity(item);
                await cartItemRepo.SaveChangesAsync();
            }

            return await LoadCartDto(cart.Id);
        }

        public async Task RemoveCartItemAsync(long userId, long cartItemId)
        {
            var item = await cartItemRepo.GetSingleAsync(ci => ci.Id == cartItemId && !ci.IsDeleted);
            if (item == null) return;
            var cart = await cartRepo.GetSingleAsync(c => c.Id == item.CartId && c.UserId == userId && !c.IsDeleted);
            if (cart == null) throw new UnauthorizedAccessException("دسترسی ندارید.");
            item.IsDeleted = true;
            await cartItemRepo.UpdateEntity(item);
            await cartItemRepo.SaveChangesAsync();
        }

        public async Task ClearCartAsync(long userId)
        {
            var cart = await cartRepo.GetSingleAsync(c => c.UserId == userId && !c.IsDeleted);
            if (cart == null) return;
            var items = await cartItemRepo.GetAsync(ci => ci.CartId == cart.Id && !ci.IsDeleted);
            foreach (var item in items)
                item.IsDeleted = true;
            await cartItemRepo.SaveChangesAsync();
        }

        public async Task<int> GetCartItemCountAsync(long userId)
        {
            var cart = await cartRepo.GetSingleAsync(c => c.UserId == userId && !c.IsDeleted);
            if (cart == null) return 0;
            var count = await cartItemRepo.CountAsync(ci => ci.CartId == cart.Id && !ci.IsDeleted);
            return (int) count;
        }

        private async Task<CartDto> LoadCartDto(long cartId)
        {
            var query = cartRepo.QueryWithIncludes(asNoTracking: true, includes: x => x.Items);
            var cart =  query.SingleOrDefault(c => c.Id == cartId && !c.IsDeleted);
            if (cart == null) throw new NotFoundException("سبد خرید یافت نشد.");

            var dto = mapper.Map<CartDto>(cart);
            foreach (var item in dto.Items)
            {
                var product = await productService.GetByIdAsync(item.ProductId);
                item.ProductName = product.Name;
                item.UnitPrice =
                    product.EffectivePrice; 
            }

            return dto;
        }
    }
}