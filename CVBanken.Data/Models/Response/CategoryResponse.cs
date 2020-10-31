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
        public IEnumerable<ProgrammeResponse> Programmes { get; set; }

        public static CategoryResponse FromCategory(Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Programmes = category.Programmes.ToResponse()
            };
        }
    }
}