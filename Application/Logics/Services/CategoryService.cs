using Application.DTOs.Categories;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class CategoryService(
        IAsyncRepository<Category, long> categoryRepo,
        IMapper mapper,
        ILogger<CategoryService> logger,
        ISectionService sectionService)
        : ICategoryService
    {
        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var section = await sectionService.GetByIdAsync(dto.SectionId);
            if (section == null)
                throw new BadRequestException("Invalid SectionId");

            var entity = mapper.Map<Category>(dto);
            entity.IsActive = true;
            await categoryRepo.AddEntity(entity);
            await categoryRepo.SaveChangesAsync();
            return mapper.Map<CategoryDto>(entity);
        }

        public async Task<CategoryDto> UpdateAsync(UpdateCategoryDto dto)
        {
            var existing = await categoryRepo.GetByIdAsync(dto.Id)
                           ?? throw new NotFoundException($"Category {dto.Id} not found");
            if (existing.SectionId != dto.SectionId)
            {
                var section = await sectionService.GetByIdAsync(dto.SectionId);
                if (section == null) throw new BadRequestException("Invalid SectionId");
            }

            mapper.Map(dto, existing);
            await categoryRepo.UpdateEntity(existing);
            await categoryRepo.SaveChangesAsync();
            return mapper.Map<CategoryDto>(existing);
        }

        public async Task DeleteAsync(long id)
        {
            var category = await categoryRepo.GetByIdAsync(id)
                           ?? throw new NotFoundException($"Category {id} not found");
            category.IsDeleted = true;
            category.IsActive = false;
            await categoryRepo.UpdateEntity(category);
            await categoryRepo.SaveChangesAsync();
        }

        public async Task<CategoryDto> GetByIdAsync(long id)
        {
            var entity = await categoryRepo.GetSingleAsync(c => c.Id == id && !c.IsDeleted, "Section");
            if (entity == null) throw new NotFoundException($"Category {id} not found");
            return mapper.Map<CategoryDto>(entity);
        }

        public async Task<IReadOnlyList<CategoryDto>> GetAllAsync()
        {
            var entities = await categoryRepo.GetAsync(c => !c.IsDeleted, includes: [x => x.Section]);
            return mapper.Map<IReadOnlyList<CategoryDto>>(entities);
        }

        public async Task<IReadOnlyList<CategoryDto>> GetBySectionIdAsync(long sectionId)
        {
            var entities = await categoryRepo.GetAsync(c => c.SectionId == sectionId && !c.IsDeleted,
                includes: [x => x.Section]);
            return mapper.Map<IReadOnlyList<CategoryDto>>(entities);
        }

        public async Task<IReadOnlyList<CategoryDto>> SearchAsync(string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();
            var entities = await categoryRepo.GetAsync(c => !c.IsDeleted && c.Name.Contains(searchTerm),
                includes: [x => x.Section]);
            return mapper.Map<IReadOnlyList<CategoryDto>>(entities);
        }
    }
}