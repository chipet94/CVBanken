using System.Collections.Generic;
using System.Linq;
using CVBanken.Data.Models;

namespace CVBanken.Data.Helpers
{
    public static class CategoryExtentions
    {
        public static IEnumerable<object> ToResponse(this IEnumerable<Category> programmes)
        {
            return programmes?.Select(x => x.ToResponse());
        }
        public static object ToResponse(this Category category)
        {
            return new {category.Id,category.Name, Programmes = category.Programmes.ToResponse()};
        }
    }
}