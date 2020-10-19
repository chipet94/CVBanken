using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API_CVPortalen.Models.Auth;
using CVBanken.Data.Models;
using CVBanken.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBanken.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _context;
        private readonly IUserService _userService;
        public ProfileController(IProfileService context, IUserService userService) 
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<object> Get()
        {
            if (User.Identity.Name == null)
            {
                return null;
            }
            var id = int.Parse(User.Identity.Name);

            var profile = await _context.GetByUser(id);
            return profile.ToResponse();
        }
        [HttpGet]
        [Route("all")]
        public async Task<object> GetAll()
        {
            var profiles = await _context.GetAll();
            return profiles.ToResponse();
        }
        [HttpGet]
        [Route("url/{url}")]
        public async Task<IActionResult> GetByUrl(string url)
        {
            var profile = await _context.GetByUrl(url);
            if (profile == null)
            {
                return NotFound();
            }
            if (profile.Private)
            {
                return BadRequest("Profile Is private.");
            }
            return Ok(profile.ToResponse());
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBy(int id)
        {
            var profile = await _context.GetById(id);
            if (profile == null)
            {
                return NotFound();
            }
            if (profile.Private)
            {
                return BadRequest("Profile Is private.");
            }
            return Ok(profile.ToResponse());
        }
        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, UpdateProfileRequest request)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return Unauthorized();
            }

            var curr_id = int.Parse(User.Identity.Name);

            if (curr_id != id && !User.IsInRole(Role.Admin))
            {
                return Unauthorized("Only Admins can edit other peoples profiles.");
            }

            if (request.User != null)
            {
                if (!string.IsNullOrEmpty(request.User.Password))
                {
                    if (!User.IsInRole(Role.Admin))
                    {
                        var correct = await  _userService.ConfirmPassword(curr_id, request.User.OldPassword);
                        if (!correct)
                        {
                            throw new ValidationException("Invalid password.");
                        }
                    }
                }
            }

            var profile = await _context.GetById(id);
            if (profile == null)
            {
                return NotFound();
            }

            try
            {
                await _context.Update(id, request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new UnprocessableEntityObjectResult("Something went wrong");
            }
            return Ok(profile.ToResponse());
        }
        [HttpPut]
        [Route("{id}/picture")]
        [Authorize]
        public async Task<IActionResult> UpdatePicture(int id, IFormFile image)
        {
            if (image == null)
            {
                return BadRequest("No image was provided.");
            }
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return Unauthorized();
            }

            var curr_id = int.Parse(User.Identity.Name);

            if (curr_id != id && !User.IsInRole(Role.Admin))
            {
                return Unauthorized("Only Admins can edit other peoples profiles.");
            }
            var profile = await _context.GetById(id);
            if (profile == null)
            {
                return NotFound();
            }

            try
            {
                await _context.UpdatePicture(id, image);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new UnprocessableEntityObjectResult("Something went wrong");
            }
            return NoContent();
        }
        [HttpGet]
        [Route("{id}/picture")]
        public async Task<IActionResult> GetPicture(int id)
        {
            var profile = await _context.GetById(id);
            if (profile == null)
            {
                return NotFound();
            }

            if (profile.ProfilePicture == null || profile.ProfilePicture.ImageData == null)
            {
                return NoContent();
            }
            return File(profile.ProfilePicture.ImageData,"image/jpeg");
        }
        
    }
}