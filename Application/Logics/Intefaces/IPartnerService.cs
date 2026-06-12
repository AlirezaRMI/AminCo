using Application.DTOs.Partner;

namespace Application.Logics.Intefaces
{
    public interface IPartnerService
    {
        Task<IReadOnlyList<PartnerDto>> GetAllActiveAsync();
    }
}