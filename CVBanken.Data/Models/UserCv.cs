using System;
using System.IO;
using System.Threading.Tasks;
using CVBanken.Data.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Office.Interop.Word;

namespace CVBanken.Data.Models
{
    public class UserCv
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string Ext { get; set; }
        public long Size { get; set; }
        public DateTime Uploaded { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }

    public class UserCvBuilder
    {
        public static async Task<UserCv> NewCvAsync(Student student, IFormFile formFile)
        {
            var file = new UserCv();
            file.StudentId = student.Id;
            file.Uploaded = DateTime.Now;
            file.Ext = Path.GetExtension(formFile.FileName);
            file.Name = $"CV_{student.FullName()}" + file.Ext;
            file.Size = formFile.Length;
            await using (var dataSource = new MemoryStream())
            {
                formFile.CopyTo(dataSource);
                file.Data = dataSource.ToArray();
            }

            return file;
        }

        public static UserCv NewCv(Student student, IFormFile formFile)
        {
            var file = new UserCv();
            file.StudentId = student.Id;
            file.Name = $"CV_{student.FullName()}";
            file.Uploaded = DateTime.Now;
            file.Ext = Path.GetExtension(formFile.FileName);
            file.Size = formFile.Length;
            using (var dataSource = new MemoryStream())
            {
                formFile.CopyTo(dataSource);
                file.Data = dataSource.ToArray();
            }

            return file;
        }
    }
}