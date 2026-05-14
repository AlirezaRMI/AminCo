using Application.DTOs.CustomDesignRequests;

namespace Application.Logics.Intefaces
{
    public interface ICustomDesignRequestService
    {
        Task<CustomDesignRequestDto> CreateAsync(CreateCustomDesignRequestDto dto);
        Task<CustomDesignRequestDto> UpdateStatusAsync(UpdateCustomDesignRequestDto dto);
        Task DeleteAsync(long id);
        Task<CustomDesignRequestDto> GetByIdAsync(long id);
        Task<IReadOnlyList<CustomDesignRequestDto>> GetAllAsync(bool onlyPending = false);
        Task<IReadOnlyList<CustomDesignRequestDto>> GetByUserEmailAsync(string email);
    }
}