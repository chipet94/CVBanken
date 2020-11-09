using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVBanken.Data.Models;
using CVBanken.Services.FileServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBanken.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFileManagerService _context;

        public FilesController(IFileManagerService context)
        {
            _context = context;
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
        [Route("user/{userId}/cv")]
        public async Task<UserCv> GetUserCv(int userId)
        {
            var cv = await _context.GetStudentCv(userId);
            return cv;
        }

        [HttpDelete]
        [Route("cv/{id}")]
        public async Task<IActionResult> RemoveCv(int id)
        {
            try
            {
                await _context.RemoveCv(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }

            return NoContent();
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
                var file = await _context.GetFile(id);
                if (file == null) return NotFound();

                return File(file.Data, $"application/{file.Ext}", file.Name);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("cv/{id}")]
        public async Task<IActionResult> DownloadCv(int id)
        {
            try
            {
                var file = await _context.GetCv(id);
                if (file == null) return NotFound();

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
        public async Task<IActionResult> Upload([FromForm] IFormFile[] files)
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

                foreach (var file in files) await _context.Upload(uploaderId, file);

                return NoContent();
                //return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        [Authorize]
        [Route("upload/cv/")]
        public async Task<IActionResult> UploadCv([FromForm] IFormFile file)
        {
            try
            {
                if (file == null) return BadRequest();
                int uploaderId;
                int.TryParse(User.Identity.Name, out uploaderId);
                if (User.Identity.Name == null) return BadRequest("Failed - No user ID was found.");
                try
                {
                    _context.ValidateFile(file);
                    await _context.UploadCv(uploaderId, file);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _context.RemoveFile(id);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

            return NoContent();
        }
    }
}