using System.Collections.Generic;
using System.Threading.Tasks;
using CVBanken.Data.Models.Auth;
using CVBanken.Data.Models.Requests.Auth;
using Microsoft.AspNetCore.Http;

namespace CVBanken.Services.UserServices
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> AdminGetAll();
        Task<User> GetById(int id);
        Task<User> GetByUrl(string url);
        Task Create(RegisterRequest user);
        Task<bool> Update(int id, UpdateUserRequest userParam);
        Task<bool> Delete(int id);
        Task<bool> ConfirmPassword(int id, string password);
        Task UpdatePicture(int id, IFormFile picture);
        Task<IEnumerable<User>> GetAllUsersInProgramme(int id);
        Task<IEnumerable<User>> GetAllUserInCategory(int category);

        byte[] GetKey();
    }
}