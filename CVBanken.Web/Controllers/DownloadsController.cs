using System;
using System.Threading.Tasks;
using CVBanken.Data.Models.Database;
using Microsoft.AspNetCore.Mvc;

namespace CVBanken.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadsController : ControllerBase
    {
        private readonly Context _context;

        public DownloadsController(Context context)
        {
            this._context = context;
        }

        public async Task<IActionResult> GetFile(int id)
        {
            var file = await _context.Files.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }
            try
            {
                return File(file.Data, $"application/{file.Ext}", file.Name);
            }
            catch
            {
                return BadRequest();
            }
        }
        
        // public async Task<IActionResult> GetPicture(int id)
        // {
        //     var file = await _context.ProfilePictures.FindAsync(id);
        //     if (file == null)
        //     {
        //         return NotFound();
        //     }
        //     try
        //     {
        //         Console.WriteLine("");
        //         return Ok();
        //         // return File(file.Data, $"application/{file.Ext}", file.Name);
        //     }
        //     catch
        //     {
        //         return BadRequest();
        //     }
        // }
        
    }
}