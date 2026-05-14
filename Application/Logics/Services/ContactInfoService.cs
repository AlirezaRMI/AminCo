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
            var entity = await repo.GetSingleAsync(c => c.Id == 1 && !c.IsDeleted);
            if (entity == null)
            {
                logger.LogWarning("ContactInfo record not found, creating default");
                entity = new ContactInfo { Id = 1, Phone = "", Email = "", Address = "" };
                await repo.AddEntity(entity);
                await repo.SaveChangesAsync();
            }
            return mapper.Map<ContactInfoDto>(entity);
        }

        public async Task<ContactInfoDto> UpdateAsync(UpdateContactInfoDto dto)
        {
            var entity = await repo.GetSingleAsync(c => c.Id == 1);
            if (entity == null)
                throw new NotFoundException("اطلاعات تماس یافت نشد.");
            mapper.Map(dto, entity);
            await repo.UpdateEntity(entity);
            await repo.SaveChangesAsync();
            return mapper.Map<ContactInfoDto>(entity);
        }
    }
}