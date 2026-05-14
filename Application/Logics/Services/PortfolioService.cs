using Application.DTOs.Portfolios;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class PortfolioService(
        IAsyncRepository<Portfolio, long> repo,
        IMapper mapper,
        ILogger<PortfolioService> logger,
        IPortfolioImageService imageService)
        : IPortfolioService
    {
        public async Task<PortfolioDto> CreateAsync(CreatePortfolioDto dto)
        {
            var entity = mapper.Map<Portfolio>(dto);
            entity.IsActive = true;
            await repo.AddEntity(entity);
            await repo.SaveChangesAsync();
            return mapper.Map<PortfolioDto>(entity);
        }

        public async Task<PortfolioDto> UpdateAsync(UpdatePortfolioDto dto)
        {
            var entity = await repo.GetByIdAsync(dto.Id);
            if (entity == null) throw new NotFoundException("پروژه یافت نشد.");
            mapper.Map(dto, entity);
            await repo.UpdateEntity(entity);
            await repo.SaveChangesAsync();
            return mapper.Map<PortfolioDto>(entity);
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException("پروژه یافت نشد.");
            entity.IsDeleted = true;
            entity.IsActive = false;
            await repo.UpdateEntity(entity);
            await repo.SaveChangesAsync();
        }

        public async Task<PortfolioDto> GetByIdAsync(long id)
        {
            var query = repo.QueryWithIncludes(asNoTracking: true, includes: x => x.Images);
            var entity = query.SingleOrDefault(p => p.Id == id && !p.IsDeleted);
            if (entity == null) throw new NotFoundException("پروژه یافت نشد.");
            return mapper.Map<PortfolioDto>(entity);
        }

        public async Task<IReadOnlyList<PortfolioDto>> GetAllAsync()
        {
            var query = repo.QueryWithIncludes(asNoTracking: true, includes: x => x.Images);
            var entities = query.Where(p => !p.IsDeleted).OrderBy(p => p.DisplayOrder).ToList();
            return mapper.Map<IReadOnlyList<PortfolioDto>>(entities);
        }

        public async Task<IReadOnlyList<PortfolioDto>> GetActivePortfoliosAsync()
        {
            var query = repo.QueryWithIncludes(asNoTracking: true, includes: x => x.Images);
            var entities = query.Where(p => !p.IsDeleted && p.IsActive).OrderBy(p => p.DisplayOrder).ToList();
            return mapper.Map<IReadOnlyList<PortfolioDto>>(entities);
        }

        public async Task<IReadOnlyList<PortfolioDto>> GetByCategoryAsync(PortfolioCategory category)
        {
            var query = repo.QueryWithIncludes(asNoTracking: true, includes: x => x.Images);
            var entities = query.Where(p => !p.IsDeleted && p.Category == category).OrderBy(p => p.DisplayOrder)
                .ToList();
            return mapper.Map<IReadOnlyList<PortfolioDto>>(entities);
        }
    }
}