using Application.DTOs.ContactInfo;

namespace Application.Logics.Intefaces
{
    public interface IContactInfoService
    {
        Task<ContactInfoDto> GetAsync();
        Task<ContactInfoDto> UpdateAsync(UpdateContactInfoDto dto);
    }
}