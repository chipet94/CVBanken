namespace CVBanken.Data.Models.Site
{
    public class HomeInfo
    {
        public int Id { get; set; }
        public SlideInfo[] Slides { get; set; }
        public string Content { get; set; }
    }
    public static class HomeInfoBuilder
    {
        public static HomeInfo Default()
        {
            return new HomeInfo
            {
                Slides = new []
                {
                    new SlideInfo{Title = "PlAcEhOlDeR Title", Subtitle = "Placeholder subtitle",Text ="placeholder text", Color = "primary"},
                    new SlideInfo{Title = "PlAcEhOlDeR 2 Title", Text = "Placeholder text without subtitle", Color = "info"},
                    
                },
                Content = "<div> Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                          " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam," +
                          " quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat." +
                          " Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur." +
                          " Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum<div>"
            };
        }
    }
}