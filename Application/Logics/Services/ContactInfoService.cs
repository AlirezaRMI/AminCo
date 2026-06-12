using Application.DTOs.ContactInfo;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class ContactInfoService(
        IAsyncRepository<ContactInfo, long> repo,
        IMapper mapper,
        ILogger<ContactInfoService> logger)
        : IContactInfoService
    {
        public async Task<ContactInfoDto> GetAsync()
        {
            var entity = await repo.GetSingleAsync(c => !c.IsDeleted);
            if (entity == null)
            {
                logger.LogWarning("ContactInfo record not found, creating default");
                entity = new ContactInfo
                {
                    Phone = "+98 21 88521436",
                    Email = "info@amin-co.ir",
                    Address = "No. 189, Valiasr St., Above Valiasr Square, Tehran, Iran",
                    WorkingHours = "Saturday to Wednesday 9:00–17:00, Thursday 9:00–13:00",
                    IsActive = true
                };
                await repo.AddEntity(entity);
                await repo.SaveChangesAsync();
            }
            return mapper.Map<ContactInfoDto>(entity);
        }

        public async Task<ContactInfoDto> UpdateAsync(UpdateContactInfoDto dto)
        {
            var entity = await repo.GetSingleAsync(c => !c.IsDeleted);
            if (entity == null)
                throw new NotFoundException("اطلاعات تماس یافت نشد.");
            mapper.Map(dto, entity);
            await repo.UpdateEntity(entity);
            await repo.SaveChangesAsync();
            return mapper.Map<ContactInfoDto>(entity);
        }
    }
}