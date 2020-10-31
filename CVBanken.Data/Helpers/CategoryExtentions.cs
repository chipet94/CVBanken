using System.Collections.Generic;
using System.Linq;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Response;

namespace CVBanken.Data.Helpers
{
    public static class CategoryExtentions
    {
        public static IEnumerable<CategoryResponse> ToResponse(this IEnumerable<Category> programmes)
        {
            return programmes?.Select(x => x.ToResponse());
        }

        public static CategoryResponse ToResponse(this Category category)
        {
            return CategoryResponse.FromCategory(category);
        }
    }
}