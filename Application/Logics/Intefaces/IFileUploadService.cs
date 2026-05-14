using Microsoft.AspNetCore.Http;

namespace Application.Logics.Intefaces
{
    public interface IFileUploadService
    {
        
        Task<string> UploadFileAsync(IFormFile file, string folderName, string? existingFilePath = null);
        
        Task DeleteFileAsync(string filePath);
        
        bool IsValidFile(IFormFile file, out string errorMessage);
    }
}