using Application.Logics.Intefaces;

namespace Web.Services
{
    public class FileUploadService(
        IWebHostEnvironment webHostEnvironment,
        IConfiguration configuration,
        ILogger<FileUploadService> logger)
        : IFileUploadService
    {
        private readonly long _maxFileSize = configuration.GetValue<long>("FileUpload:MaxFileSize", 2 * 1024 * 1024);
        private readonly string[] _allowedExtensions = configuration.GetSection("FileUpload:AllowedExtensions").Get<string[]>() 
                                                       ?? [".jpg", ".jpeg", ".png", ".gif", ".webp"];
        private readonly string _baseUrl = configuration.GetValue<string>("FileUpload:BaseUrl", "/uploads");

        public async Task<string> UploadFileAsync(IFormFile file, string folderName, string? existingFilePath = null)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("فایل معتبر نیست.");

            if (!IsValidFile(file, out var error))
                throw new ArgumentException(error);

            if (!string.IsNullOrEmpty(existingFilePath))
                await DeleteFileAsync(existingFilePath);

            var uploadsRoot = Path.Combine(webHostEnvironment.WebRootPath, "uploads", folderName);
            if (!Directory.Exists(uploadsRoot))
                Directory.CreateDirectory(uploadsRoot);

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsRoot, uniqueFileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var relativePath = $"{_baseUrl}/{folderName}/{uniqueFileName}";
            logger.LogInformation("فایل آپلود شد: {RelativePath}", relativePath);
            return relativePath;
        }

        public Task DeleteFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return Task.CompletedTask;

            var relativePath = filePath.StartsWith(_baseUrl) 
                ? filePath[_baseUrl.Length..] 
                : filePath;
            var fullPath = Path.Combine(webHostEnvironment.WebRootPath, relativePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                    logger.LogInformation("فایل حذف شد: {FilePath}", filePath);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "خطا در حذف فایل: {FilePath}", filePath);
                }
            }
            return Task.CompletedTask;
        }

        public bool IsValidFile(IFormFile file, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (file.Length > _maxFileSize)
            {
                errorMessage = $"حجم فایل نباید بیشتر از {_maxFileSize / 1024 / 1024} مگابایت باشد.";
                return false;
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
            {
                errorMessage = $"پسوند فایل مجاز نیست. فقط {string.Join(", ", _allowedExtensions)} مجاز است.";
                return false;
            }

            return true;
        }
    }
}