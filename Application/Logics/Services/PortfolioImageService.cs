using Application.DTOs.PortfolioImages;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class PortfolioImageService(
        IAsyncRepository<PortfolioImage, long> imageRepo,
        IServiceScopeFactory scopeFactory,
        IMapper mapper,
        ILogger<PortfolioImageService> logger)
        : IPortfolioImageService
    {

        private async Task<Portfolio> GetPortfolioByIdAsync(long portfolioId)
        {
            using var scope = scopeFactory.CreateScope();
            var portfolioRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Portfolio, long>>();
            var portfolio = await portfolioRepo.GetByIdAsync(portfolioId);
            if (portfolio == null || portfolio.IsDeleted)
                throw new NotFoundException("پروژه یافت نشد.");
            return portfolio;
        }

        public async Task<PortfolioImageDto> CreateAsync(CreatePortfolioImageDto dto)
        {
            await GetPortfolioByIdAsync(dto.PortfolioId);

            var entity = mapper.Map<PortfolioImage>(dto);
            await imageRepo.AddEntity(entity);
            await imageRepo.SaveChangesAsync();

            if (dto.IsMain)
                await SetMainImageAsync(dto.PortfolioId, entity.Id);

            return mapper.Map<PortfolioImageDto>(entity);
        }

        public async Task<PortfolioImageDto> UpdateAsync(UpdatePortfolioImageDto dto)
        {
            var entity = await imageRepo.GetByIdAsync(dto.Id);
            if (entity == null)
                throw new NotFoundException("تصویر یافت نشد.");

            mapper.Map(dto, entity);
            await imageRepo.UpdateEntity(entity);
            await imageRepo.SaveChangesAsync();

            if (dto.IsMain)
                await SetMainImageAsync(entity.PortfolioId, entity.Id);

            return mapper.Map<PortfolioImageDto>(entity);
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await imageRepo.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException("تصویر یافت نشد.");

            entity.IsDeleted = true;
            await imageRepo.UpdateEntity(entity);
            await imageRepo.SaveChangesAsync();
        }

        public async Task<PortfolioImageDto> GetByIdAsync(long id)
        {
            var entity = await imageRepo.GetSingleAsync(i => i.Id == id && !i.IsDeleted);
            if (entity == null)
                throw new NotFoundException("تصویر یافت نشد.");
            return mapper.Map<PortfolioImageDto>(entity);
        }

        public async Task<IReadOnlyList<PortfolioImageDto>> GetByPortfolioIdAsync(long portfolioId)
        {
            var entities = await imageRepo.GetAsync(
                predicate: i => i.PortfolioId == portfolioId && !i.IsDeleted,
                orderBy: q => q.OrderBy(x => x.DisplayOrder),
                includeString: null);
            return mapper.Map<IReadOnlyList<PortfolioImageDto>>(entities);
        }

        public async Task SetMainImageAsync(long portfolioId, long imageId)
        {
            var images = await imageRepo.GetAsync(i => i.PortfolioId == portfolioId && !i.IsDeleted);
            foreach (var img in images)
            {
                img.IsMain = (img.Id == imageId);
                await imageRepo.UpdateEntity(img);
            }
            await imageRepo.SaveChangesAsync();
        }
    }
}