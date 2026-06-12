using Application.DTOs.AboutUs;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class AboutUsService(IAsyncRepository<AboutUs, long> repo, IMapper mapper, ILogger<AboutUsService> logger)
        : IAboutUsService
    {
        public async Task<AboutUsDto> GetAsync()
        {
            var entity = await repo.GetSingleAsync(a => !a.IsDeleted);
            if (entity == null)
            {
                logger.LogWarning("AboutUs record not found, creating default");
                entity = new AboutUs
                {
                    Title = "Amin Co – Industrial Kitchen Equipment Manufacturer in Iran",
                    Content =
                        "Amin Co has been designing and manufacturing industrial kitchen equipment in Iran since 2008...",
                    LastUpdated = DateTime.UtcNow,
                    IsActive = true
                };
                await repo.AddEntity(entity);
                await repo.SaveChangesAsync();
            }

            return mapper.Map<AboutUsDto>(entity);
        }

        public async Task<AboutUsDto> UpdateAsync(UpdateAboutUsDto dto)
        {
            var entity = await repo.GetSingleAsync(a => !a.IsDeleted);
            if (entity == null)
                throw new NotFoundException("اطلاعات درباره ما یافت نشد.");
            mapper.Map(dto, entity);
            entity.LastUpdated = DateTime.UtcNow;
            await repo.UpdateEntity(entity);
            await repo.SaveChangesAsync();
            return mapper.Map<AboutUsDto>(entity);
        }
        
    }
}