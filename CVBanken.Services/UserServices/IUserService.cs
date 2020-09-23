using System.Collections.Generic;
using System.Threading.Tasks;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Services.UserServices
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Create(User user, string password);
        Task<bool> Update(User user, string password = null);
        Task<bool> Delete(int id);

        byte[] GetKey();
    }
}