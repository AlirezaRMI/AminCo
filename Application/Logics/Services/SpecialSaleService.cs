using Application.DTOs.SpecialSales;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class SpecialSaleService(
        IAsyncRepository<SpecialSale, long> repo,
        IMapper mapper,
        ILogger<SpecialSaleService> logger,
        IProductService productService)
        : ISpecialSaleService
    {

        public async Task<SpecialSaleDto> CreateAsync(CreateSpecialSaleDto dto)
        {
            await productService.GetByIdAsync(dto.ProductId);
            var overlap = await repo.AnyAsync(s =>
                s.ProductId == dto.ProductId && s.StartDate <= dto.EndDate && s.EndDate >= dto.StartDate &&
                !s.IsDeleted);
            if (overlap) throw new BadRequestException("فروش ویژه همپوشانی دارد.");
            var entity = mapper.Map<SpecialSale>(dto);
            await repo.AddEntity(entity);
            await repo.SaveChangesAsync();
            return mapper.Map<SpecialSaleDto>(entity);
        }

        public async Task<SpecialSaleDto> UpdateAsync(UpdateSpecialSaleDto dto)
        {
            var entity = await repo.GetByIdAsync(dto.Id);
            if (entity == null) throw new NotFoundException("فروش ویژه یافت نشد.");
            mapper.Map(dto, entity);
            await repo.UpdateEntity(entity);
            await repo.SaveChangesAsync();
            return mapper.Map<SpecialSaleDto>(entity);
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException("فروش ویژه یافت نشد.");
            entity.IsDeleted = true;
            await repo.UpdateEntity(entity);
            await repo.SaveChangesAsync();
        }

        public async Task<SpecialSaleDto> GetByIdAsync(long id)
        {
            var entity = await repo.GetSingleAsync(s => s.Id == id && !s.IsDeleted);
            if (entity == null) throw new NotFoundException("فروش ویژه یافت نشد.");
            return mapper.Map<SpecialSaleDto>(entity);
        }

        public async Task<IReadOnlyList<SpecialSaleDto>> GetAllAsync()
        {
            var entities = await repo.GetAsync(predicate: s => !s.IsDeleted,
                orderBy: q => q.OrderByDescending(x => x.StartDate), includeString: null);
            return mapper.Map<IReadOnlyList<SpecialSaleDto>>(entities);
        }

        public async Task<IReadOnlyList<SpecialSaleDto>> GetActiveSalesAsync()
        {
            var now = DateTime.UtcNow;
            var entities = await repo.GetAsync(predicate: s => !s.IsDeleted && s.StartDate <= now && s.EndDate >= now,
                orderBy: q => q.OrderBy(x => x.EndDate), includeString: null);
            return mapper.Map<IReadOnlyList<SpecialSaleDto>>(entities);
        }

        public async Task<IReadOnlyList<SpecialSaleDto>> GetByProductIdAsync(long productId)
        {
            var entities = await repo.GetAsync(predicate: s => s.ProductId == productId && !s.IsDeleted,
                orderBy: q => q.OrderByDescending(x => x.StartDate), includeString: null);
            return mapper.Map<IReadOnlyList<SpecialSaleDto>>(entities);
        }

        public async Task<SpecialSaleDto?> GetActiveSaleForProductAsync(long productId)
        {
            var now = DateTime.UtcNow;
            var entity = await repo.GetSingleAsync(s =>
                s.ProductId == productId && !s.IsDeleted && s.StartDate <= now && s.EndDate >= now);
            return entity == null ? null : mapper.Map<SpecialSaleDto>(entity);
        }
    }
}