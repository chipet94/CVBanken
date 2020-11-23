using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVBanken.Data.Models.Database;
using CVBanken.Data.Models.Site;
using Microsoft.EntityFrameworkCore;

namespace CVBanken.Services.SiteServices
{
    public class SiteService : ISiteService
    {
        // System.IO.StreamReader file = new System.IO.StreamReader(  
        //     SiteSettings.BASE_PATH +"/Settings.xml");  
        // private XmlSerializer Reader = new XmlSerializer(typeof(HomeInfo));
        private readonly Context _context;
        public SiteService(Context context)
        {
            _context = context;
        }
        public HomeInfo GetHome()
        {
            //return (HomeInfo)Reader.Deserialize(file);
            return HomeInfoBuilder.Default();
        }

        public async Task<SiteMessage> GetMessage(int id)
        {
            return await _context.SiteMessages.FindAsync(id);
        }

        public async Task RemoveMessage(int id)
        {
            var message = await _context.SiteMessages.FindAsync(id);
            if (message == null)
            {
                throw new Exception("Site Message not found.");
            }

            _context.Remove(message);
            await _context.SaveChangesAsync();
        }

        public async Task AddMessage(SiteMessage message)
        {
            _context.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task EditMessage(int id, SiteMessage message)
        {
            var oldMessage = await _context.SiteMessages.FindAsync(id);
            if (oldMessage == null)
            {
                throw new Exception("Site Message not found.");
            }

            oldMessage.Text = message.Text;
            _context.Update(oldMessage);
            await _context.SaveChangesAsync();        }

        public async Task<IEnumerable<SiteMessage>> GetMessages()
        {
            return await _context.SiteMessages.ToListAsync();
        }

        public Task SetHome()
        {
            throw new NotImplementedException();
        }
    }
}