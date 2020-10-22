using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Auth;
using CVBanken.Data.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CVBanken.Services.EducationServices
{
    public class EducationService : IEducationService
    {
        private readonly Context _context;

        public EducationService(Context context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Programme>> GetAllProgrammes()
        {
            return await _context.Programmes.ToListAsync();
        }
        public async Task<IEnumerable<Programme>> GetAllEducationsByCategory(int id)
        {
            return await _context.Programmes.Where(p => p.Category == (Programme.ProgrammeCategory) id).ToListAsync();
        }
        

        public async Task<Programme> GetProgrammeById(int id)
        {
            var programme = await _context.Programmes.FindAsync(id);
            return programme ?? null;
        }
        
        public async Task Create(Programme programme)
        {
            await _context.AddAsync(programme);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Programme programme)
        {
            var toUpdate = await _context.Programmes.FindAsync(id);
            foreach (var property in typeof(Programme).GetProperties())
            {
                var sourceProp = property.GetValue(programme);
                var targetProp = property.GetValue(toUpdate);
                if (sourceProp != null && targetProp != sourceProp)
                {
                    property.SetValue(toUpdate, sourceProp);
                }
            }

            try
            {
                _context.Programmes.Update(toUpdate);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
      
        }

        public async Task Delete(int id)
        {
            var toDelete = await _context.Programmes.FindAsync(id);
            if (toDelete == null)
            {
                throw new Exception("not found.");
            }
            _context.Programmes.Remove(toDelete);
            await _context.SaveChangesAsync();
        }
    }
}