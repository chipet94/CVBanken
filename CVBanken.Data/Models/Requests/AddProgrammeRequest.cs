using System;

namespace CVBanken.Data.Models.Requests
{
    public class ProgrammeRequest
    {
        public string Name { get; set; }
        public bool Hidden { get; set; } = false;
        public string Location { get; set; }
        public Lia[] Lias { get; set; }
        public int CategoryId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }


        public Programme ToProgramme()
        {
            return new Programme
            {
                Name = Name,
                Hidden = Hidden,
                Location = Location,
                Lias = Lias,
                CategoryId = CategoryId,
                Start = Start,
                End = End
            };
        }
    }
}