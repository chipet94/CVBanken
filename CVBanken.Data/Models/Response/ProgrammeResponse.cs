using System.Collections.Generic;

namespace CVBanken.Data.Models.Response
{
    public class ProgrammeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int TotalStudents { get; set; }
        public int PublicStudents { get; set; }
        public ICollection<Lia> Lias { get; set; }
        public bool Hidden { get; set; }

        public static ProgrammeResponse FromProgramme(Programme programme)
        {
            return new ProgrammeResponse
            {
                Id = programme.Id,
                Name = programme.Name,
                Location = programme.Location,
                Lias = programme.Lias,
                Hidden = programme.Hidden,
                CategoryId = programme.CategoryId,
                CategoryName = programme.Category.Name,
                End = programme.End.Date.ToShortDateString(),
                Start = programme.Start.Date.ToShortDateString(),
                TotalStudents = programme.TotalStudents(),
                PublicStudents = programme.PublicStudents()
            };
        }
    }
}