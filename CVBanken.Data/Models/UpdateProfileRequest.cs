using Microsoft.AspNetCore.Http;

namespace CVBanken.Data.Models
{
    public class UpdateProfileRequest
    {
        public UpdateUser User { get; set; }
        public bool Private { get; set; }
        public bool Searching { get; set; }
        public string Description { get; set; }
        public virtual IFormFile ProfilePicture { get; set; }
    }
    public class UpdateUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ProgrammeId { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
    }
    
}