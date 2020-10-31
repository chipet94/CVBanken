using System.Collections.Generic;
using System.Linq;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Response;

namespace CVBanken.Data.Helpers
{
    public static class ProgrammeExtentions
    {
        public static IEnumerable<ProgrammeResponse> ToResponse(this IEnumerable<Programme> programmes)
        {
            return programmes?.Select(x => x.ToResponse());
        }

        public static ProgrammeResponse ToResponse(this Programme programme)
        {
            return ProgrammeResponse.FromProgramme(programme);
        }
    }
}