using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVBanken.Data.Models
{
    public class Programme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProgrammeCategory Category { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        
        
        public enum ProgrammeCategory
        {
            Empty,
            Webbutvecklare,
            AppUtvecklare,
            JavaUtvecklare,
            Net_Utvecklare,
            Mjukvarutestare,
            Frontendutvecklare,
            IT_Projektledare,
            JavaScriptutvecklare,
            UX_designer,
        }
    }
}