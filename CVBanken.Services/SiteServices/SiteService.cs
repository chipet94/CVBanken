using System.Threading.Tasks;
using CVBanken.Data.Models.Site;

namespace CVBanken.Services.SiteServices
{
    public class SiteService : ISiteService
    {
        // System.IO.StreamReader file = new System.IO.StreamReader(  
        //     SiteSettings.BASE_PATH +"/Settings.xml");  
        // private XmlSerializer Reader = new XmlSerializer(typeof(HomeInfo));

        public async Task<HomeInfo> GetHome()
        {
            //return (HomeInfo)Reader.Deserialize(file);
            return HomeInfoBuilder.Default();
        }

        public async Task SetHome()
        {
        }
    }
}