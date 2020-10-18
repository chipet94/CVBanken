namespace CVBanken.Data.Models
{
    public class ProfileRequest
    {
        public string Description { get; set; }
        public bool Private { get; set; }
        public bool Searching { get; set; }
        public ProfilePicture ProfilePicture { get; set; }
    }
}