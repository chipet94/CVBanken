using System.Threading.Tasks;
using CVBanken.Data.Models.Site;
using CVBanken.Services.SiteServices;
using Microsoft.AspNetCore.Mvc;

namespace CVBanken.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteController : ControllerBase
    {
        private readonly ISiteService _context;

        public SiteController(ISiteService context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<HomeInfo> GetHome()
        {
            return await _context.GetHome();
        }
    }
}