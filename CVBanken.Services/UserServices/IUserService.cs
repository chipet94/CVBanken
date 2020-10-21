using System.Collections.Generic;
using System.Threading.Tasks;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Services.UserServices
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> AdminGetAll();
        Task<User> GetById(int id);
        Task Create(User user);
        Task<bool> Update(User user, string password = null);
        Task<bool> Delete(int id);
        Task<bool> ConfirmPassword(int id, string password);

        byte[] GetKey();
    }
}