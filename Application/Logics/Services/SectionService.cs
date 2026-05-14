using Application.DTOs.Sections;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class SectionService(
        IAsyncRepository<Section, long> sectionRepo,
        IMapper mapper,
        ILogger<SectionService> logger)
        : ISectionService
    {
        public async Task<SectionDto> CreateAsync(CreateSectionDto dto)
        {
            logger.LogDebug("Creating new section: {Name}", dto.Name);
            var entity = mapper.Map<Section>(dto);
            entity.IsActive = true;
            await sectionRepo.AddEntity(entity);
            await sectionRepo.SaveChangesAsync();
            logger.LogInformation("Section created with Id: {Id}", entity.Id);
            return mapper.Map<SectionDto>(entity);
        }

        public async Task<SectionDto> UpdateAsync(UpdateSectionDto dto)
        {
            logger.LogDebug("Updating section: {Id}", dto.Id);
            var existing = await sectionRepo.GetByIdAsync(dto.Id)
                           ?? throw new NotFoundException($"Section with Id {dto.Id} not found.");
            mapper.Map(dto, existing);
            await sectionRepo.UpdateEntity(existing);
            await sectionRepo.SaveChangesAsync();
            return mapper.Map<SectionDto>(existing);
        }

        public async Task DeleteAsync(long id)
        {
            logger.LogDebug("Soft-deleting section: {Id}", id);
            var entity = await sectionRepo.GetByIdAsync(id)
                         ?? throw new NotFoundException($"Section with Id {id} not found.");
            entity.IsDeleted = true;
            entity.IsActive = false;
            await sectionRepo.UpdateEntity(entity);
            await sectionRepo.SaveChangesAsync();
        }

        public async Task<SectionDto> GetByIdAsync(long id)
        {
            var entity = await sectionRepo.GetByIdAsync(id);
            if (entity == null || entity.IsDeleted)
                throw new NotFoundException($"Section with Id {id} not found.");
            return mapper.Map<SectionDto>(entity);
        }

        public async Task<IReadOnlyList<SectionDto>> GetAllAsync()
        {
            var entities = await sectionRepo.GetAsync(s => !s.IsDeleted, orderBy: q => q.OrderBy(x => x.DisplayOrder),includeString:null );
            return mapper.Map<IReadOnlyList<SectionDto>>(entities);
        }

        public async Task<IReadOnlyList<SectionDto>> GetActiveSectionsAsync()
        {
            var entities = await sectionRepo.GetAsync(s => !s.IsDeleted && s.IsActive,
                orderBy: q => q.OrderBy(x => x.DisplayOrder),includeString:null );
            return mapper.Map<IReadOnlyList<SectionDto>>(entities);
        }

        public async Task<IReadOnlyList<SectionDto>> SearchAsync(string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();
            var entities = await sectionRepo.GetAsync(s => !s.IsDeleted && s.Name.Contains(searchTerm));
            return mapper.Map<IReadOnlyList<SectionDto>>(entities);
        }
    }
}