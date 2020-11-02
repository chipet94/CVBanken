using System.Collections.Generic;
using System.Threading.Tasks;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Services.EducationServices
{
    public interface IEducationService
    {
        Task<IEnumerable<Programme>> GetAllProgrammes();
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Programme> GetProgrammeByName(string name);
        Task<Category> GetCategoryByName(string name);
        Task<Category> GetCategoryById(int id);
        Task<IEnumerable<User>> GetStudents(int id);
        Task<IEnumerable<Programme>> GetAllEducationsByCategory(int id);
        Task<Programme> GetProgrammeById(int id);

        Task Create(Programme programme);
        Task Update(int id, Programme programme);
        Task Delete(int id);
        Task CreateCategory(Category category);
        Task DeleteCategory(int id);
    }
}