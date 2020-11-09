using System;
using System.IO;
using System.Threading.Tasks;
using CVBanken.Data.Models.Auth;
using Microsoft.AspNetCore.Http;

namespace CVBanken.Data.Models
{
    public class UserFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string Ext { get; set; }

        public bool IsCv { get; set; }
        public long Size { get; set; }
        public DateTime Uploaded { get; set; }
        public int OwnerID { get; set; }
        public virtual Student Owner { get; set; }
    }

    public class UserFileBuilder
    {
        public static async Task<UserFile> NewUserFileAsync(Student owner, IFormFile formFile)
        {
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

            return file;
        }

        public static UserFile NewUserFile(Student owner, IFormFile formFile)
        {
            var file = new UserFile();
            file.OwnerID = owner.Id;
            file.Owner = owner;
            file.Name = formFile.FileName;
            file.Uploaded = DateTime.Now;
            file.Ext = Path.GetExtension(formFile.FileName);
            file.Size = formFile.Length; //storlek i bytes tex: 1 000 000
            using (var dataSource = new MemoryStream())
            {
                formFile.CopyTo(dataSource);
                file.Data = dataSource.ToArray();
            }

            return file;
        }
    }
}