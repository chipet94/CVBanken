//using API_CVPortalen.Helpers.Programmes;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using API_CVPortalen.Models.Auth;

namespace CVBanken.Data.Models.Auth
{
    public class User
    {
        public int Id { get; set; }
        public int ProgrammeId { get; set; } = 0;//ProgrammeBuilder.Empty.Id;
        public virtual Programme Programme { get; set; }
        
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        
        public string LastName { get; set; }
        public string FirstName { get; set; }
        
        public bool Private { get; set; } = false;
        
        public bool Searching { get; set; } = false;
        
        public string Url { get; set; }
        
        public string Description { get; set; } = "Skriv något om dig själv.";
        
        public virtual ProfilePicture ProfilePicture { get; set; }
        
        public virtual IEnumerable<UserFile> Files { get; set; }

        public string Role { get; set; } = "User";//Auth.Role.User;
        public string Token { get; set; }


        public void SetCv(UserFile cv)
        {
            foreach (var file in Files)
            {
                if (file != cv)
                {
                    file.IsCv = false;
                }
                else
                {
                    file.IsCv = true;
                }
            }
        }

        public bool GotCv()
        {
            return Files.Any(f => f.IsCv);
        }
    }
}