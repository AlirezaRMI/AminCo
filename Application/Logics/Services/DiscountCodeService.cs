using Application.DTOs.DiscountCodes;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class DiscountCodeService(
        IAsyncRepository<DiscountCode, long> repo,
        IMapper mapper,
        ILogger<DiscountCodeService> logger)
        : IDiscountCodeService
    {
        public async Task<DiscountCodeDto> CreateAsync(CreateDiscountCodeDto dto)
        {
            var existing = await repo.GetSingleAsync(c => c.Code == dto.Code);
            if (existing != null)
                throw new BadRequestException("کد تخفیف تکراری است.");
            var entity = mapper.Map<DiscountCode>(dto);
            entity.IsActive = true;
            await repo.AddEntity(entity);
            await repo.SaveChangesAsync();
            return mapper.Map<DiscountCodeDto>(entity);
        }

        public async Task<DiscountCodeDto> UpdateAsync(UpdateDiscountCodeDto dto)
        {
            var entity = await repo.GetByIdAsync(dto.Id);
            if (entity == null) throw new NotFoundException("کد تخفیف یافت نشد.");
            mapper.Map(dto, entity);
            await repo.UpdateEntity(entity);
            await repo.SaveChangesAsync();
            return mapper.Map<DiscountCodeDto>(entity);
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException("کد تخفیف یافت نشد.");
            entity.IsDeleted = true;
            entity.IsActive = false;
            await repo.UpdateEntity(entity);
            await repo.SaveChangesAsync();
        }

        public async Task<DiscountCodeDto> GetByIdAsync(long id)
        {
            var entity = await repo.GetSingleAsync(c => c.Id == id && !c.IsDeleted);
            if (entity == null) throw new NotFoundException("کد تخفیف یافت نشد.");
            return mapper.Map<DiscountCodeDto>(entity);
        }

        public async Task<IReadOnlyList<DiscountCodeDto>> GetAllAsync()
        {
            var entities = await repo.GetAsync(predicate: c => !c.IsDeleted,
                orderBy: q => q.OrderByDescending(x => x.CreatedAt), includeString: null);
            return mapper.Map<IReadOnlyList<DiscountCodeDto>>(entities);
        }

        public async Task<IReadOnlyList<DiscountCodeDto>> GetActiveCodesAsync()
        {
            var now = DateTime.UtcNow;
            var entities = await repo.GetAsync(
                predicate: c =>
                    !c.IsDeleted && c.IsActive && (c.StartDate == null || c.StartDate <= now) &&
                    (c.EndDate == null || c.EndDate >= now),
                orderBy: q => q.OrderByDescending(x => x.CreatedAt),
                includeString: null);
            return mapper.Map<IReadOnlyList<DiscountCodeDto>>(entities);
        }

        public async Task<DiscountCodeDto?> ValidateCodeAsync(string code, decimal? orderTotal = null)
        {
            var now = DateTime.UtcNow;
            var entity = await repo.GetSingleAsync(c => c.Code == code && !c.IsDeleted && c.IsActive &&
                                                        (c.StartDate == null || c.StartDate <= now) &&
                                                        (c.EndDate == null || c.EndDate >= now) &&
                                                        c.UsedCount < c.UsageLimit);
            if (entity == null) return null;
            return mapper.Map<DiscountCodeDto>(entity);
        }

        public async Task IncrementUsageAsync(long id)
        {
            var entity = await repo.GetByIdAsync(id);
            if (entity != null)
            {
                entity.UsedCount++;
                await repo.UpdateEntity(entity);
                await repo.SaveChangesAsync();
            }
        }
    }
}