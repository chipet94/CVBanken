using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVBanken.Data.Helpers;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Auth;
using CVBanken.Data.Models.Requests;
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

            return categories.Where(p => !p.Hidden).ToResponse();
        }

        [HttpGet("{name}")]
        public async Task<CategoryResponse> GetByCategory(string name)
        {
            var category = await _context.GetCategoryByName(name);
            return category.ToResponse();
        }

        [HttpGet("{id:int}")]
        public async Task<CategoryResponse> GetCategory(int id)
        {
            var category = await _context.GetCategoryById(id);
            return category.ToResponse();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Create(CategoryRequest model)
        {
            if (model == null) return BadRequest();
            var category = model.ToCategory();
            try
            {
                await _context.CreateCategory(category);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _context.DeleteCategory(id);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            try
            {
                await _context.UpdateCategory(id, category);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }

            return NoContent();
        }
    }
}