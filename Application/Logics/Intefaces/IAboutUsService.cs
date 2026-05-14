using Application.DTOs.AboutUs;

namespace Application.Logics.Intefaces
{
    public interface IAboutUsService
    {
        Task<AboutUsDto> GetAsync();
        Task<AboutUsDto> UpdateAsync(UpdateAboutUsDto dto);
    }
}