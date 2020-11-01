using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVBanken.Data.Helpers;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Auth;
using CVBanken.Data.Models.Response;
using CVBanken.Services.EducationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVBanken.Web.Controllers
{
[ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IEducationService _context;

        public CategoryController(IEducationService educationService)
        {
            _context = educationService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryResponse>> GetAllCategories()
        {
            var categories = await _context.GetAllCategories();
            if (User.IsInRole(Role.Admin)) return categories.ToResponse();

            return categories.Where(p => p.Name != "Default").ToResponse();
        }
        [HttpGet("{name}")]
        public async Task<CategoryResponse> GetByCategory(string name)
        {
            var category = await _context.GetCategoryByName(name);
            return category.ToResponse();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _context.Delete(id);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Programme programme)
        {
            try
            {
                await _context.Update(id, programme);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }

            return NoContent();
        }
    }
}