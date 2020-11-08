using System;
using System.Linq;
using System.Threading.Tasks;
using CVBanken.Data.Helpers;
using CVBanken.Data.Models.Auth;
using CVBanken.Data.Models.Database;
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
    public class AuthController : ControllerBase
    {
        private readonly Context _context;
        private readonly IUserService _userService;

        public AuthController(IUserService userService, Context context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(AuthRequest model)
        {
            var user = await _userService.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new {error = "Username or password is incorrect"});

            return Ok(user.ToAuthResponse());
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(r => r.Errors));
            if (request == null) return BadRequest();
            try
            {
                await _userService.Create(request);
            }
            catch (Exception e)
            {
                return BadRequest(new {errors = e.Message});
            }

            return Ok("Success!");
        }

        [HttpGet("/super")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> AdminGetAll()
        {
            var users = await _userService.AdminGetAll();
            return Ok(users.ToSafeResponse());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var users = User.IsInRole(Role.Admin) ? await _userService.AdminGetAll() : await _userService.GetAll();
            return Ok(users.ToSafeResponse());
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.IsInRole(Role.Admin))
                if (User.Identity.Name != id.ToString())
                    return Unauthorized();

            try
            {
                var deleted = await _userService.Delete(id);
                if (deleted) return Ok();

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(new {error = e.Message});
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request)
        {
            if (string.IsNullOrEmpty(User.Identity.Name)) return Unauthorized();

            var curr_id = int.Parse(User.Identity.Name);

            if (curr_id != id && !User.IsInRole(Role.Admin))
                return Unauthorized("Only Admins can edit other peoples profiles.");
            // if (!string.IsNullOrEmpty(request.Password))
            //     if (!User.IsInRole(Role.Admin))
            //     {
            //         var correct = await _userService.ConfirmPassword(curr_id, request.OldPassword);
            //         if (!correct) throw new ValidationException("Invalid password.");
            //     }

            try
            {
                await _userService.Update(id, request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new UnprocessableEntityObjectResult("Something went wrong");
            }

            return Ok();
        }

        [HttpPut]
        [Route("{id}/picture")]
        [Authorize]
        public async Task<IActionResult> UpdatePicture(int id, IFormFile image)
        {
            if (image == null) return BadRequest("No image was provided.");
            if (string.IsNullOrEmpty(User.Identity.Name)) return Unauthorized();

            var curr_id = int.Parse(User.Identity.Name);

            if (curr_id != id && !User.IsInRole(Role.Admin))
                return Unauthorized("Only Admins can edit other peoples profiles.");

            try
            {
                await _userService.UpdatePicture(id, image);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.Message);
            }

            return NoContent();
        }

        [HttpGet]
        [Authorize]
        [Route("loggedin")]
        public async Task<ActionResult> IsLoggedIn()
        {
            if (string.IsNullOrEmpty(User.Identity.Name)) return Unauthorized();

            var curr_id = int.Parse(User.Identity.Name);
            var user = await _context.Users.FindAsync(curr_id);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user.ToAuthResponse());
        }
    }
}