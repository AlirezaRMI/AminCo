using Application.DTOs.Products;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class ProductService(
        IAsyncRepository<Product, long> productRepo,
        IAsyncRepository<SpecialSale, long> specialSaleRepo,
        IMapper mapper,
        ILogger<ProductService> logger,
        ICategoryService categoryService)
        : IProductService
    {

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var cat = await categoryService.GetByIdAsync(dto.CategoryId);
            var entity = mapper.Map<Product>(dto);
            entity.IsActive = true;
            await productRepo.AddEntity(entity);
            await productRepo.SaveChangesAsync();
            return await GetByIdAsync(entity.Id);
        }

        public async Task<ProductDto> UpdateAsync(UpdateProductDto dto)
        {
            var existing = await productRepo.GetByIdAsync(dto.Id)
                ?? throw new NotFoundException($"Product {dto.Id} not found");
            mapper.Map(dto, existing);
            await productRepo.UpdateEntity(existing);
            await productRepo.SaveChangesAsync();
            return await GetByIdAsync(existing.Id);
        }

        public async Task DeleteAsync(long id)
        {
            var product = await productRepo.GetByIdAsync(id)
                ?? throw new NotFoundException($"Product {id} not found");
            product.IsDeleted = true;
            product.IsActive = false;
            await productRepo.UpdateEntity(product);
            await productRepo.SaveChangesAsync();
        }

        public async Task<ProductDto> GetByIdAsync(long id)
        {
            var entity = await productRepo.GetSingleAsync(p => p.Id == id && !p.IsDeleted, 
                includes: [x => x.Category, x => x.SpecialSales]);
            if (entity == null) throw new NotFoundException($"Product {id} not found");
            var dto = mapper.Map<ProductDto>(entity);
            dto.EffectivePrice = await GetEffectivePriceAsync(id);
            return dto;
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllAsync()
        {
            var entities = await productRepo.GetAsync(p => !p.IsDeleted, 
                includes: [x => x.Category, x => x.SpecialSales]);
            var dtos = mapper.Map<IReadOnlyList<ProductDto>>(entities);
            foreach (var dto in dtos)
                dto.EffectivePrice = await GetEffectivePriceAsync(dto.Id);
            return dtos;
        }

        public async Task<IReadOnlyList<ProductDto>> GetByCategoryIdAsync(long categoryId)
        {
            var entities = await productRepo.GetAsync(p => p.CategoryId == categoryId && !p.IsDeleted,
                includes: [x => x.Category, x => x.SpecialSales]);
            var dtos = mapper.Map<IReadOnlyList<ProductDto>>(entities);
            foreach (var dto in dtos)
                dto.EffectivePrice = await GetEffectivePriceAsync(dto.Id);
            return dtos;
        }

        public async Task<IReadOnlyList<ProductDto>> GetActiveProductsAsync()
        {
            var entities = await productRepo.GetAsync(p => !p.IsDeleted && p.IsActive && p.StockQuantity > 0,
                includes: [x => x.Category, x => x.SpecialSales]);
            var dtos = mapper.Map<IReadOnlyList<ProductDto>>(entities);
            foreach (var dto in dtos)
                dto.EffectivePrice = await GetEffectivePriceAsync(dto.Id);
            return dtos;
        }

        public async Task<IReadOnlyList<ProductDto>> SearchAsync(string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();
            var entities = await productRepo.GetAsync(p => !p.IsDeleted && p.Name.Contains(searchTerm),
                includes: [x => x.Category, x => x.SpecialSales]);
            var dtos = mapper.Map<IReadOnlyList<ProductDto>>(entities);
            foreach (var dto in dtos)
                dto.EffectivePrice = await GetEffectivePriceAsync(dto.Id);
            return dtos;
        }

        public async Task<bool> CheckStockAsync(long productId, int quantity)
        {
            var product = await productRepo.GetByIdAsync(productId);
            return product != null && product.StockQuantity >= quantity;
        }

        public async Task<decimal> GetEffectivePriceAsync(long productId)
        {
            var activeSale = await specialSaleRepo.GetSingleAsync(ss => ss.ProductId == productId && ss.IsActive);
            if (activeSale != null)
                return activeSale.SalePrice;

            var product = await productRepo.GetByIdAsync(productId);
            return product?.DiscountPrice ?? product?.Price ?? 0;
        }
    }
}