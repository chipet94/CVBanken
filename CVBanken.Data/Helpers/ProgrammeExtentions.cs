using System.Collections.Generic;
using System.Linq;
using CVBanken.Data.Models;

namespace CVBanken.Data.Helpers
{
    public static class ProgrammeExtentions
    {
        public static IEnumerable<object> ToResponse(this IEnumerable<Programme> programmes)
        {
            return programmes?.Select(x => x.ToResponse());
        }
        public static object ToResponse(this Programme programme)
        {
            return new {programme.Id, programme.Name, Category = programme.Category.Id, categoryName= programme.Category.Name, 
                TotalStudents = programme.Students.Count,
                PublicStudents = programme.Students.Count(s => !s.Private), 
                programme.Start, programme.End };
        }
    }
}