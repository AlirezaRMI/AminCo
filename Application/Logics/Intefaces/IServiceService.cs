using Application.DTOs.Service;

namespace Application.Logics.Intefaces
{
    public interface IServiceService
    {
        Task<IReadOnlyList<ServiceDto>> GetAllActiveAsync();
    }
}