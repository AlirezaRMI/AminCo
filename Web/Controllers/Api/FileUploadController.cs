using Application.Logics.Intefaces;
using Domain.Common;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] 
    public class FileUploadController(IFileUploadService fileUploadService) : ControllerBase
    {
        [HttpPost("product")]
        public async Task<ApiResult<string>> UploadProductImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new BadRequestException("فایل انتخاب نشده است.");

            var url = await fileUploadService.UploadFileAsync(file, "products");
            return url;
        }
        
        [HttpPost("article")]
        public async Task<ApiResult<string>> UploadArticleImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new BadRequestException("فایل انتخاب نشده است.");

            var url = await fileUploadService.UploadFileAsync(file, "articles");
            return url;
        }
        
        [HttpPost("portfolio-gallery")]
        public async Task<ApiResult<string>> UploadPortfolioGalleryImage([FromForm] IFormFile file, [FromForm] long portfolioId)
        {
            if (file == null || file.Length == 0)
                throw new BadRequestException("فایل انتخاب نشده است.");
            
            var url = await fileUploadService.UploadFileAsync(file, $"portfolio/{portfolioId}");
            return url;
        }
        
        [HttpPost("portfolio-main")]
        public async Task<ApiResult<string>> UploadPortfolioMainImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new BadRequestException("فایل انتخاب نشده است.");

            var url = await fileUploadService.UploadFileAsync(file, "portfolio-main");
            return url;
        }
        
        [HttpPost("profile-picture")]
        public async Task<ApiResult<string>> UploadProfilePicture([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new BadRequestException("فایل انتخاب نشده است.");

            var url = await fileUploadService.UploadFileAsync(file, "profiles");
            return url;
        }
        
        [HttpDelete("delete")]
        public async Task<ApiResult> DeleteFile([FromQuery] string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
                throw new BadRequestException("آدرس فایل نامعتبر است.");

            await fileUploadService.DeleteFileAsync(fileUrl);
            return new OkResult();
        }
        
        [HttpPost("info")]
        public ApiResult<object> GetFileInfo([FromForm] IFormFile file)
        {
            if (file == null)
                throw new BadRequestException("فایل انتخاب نشده است.");

            var info = new
            {
                file.FileName,
                file.Length,
                ContentType = file.ContentType,
                Extension = Path.GetExtension(file.FileName)
            };
            return new ApiResult<object>(true, ApiResultStatusCode.Success, info);
        }
    }
}