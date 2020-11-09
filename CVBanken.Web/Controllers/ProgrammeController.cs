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
    public class ProgrammeController : ControllerBase
    {
        private readonly IEducationService _context;

        public ProgrammeController(IEducationService educationService)
        {
            _context = educationService;
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Create(ProgrammeRequest model)
        {
            if (model == null) return BadRequest();
            var programme = model.ToProgramme();
            try
            {
                await _context.Create(programme);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IEnumerable<ProgrammeResponse>> GetAll()
        {
            var programmes = await _context.GetAllProgrammes();
            return programmes.ToResponse();
        }

        [HttpGet]
        [Route("category")]
        public async Task<IEnumerable<CategoryResponse>> GetAllCategories()
        {
            var categories = await _context.GetAllCategories();
            if (User.IsInRole(Role.Admin)) return categories.ToResponse();

            return categories.Where(p => !p.Hidden).ToResponse();
        }

        [HttpGet("{id:int}")]
        public async Task<ProgrammeResponse> GetById(int id)
        {
            var programme = await _context.GetProgrammeById(id);
            return programme.ToResponse();
        }

        [HttpGet("{name}")]
        public async Task<ProgrammeResponse> GetByName(string name)
        {
            var programme = await _context.GetProgrammeByName(name);
            return programme.ToResponse();
        }

        [HttpGet("{id}/students")]
        public async Task<IEnumerable<StudentResponse>> GetStudentsIn(int id)
        {
            var programme = await _context.GetProgrammeById(id);
            if (User.IsInRole(Role.Admin)) return programme.Students.ToSafeResponse();
            return programme.Students.Where(u => !u.Private).ToSafeResponse();
        }

        [HttpGet("category/{name}")]
        public async Task<Category> GetByCategory(string name)
        {
            return await _context.GetCategoryByName(name);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _context.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong... " + e.Message);
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