//using API_CVPortalen.Helpers.Programmes;
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


        public string Role { get; set; } = "User";//Auth.Role.User;
        public string Token { get; set; }
    }
}