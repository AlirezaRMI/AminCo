using Application.DTOs.Service;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class ServiceService(
        IAsyncRepository<Service, long> repo,
        IMapper mapper,
        ILogger<ServiceService> logger)
        : IServiceService
    {
        public async Task<IReadOnlyList<ServiceDto>> GetAllActiveAsync()
        {
            var entities = await repo.GetAsync(
                predicate: s => !s.IsDeleted && s.IsActive,
                orderBy: q => q.OrderBy(s => s.DisplayOrder),
                includeString: null);
            return mapper.Map<IReadOnlyList<ServiceDto>>(entities);
        }
    }
}