using Application.DTOs.Partner;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class PartnerService(
        IAsyncRepository<Partner, long> repo,
        IMapper mapper,
        ILogger<PartnerService> logger)
        : IPartnerService
    {
        public async Task<IReadOnlyList<PartnerDto>> GetAllActiveAsync()
        {
            var entities = await repo.GetAsync(
                predicate: p => !p.IsDeleted && p.IsActive,
                orderBy: q => q.OrderBy(p => p.DisplayOrder),
                includeString: null);
            return mapper.Map<IReadOnlyList<PartnerDto>>(entities);
        }
    }
}