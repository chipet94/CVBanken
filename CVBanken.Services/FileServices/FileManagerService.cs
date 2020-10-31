using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Database;
using CVBanken.Data.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVBanken.Services.FileServices
{
    public class FileManagerService : IFileManagerService
    {
        private readonly Context _context;

        public FileManagerService(Context context)
        {
            this._context = context;
        }

        public async Task<UserFile> Upload(int userID, IFormFile formFile)
        {
            try
            {
                var owner = await _context.Users.FindAsync(userID);
                if (owner == null)
                {
                    throw new Exception("Incorrect uploader.");
                }

                var file = new UserFile();
                file.OwnerID = owner.Id;
                file.Owner = owner;
                file.Name = formFile.FileName;
                file.Uploaded = DateTime.Now;
                file.Ext = Path.GetExtension(formFile.FileName);
                file.Size = formFile.Length; //storlek i bytes tex: 1 000 000
                await using (var dataSource = new MemoryStream())
                {
                    formFile.CopyTo(dataSource);
                    file.Data = dataSource.ToArray();
                }
                
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

        public async Task<UserFile> GetFile(int id)
        {
            var file = await _context.Files.FindAsync(id);
            if (file == null)
            {
                throw new Exception("File not found.");
            }

            return file;
        }

        public async Task SetCv(int id)
        {
            var file = await _context.Files.FindAsync(id); 
            file.Owner.SetCv(file);
            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<UserFile>> GetAll()
        {
            return await _context.Files.ToListAsync();
        }

        public async Task RemoveFile(int id)
        {
            var target = await _context.Files.FindAsync(id);
            if (target == null)
            {
                throw new Exception("File not found.");
            }

            _context.Remove(target);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateUploadRequest(IFormFile[] files, int userId)
        {
            var userFiles = await _context.Files.Where(f => f.OwnerID == userId).ToListAsync();
            if (files.Length + userFiles.Count > FilesSettings.MAX_USER_FILES )
            {
                throw new Exception($"Too many files." +
                                    $" you currently have: {userFiles.Count}/" +
                                    $"{FilesSettings.MAX_USER_FILES} " +
                                    $"and you're trying to upload {files.Length} more.");
            }

            if (files.Any(f => f.Length > FilesSettings.MAX_SIZE))
            {
                throw new Exception($"The size limit per file is {FilesSettings.MAX_SIZE * 10^-6} mb");
            }

            foreach (var file in files)
            {
                var ext = Path.GetExtension(file.FileName);
                if (!FilesSettings.SUPPORTED_FILES.Contains(ext))
                {
                    throw new Exception($"fileformat '{ext}' is not supported.");
                }
            }

            return true;
        }
    }
}