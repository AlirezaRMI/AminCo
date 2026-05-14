using System.Linq.Expressions;
using Application.DTOs.CustomDesignRequests;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class CustomDesignRequestService(
        IAsyncRepository<CustomDesignRequest, long> repo,
        IMapper mapper,
        ILogger<CustomDesignRequestService> logger)
        : ICustomDesignRequestService
    {
        private readonly ILogger<CustomDesignRequestService> _logger = logger;

        public async Task<CustomDesignRequestDto> CreateAsync(CreateCustomDesignRequestDto dto)
        {
            var entity = mapper.Map<CustomDesignRequest>(dto);
            entity.Status = RequestStatus.Pending;
            await repo.AddEntity(entity);
            await repo.SaveChangesAsync();
            return mapper.Map<CustomDesignRequestDto>(entity);
        }

        public async Task<CustomDesignRequestDto> UpdateStatusAsync(UpdateCustomDesignRequestDto dto)
        {
            var entity = await repo.GetByIdAsync(dto.Id);
            if (entity == null) throw new NotFoundException("درخواست یافت نشد.");
            mapper.Map(dto, entity);
            await repo.UpdateEntity(entity);
            await repo.SaveChangesAsync();
            return mapper.Map<CustomDesignRequestDto>(entity);
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException("درخواست یافت نشد.");
            entity.IsDeleted = true;
            await repo.UpdateEntity(entity);
            await repo.SaveChangesAsync();
        }

        public async Task<CustomDesignRequestDto> GetByIdAsync(long id)
        {
            var entity = await repo.GetSingleAsync(r => r.Id == id && !r.IsDeleted);
            if (entity == null) throw new NotFoundException("درخواست یافت نشد.");
            return mapper.Map<CustomDesignRequestDto>(entity);
        }

        public async Task<IReadOnlyList<CustomDesignRequestDto>> GetAllAsync(bool onlyPending = false)
        {
            var predicate = onlyPending ? (Expression<Func<CustomDesignRequest, bool>>)(r => !r.IsDeleted && r.Status == RequestStatus.Pending) : r => !r.IsDeleted;
            var entities = await repo.GetAsync(predicate: predicate, orderBy: q => q.OrderByDescending(x => x.CreatedAt), includeString: null);
            return mapper.Map<IReadOnlyList<CustomDesignRequestDto>>(entities);
        }

        public async Task<IReadOnlyList<CustomDesignRequestDto>> GetByUserEmailAsync(string email)
        {
            var entities = await repo.GetAsync(predicate: r => r.Email == email && !r.IsDeleted, orderBy: q => q.OrderByDescending(x => x.CreatedAt), includeString: null);
            return mapper.Map<IReadOnlyList<CustomDesignRequestDto>>(entities);
        }
    }
}