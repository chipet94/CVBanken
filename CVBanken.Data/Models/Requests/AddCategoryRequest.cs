namespace CVBanken.Data.Models.Requests
{
    public class CategoryRequest
    {
        public string Name { get; set; }

        public bool Hidden { get; set; } = false;

        public Category ToCategory()
        {
            return new Category
            {
                Name = Name,
                Hidden = Hidden
            };
        }
    }
}