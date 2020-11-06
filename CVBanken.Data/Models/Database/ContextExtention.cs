using CVBanken.Data.Helpers.Database;

namespace CVBanken.Data.Models.Database
{
    public static class ContextExtention
    {
        public static void ApplySeedData(this Context context)
        {
            SeedData.Seed(context);
        }
    }
}