using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API_CVPortalen.Models.Auth;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Requests;
using CVBanken.Services.FileServices;
using CVBanken.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediaTypeHeaderValue = Microsoft.Net.Http.Headers.MediaTypeHeaderValue;

namespace CVBanken.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFileManagerService _context;
        public FilesController(IFileManagerService context)
        {
            this._context = context;
        }
        
        [HttpGet]
        public async Task<IEnumerable<object>> GetAll()
        {
            var files = await _context.GetAll();
            return files.WithoutDatas();
        }
        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IEnumerable<object>> GetAllByUser(int userId)
        {
            var files = (await _context.GetAll()).Where(f => f.OwnerID == userId);
            return files.WithoutDatas();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<byte[]> GetById(int id)
        { 
            var userFile = await _context.GetFile(id);
            return userFile.Data;
        }
        [HttpGet]
        [Route("download/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            try
            {
                var file = await  _context.GetFile(id);
                if (file == null)
                {
                    return NotFound();
                }

               return File(file.Data, $"application/{file.Ext}", file.Name);
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [HttpPost]
        [Authorize]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm]IFormFile[] files)
        {
            try
            {
                if (files == null) return BadRequest();
                
                int uploaderId;
                int.TryParse(User.Identity.Name, out uploaderId);
                if (User.Identity.Name == null) return BadRequest("Failed - No user ID was found.");
                try
                {
                    await _context.ValidateUploadRequest(files, uploaderId);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
                foreach (var file in files)
                {
                    await _context.Upload(uploaderId, file);
                }

                return NoContent();
                //return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
        //obsolete just for debuging
        [HttpPost]
        [Authorize]
        [Route("upload_files")]
        public async Task<string> UploadMany([FromForm]UploadFileRequest files)
        {
            try
            {
                if (files != null)
                {
                    int uploaderId;
                    int.TryParse(User.Identity.Name, out uploaderId);
                    if (User.Identity.Name == null) return "Failed - No user ID was found.";
                    foreach (var file in files.FileData)
                    {
                        await _context.Upload(uploaderId, file);
                    }
                    return "completed.";

                }

                return "Failed";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet]
        [Route("setcv/{id}")]
        [Authorize]
        public async Task<IActionResult> SetCv(int id)
        {
            if (!User.IsInRole(Role.Admin))
            {
                var file = await _context.GetFile(id);
                if (User.Identity.Name != file.OwnerID.ToString())
                {
                    return BadRequest("You're not the owner of this file.");
                }
            }

            try
            {
                await _context.SetCv(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _context.RemoveFile(id);
            }
            catch (Exception e)
            {
                return this.Problem(e.Message);
            }

            return NoContent();
        }
        
        
    }
}