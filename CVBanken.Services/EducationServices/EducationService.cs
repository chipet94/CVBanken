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
            _context = context;
        }

        public async Task<IEnumerable<Programme>> GetAllProgrammes()
        {
            return await _context.Programmes.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category> GetCategoryByName(string name)
        {
            return await _context.Categories.FirstAsync(c => c.Name.ToUpper() == name.ToUpper());
        }
        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Programme> GetProgrammeByName(string name)
        {
            return await _context.Programmes.FirstAsync(c => c.Name.ToUpper() == name.ToUpper());
        }
        public async Task<IEnumerable<Programme>> GetAllEducationsByCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category.Programmes;
        }


        public async Task<IEnumerable<User>> GetStudents(int id)
        {
            var programme = await _context.Programmes.FindAsync(id);
            return programme.Students;
        }

        public async Task<Programme> GetProgrammeById(int id)
        {
            var programme = await _context.Programmes.FindAsync(id);
            return programme ?? null;
        }

        public async Task Create(Programme programme)
        {
            var exists = await _context.Programmes.AnyAsync(p => p.Name == programme.Name);
            if (exists)
            {
                throw new Exception($"A programme with the name '{programme.Name}' already exists.");
            }
            try
            {
                await _context.AddAsync(programme);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

  
        }

        public async Task Update(int id, Programme programme)
        {
            var toUpdate = await _context.Programmes.FindAsync(id);
            foreach (var property in typeof(Programme).GetProperties())
            {
                var sourceProp = property.GetValue(programme);
                var targetProp = property.GetValue(toUpdate);
                if (sourceProp != null && targetProp != sourceProp) property.SetValue(toUpdate, sourceProp);
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
            if (toDelete == null) throw new Exception("not found.");
 
            try
            {
                if (toDelete.Students.Any())
                {
                    foreach (var student in toDelete.Students)
                    {
                        student.ProgrammeId = 1000;
                    }
                    _context.UpdateRange(toDelete.Students);
                }
                _context.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task CreateCategory(Category category)
        {
            var exists = await _context.Categories.AnyAsync(p => p.Name == category.Name);
            if (exists)
            {
                throw new Exception($"A category with the name '{category.Name}' already exists.");
            }
            try
            {
                await _context.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteCategory(int id)
        {
            var toDelete = await _context.Categories.FindAsync(id);
            if (toDelete == null) throw new Exception("not found.");
 
            try
            {
                if (toDelete.Programmes.Any())
                {
                    foreach (var programme in toDelete.Programmes)
                    {
                        programme.CategoryId = 1;
                    }
                    _context.UpdateRange(toDelete.Programmes);
                }
                _context.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}