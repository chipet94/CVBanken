using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CVBanken.Data.Models;
using CVBanken.Services.EducationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVBanken.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgrammeController: ControllerBase
    {
        private readonly IEducationService _context;
        public ProgrammeController(IEducationService educationService)
        {
            _context = educationService;
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(Programme model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            try
            {
                await _context.Create(model);
            }
            catch (Exception e)
            {
                Conflict(e);
            }

            return Ok();
        }
        
        [HttpGet]
        public async Task<IEnumerable<Programme>> GetAll()
        {
            return await _context.GetAllProgrammes();
        }
        
        [HttpGet("{id}")]
        public async Task<Programme> GetById(int id)
        {
            return await _context.GetProgrammeById(id);
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
                return this.Problem(e.Message);
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