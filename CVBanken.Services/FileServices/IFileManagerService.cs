using System.Collections.Generic;
using System.Threading.Tasks;
using CVBanken.Data.Models;
using CVBanken.Services.UserServices;
using Microsoft.AspNetCore.Http;


namespace CVBanken.Services.FileServices
{
    public interface IFileManagerService
    {
        Task<UserFile> Upload(int userId, IFormFile formFile);
        Task<UserFile> GetFile(int id);
        Task<IEnumerable<UserFile>> GetAll();
        Task RemoveFile(int id);

        Task<bool> ValidateUploadRequest(IFormFile[] files, int userId);
    }
}