using System.Threading.Tasks;
using CVBanken.Data.Helpers;
using CVBanken.Data.Models.Auth;
using CVBanken.Data.Models.Database;
using CVBanken.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVBanken.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Context _context;
        private readonly IUserService _userService;

        public UserController(IUserService userService, Context context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpGet("super/all")]
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
            var users = await _userService.GetAll();
            return Ok(users.ToSafeResponse());
        }

        [HttpGet("programme/{id}")]
        public async Task<object> GetAllUsers(int id)
        {
            var users = await _userService.GetAllUsersInProgramme(id);
            return users.ToSafeResponse();
        }

        [HttpGet("category/{category}")]
        public async Task<object> GetAllUsersInCategory(int category)
        {
            var users = await _userService.GetAllUserInCategory(category);
            return users.ToSafeResponse();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            if (user != null) return Ok(user.ToSafeResponse());

            return NotFound();
        }

        [HttpGet]
        [Route("{id}/picture")]
        public async Task<IActionResult> GetPicture(int id)
        {
            var profile = await _userService.GetById(id);
            if (profile == null) return NotFound();

            if (profile.ProfilePicture == null || profile.ProfilePicture.ImageData == null) return NoContent();
            return File(profile.ProfilePicture.ImageData, "image/jpeg");
        }
    }
}