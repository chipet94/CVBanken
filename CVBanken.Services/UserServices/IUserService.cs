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
        Task<IEnumerable<Student>> GetAll();
        Task<IEnumerable<Student>> AdminGetAll();
        Task<Student> GetById(int id);
        Task<Student> GetByUrl(string url);
        Task Create(RegisterRequest user);
        Task Update(int id, UpdateUserRequest userParam);
        Task<bool> Delete(int id);
        Task<bool> ConfirmPassword(int id, string password);
        Task UpdatePicture(int id, IFormFile picture);
        Task<IEnumerable<Student>> GetAllUsersInProgramme(int id);
        Task<IEnumerable<Student>> GetAllUserInCategory(int category);

        byte[] GetKey();
    }
}