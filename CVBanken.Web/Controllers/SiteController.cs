using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVBanken.Data.Models.Requests;
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
        
        [HttpGet]
        [Route("messages")]
        public async Task<IEnumerable<SiteMessage>> GetMessages()
        {
            return  await _context.GetMessages();
        }
        [HttpGet]
        [Route("messages/{id}")]
        public async Task<SiteMessage> GetMessage(int id)
        {
            return  await _context.GetMessage(id);
        }
        [HttpPost]
        [Route("messages")]
        public async Task<IActionResult> AddMessage(MessageRequest message)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(r => r.Errors.First()));
            try
            {
                await _context.AddMessage(message.ToMessage());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }
        [HttpPut]
        [Route("messages/{id}")]
        public async Task<IActionResult> PutMessage(int id,SiteMessage message)
        {
            try
            {
                await _context.EditMessage(id,message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }
        [HttpDelete]
        [Route("messages/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                await _context.RemoveMessage(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return NoContent();
        }
    }
}