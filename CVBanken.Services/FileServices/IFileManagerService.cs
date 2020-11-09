using System.Collections.Generic;
using System.Threading.Tasks;
using CVBanken.Data.Models;
using Microsoft.AspNetCore.Http;

namespace CVBanken.Services.FileServices
{
    public interface IFileManagerService
    {
        Task<UserFile> Upload(int userId, IFormFile formFile);
        Task UploadCv(int userID, IFormFile formFile);
        Task<UserFile> GetFile(int id);
        Task<UserCv> GetCv(int id);
        Task<UserCv> GetStudentCv(int studentId);
        Task<IEnumerable<UserFile>> GetAll();
        Task RemoveFile(int id);
        Task RemoveCv(int userId);
        Task<bool> ValidateUploadRequest(IFormFile[] files, int userId);
        bool ValidateFile(IFormFile file);
    }
}