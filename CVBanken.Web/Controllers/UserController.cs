using System;
using System.Threading.Tasks;
using API_CVPortalen.Models.Auth;
using CVBanken.Data.Helpers;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Auth;
using CVBanken.Data.Models.Database;
using CVBanken.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVBanken.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly Context _context;

        public UserController(IUserService userService, Context context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(UserRequest.Authenticate model)
        {
            var user = await _userService.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new {error = "Username or password is incorrect"});

            return Ok(user.ToAuthResponse());
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Create(UserRequest.Register request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            try
            {
                var user = await _userService.Create(request.ToUser(), request.Password);
                return Ok("Success!");
            }
            catch (Exception e)
            {
                return BadRequest(new {error = e.Message});
            }
        }

        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users.ToSafeResponse());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            if (user != null)
            {
                return Ok(user.ToSafeResponse());
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.IsInRole(Role.Admin))
            {
                if (User.Identity.Name != id.ToString())
                {
                    return Unauthorized();
                }
            }

            try
            {
                var deleted = await _userService.Delete(id);
                if (deleted)
                {
                    return Ok();
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(new {error = e.Message});
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserRequest.Update request)
        {
            if (!User.IsInRole(Role.Admin))
            {
                if (User.Identity.Name != id.ToString())
                {
                    return Unauthorized();
                }
            }

            var user = request.ToUser();
            user.Id = id;
            try
            {
                var updated = await _userService.Update(user, request.Password);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new {error = e.Message});
            }
        }

        [HttpGet]
        [Authorize]
        [Route("loggedin")]
        public ActionResult IsLoggedIn()
        {
            return Ok();
        }
    }
}