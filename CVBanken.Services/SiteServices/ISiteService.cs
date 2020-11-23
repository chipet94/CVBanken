using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CVBanken.Data.Models.Site;

namespace CVBanken.Services.SiteServices
{
    public interface ISiteService
    {
        HomeInfo GetHome();

        Task<SiteMessage> GetMessage(int id);
        Task RemoveMessage(int id);
        Task AddMessage(SiteMessage message);
        Task EditMessage(int id, SiteMessage message);
        Task<IEnumerable<SiteMessage>> GetMessages();
        Task SetHome();
    }
}