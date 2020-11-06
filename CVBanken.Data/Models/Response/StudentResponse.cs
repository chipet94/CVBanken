using System.Collections.Generic;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Data.Models.Response
{
    public class StudentResponse
    {
        public int Id { get; set; }
        public int ProgrammeId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Category { get; set; }
        public string Programme { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

        public bool GotCv { get; set; }
        public bool Private { get; set; }
        public bool Searching { get; set; }
        public IEnumerable<FileResponse> Files { get; set; }
        public object StudentCv { get; set; }

        public static StudentResponse FromUser(Student user)
        {
            return new StudentResponse
            {
                Id = user.Id,
                ProgrammeId = user.ProgrammeId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Category = user.Programme.Category.Name,
                Programme = user.Programme.Name,
                Url = user.Url,
                Description = user.Description,
                GotCv = user.GotCv(),
                Private = user.Private,
                Searching = user.Searching,
                Files = user.Files.WithoutDatas(),
                StudentCv = user.Cv != null
                    ? new {user.Cv.Name, user.Cv.Id, uploaded = user.Cv.Uploaded.ToShortDateString(), user.Cv.Data}
                    : null //todo Cv modell
            };
        }
    }
}