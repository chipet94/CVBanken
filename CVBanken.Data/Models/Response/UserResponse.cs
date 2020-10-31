using System.Collections.Generic;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Data.Models.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public int ProgrammeId{ get; set; }
        
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string Email{ get; set; }
        public string Role{ get; set; }

        public string Category{ get; set; }
        public string Programme{ get; set; }
        public string Url{ get; set; }
        public string Description{ get; set; }
        
        public bool GotCv{ get; set; }
        public bool Private{ get; set; }
        public bool Searching{ get; set; }
        public IEnumerable<FileResponse> Files { get; set; }

        public static UserResponse FromUser(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                ProgrammeId = user.ProgrammeId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                Category = user.Programme.Category.Name,
                Programme = user.Programme.Name,
                Url = user.Url,
                Description = user.Description,
                GotCv = user.GotCv(),
                Private = user.Private,
                Searching = user.Searching,
                Files = user.Files.WithoutDatas() 
            };
        }
    }
}