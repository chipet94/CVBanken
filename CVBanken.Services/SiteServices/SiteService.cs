using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CVBanken.Data.Models.Site;
using CVBanken.Data.Settings;

namespace CVBanken.Services.SiteServices
{
    public class SiteService : ISiteService
    {
        // System.IO.StreamReader file = new System.IO.StreamReader(  
        //     SiteSettings.BASE_PATH +"/Settings.xml");  
        // private XmlSerializer Reader = new XmlSerializer(typeof(HomeInfo));
        public SiteService()
        {
        }
        
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