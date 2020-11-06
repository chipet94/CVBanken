//using API_CVPortalen.Helpers.Programmes;

using System.Security.Cryptography;
using System.Text;

namespace CVBanken.Data.Models.Auth
{
    public class User
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; } = "User"; //Auth.Role.User;
        public string Token { get; set; }
    }

    public static class UserBuilder
    {
        public static User NewUser(string email, string password, string firstname, string lastname, int programmeid,
            string role)
        {
            CreatePasswordHash(password, out var hash, out var salt);
            return new User
            {
                Email = email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = role,
                Student = new Student
                {
                    Searching = false,
                    Private = false,
                    Description = "Skriv något om dig själv...",
                    Url = ProfileBuilder.NewProfileUrl(25),
                    ProgrammeId = programmeid,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email
                }
            };
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}