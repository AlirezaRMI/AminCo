using System.Linq.Expressions;
using Application.DTOs.Articles;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class ArticleService(IAsyncRepository<Article, long> repo, IMapper mapper, ILogger<ArticleService> logger)
        : IArticleService
    {
        public async Task<ArticleDto> CreateAsync(CreateArticleDto dto)
        {
            var existing = await repo.GetSingleAsync(a => a.Slug == dto.Slug);
            if (existing != null) throw new BadRequestException("Slug تکراری است.");
            var entity = mapper.Map<Article>(dto);
            entity.PublishDate = dto.PublishDate == default ? DateTime.UtcNow : dto.PublishDate;
            entity.IsPublished = true;
            await repo.AddEntity(entity);
            await repo.SaveChangesAsync();
            return mapper.Map<ArticleDto>(entity);
        }

        public async Task<ArticleDto> UpdateAsync(UpdateArticleDto dto)
        {
            var entity = await repo.GetByIdAsync(dto.Id);
            if (entity == null) throw new NotFoundException("مقاله یافت نشد.");
            mapper.Map(dto, entity);
            await repo.UpdateEntity(entity);
            await repo.SaveChangesAsync();
            return mapper.Map<ArticleDto>(entity);
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException("مقاله یافت نشد.");
            entity.IsDeleted = true;
            entity.IsPublished = false;
            await repo.UpdateEntity(entity);
            await repo.SaveChangesAsync();
        }

        public async Task<ArticleDto> GetByIdAsync(long id)
        {
            var entity = await repo.GetSingleAsync(a => a.Id == id && !a.IsDeleted);
            if (entity == null) throw new NotFoundException("مقاله یافت نشد.");
            return mapper.Map<ArticleDto>(entity);
        }

        public async Task<ArticleDto> GetBySlugAsync(string slug)
        {
            var entity = await repo.GetSingleAsync(a => a.Slug == slug && !a.IsDeleted);
            if (entity == null) throw new NotFoundException("مقاله یافت نشد.");
            return mapper.Map<ArticleDto>(entity);
        }

        public async Task<IReadOnlyList<ArticleDto>> GetAllAsync(bool onlyPublished = true)
        {
            var predicate = onlyPublished ? (Expression<Func<Article, bool>>)(a => !a.IsDeleted && a.IsPublished) : a => !a.IsDeleted;
            var entities = await repo.GetAsync(predicate: predicate, orderBy: q => q.OrderByDescending(x => x.PublishDate), includeString: null);
            return mapper.Map<IReadOnlyList<ArticleDto>>(entities);
        }

        public async Task<IReadOnlyList<ArticleDto>> SearchAsync(string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();
            var entities = await repo.GetAsync(predicate: a => !a.IsDeleted && (a.Title.Contains(searchTerm) || a.ShortDescription.Contains(searchTerm)),
                                                orderBy: q => q.OrderByDescending(x => x.PublishDate), includeString: null);
            return mapper.Map<IReadOnlyList<ArticleDto>>(entities);
        }

        public async Task IncrementViewCountAsync(long id)
        {
            var entity = await repo.GetByIdAsync(id);
            if (entity != null)
            {
                entity.ViewCount++;
                await repo.UpdateEntity(entity);
                await repo.SaveChangesAsync();
            }
        }
    }
}