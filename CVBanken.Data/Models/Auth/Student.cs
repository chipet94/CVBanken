using System.Collections.Generic;

namespace CVBanken.Data.Models.Auth
{
    public class Student
    {
        public int Id { get; set; }
        public int ProgrammeId { get; set; } //ProgrammeBuilder.Empty.Id;
        public virtual Programme Programme { get; set; }
        public string Email { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }

        public bool Private { get; set; }

        public bool Searching { get; set; }

        public string Url { get; set; }

        public string Description { get; set; } = "Skriv något om dig själv.";

        public virtual ProfilePicture ProfilePicture { get; set; }

        public virtual IEnumerable<UserFile> Files { get; set; }

        public virtual UserCv Cv { get; set; }

        public void SetCv(UserCv cv)
        {
            Cv = cv;
        }

        public bool GotCv()
        {
            return Cv != null;
        }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}