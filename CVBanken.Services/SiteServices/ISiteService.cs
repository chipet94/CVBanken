using System.Threading.Tasks;
using CVBanken.Data.Models.Site;

namespace CVBanken.Services.SiteServices
{
    public interface ISiteService
    {
        Task<HomeInfo> GetHome();
        Task SetHome();
    }
}