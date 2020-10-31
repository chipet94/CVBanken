using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API_CVPortalen.Models.Auth;
using CVBanken.Data.Helpers;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Requests.Auth;
using CVBanken.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBanken.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _context;
        public ProfileController(IUserService context) 
        {
            _context = context;
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

            var profile = await _context.GetById(id);
            return profile.ToSafeResponse();
        }
        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        public async Task<object> GetAll()
        {
            var profiles = await _context.GetAll();
            return profiles.ToSafeResponse();
        }
        [HttpGet]
        [Route("url/{url}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUrl(string url)
        {
            var profile = await _context.GetByUrl(url);
            if (profile == null)
            {
                return NotFound();
            }
            if (profile.Private)
            {
                if (!User.IsInRole(Role.Admin))
                {
                    return BadRequest();
                }
            }
            return Ok(profile.ToSafeResponse());
        }
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBy(int id)
        {
            var profile = await _context.GetById(id);
            if (profile == null)
            {
                return NotFound();
            }
            if (profile.Private && !User.IsInRole(Role.Admin))
            {
                return BadRequest("Profile Is private.");
            }
            return Ok(profile.ToSafeResponse());
        }

    }
}