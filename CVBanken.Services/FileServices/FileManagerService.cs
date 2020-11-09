using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CVBanken.Services.FileServices
{
    public class FileManagerService : IFileManagerService
    {
        private readonly Context _context;

        public FileManagerService(Context context)
        {
            _context = context;
        }

        public async Task<UserFile> Upload(int userID, IFormFile formFile)
        {
            try
            {
                var owner = await _context.Students.FindAsync(userID);
                if (owner == null) throw new Exception("Incorrect uploader.");

                var file = await UserFileBuilder.NewUserFileAsync(owner, formFile);

                await _context.Files.AddAsync(file);
                await _context.SaveChangesAsync();
                return file;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task UploadCv(int userID, IFormFile formFile)
        {
            try
            {
                var student = await _context.Students.FindAsync(userID);
                if (student == null) throw new Exception("Incorrect uploader.");
                var file = await UserCvBuilder.NewCvAsync(student, formFile);
                await _context.CvFiles.AddAsync(file);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<UserCv> GetStudentCv(int studentId)
        {
            var file = await _context.CvFiles.FirstAsync(f => f.StudentId == studentId);
            if (file == null) throw new Exception("Cv not found.");

            return file;
        }

        public async Task<UserCv> GetCv(int id)
        {
            var file = await _context.CvFiles.FindAsync(id);
            if (file == null) throw new Exception("Cv not found.");

            return file;
        }

        public async Task<UserFile> GetFile(int id)
        {
            var file = await _context.Files.FindAsync(id);
            if (file == null) throw new Exception("File not found.");

            return file;
        }

        public async Task<IEnumerable<UserFile>> GetAll()
        {
            return await _context.Files.ToListAsync();
        }

        public async Task RemoveFile(int id)
        {
            var target = await _context.Files.FindAsync(id);
            if (target == null) throw new Exception("File not found.");

            _context.Remove(target);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCv(int id)
        {
            var cv = await _context.CvFiles.FindAsync(id);
            if (cv == null) throw new Exception("Student cv not found.");
            var student = await _context.Students.FindAsync(cv.Student.Id);
            if (student != null)
                try
                {
                    student.Cv = null;
                    _context.Update(student);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            _context.Remove(cv);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateUploadRequest(IFormFile[] files, int userId)
        {
            var userFiles = await _context.Files.Where(f => f.OwnerID == userId).ToListAsync();
            if (files.Length + userFiles.Count > FilesSettings.MAX_USER_FILES)
                throw new Exception("Too many files." +
                                    $" you currently have: {userFiles.Count}/" +
                                    $"{FilesSettings.MAX_USER_FILES} " +
                                    $"and you're trying to upload {files.Length} more.");

            if (files.Any(f => f.Length > FilesSettings.MAX_SIZE))
                throw new Exception($"The size limit per file is {(FilesSettings.MAX_SIZE * 10) ^ -6} mb");

            foreach (var file in files)
            {
                var ext = Path.GetExtension(file.FileName);
                if (!FilesSettings.SUPPORTED_FILES.Contains(ext))
                    throw new Exception($"fileformat '{ext}' is not supported.");
            }

            return true;
        }

        public bool ValidateFile(IFormFile file)
        {
            if (file.Length > FilesSettings.MAX_SIZE)
                throw new Exception($"The size limit per file is {(FilesSettings.MAX_SIZE * 10) ^ -6} mb");

            var ext = Path.GetExtension(file.FileName);
            if (!FilesSettings.SUPPORTED_FILES.Contains(ext))
                throw new Exception($"fileformat '{ext}' is not supported.");

            return true;
        }
    }
}