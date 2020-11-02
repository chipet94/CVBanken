using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CVBanken.Data.Helpers;

namespace CVBanken.Data.Models.Response
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Hidden { get; set; }
        public IEnumerable<ProgrammeResponse> Programmes { get; set; }

        public static CategoryResponse FromCategory(Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Hidden = category.Hidden,
                Programmes = category.Programmes.ToResponse()
            };
        }
    }
}