using System.Linq;
using System.Threading.Tasks;
using CVBanken.Data.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVBanken.Web.Controllers
{
    public class ValidationController : ControllerBase
    {
        private readonly Context _context;
        public ValidationController(Context context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<bool> ProgrammeIdIsValid(int id)
        {
            return await _context.Programmes.AnyAsync(i => i.Id == id);
        }
    }
}