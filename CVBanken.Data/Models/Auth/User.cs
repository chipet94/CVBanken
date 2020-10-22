//using API_CVPortalen.Helpers.Programmes;

using System.ComponentModel.DataAnnotations.Schema;
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

        public string Role { get; set; } = "User";//Auth.Role.User;
        public string Token { get; set; }
    }

    public static class UserBuilder
    {
        public static User NewUser(string email,string password, string firstname, string lastname, int programmeid, string role)
        {
            CreatePasswordHash(password, out var hash,out var salt);
            return new User
            {
                Searching = false,
                Private = false,
                Description = "Skriv något om dig själv...",
                Url = ProfileBuilder.NewProfileUrl(25),
                ProgrammeId = programmeid,
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = role

            };
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}