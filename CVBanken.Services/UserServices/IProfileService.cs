using System.Collections.Generic;
using System.Threading.Tasks;
using CVBanken.Data.Models;
using Microsoft.AspNetCore.Http;

namespace CVBanken.Services.UserServices
{
    public interface IProfileService
    {
        Task<IEnumerable<Profile>> GetAll();
        Task<Profile> GetById(int id);
        Task<Profile> GetByUser(int userId);
        Task<Profile> GetByUrl(string url);
        Task Update(int id, UpdateProfileRequest newProfile);
        Task UpdatePicture(int id, IFormFile picture);
    }
}