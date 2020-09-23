using System.Collections.Generic;
using System.Threading.Tasks;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Services.EducationServices
{
    public interface IEducationService
    {
        Task<IEnumerable<Programme>> GetAllProgrammes();
        Task<Programme> GetProgrammeById(int id);
        Task Create(Programme programme);
        Task Update(int id, Programme programme);
        Task Delete(int id);
    }
}