using CVBanken.Data.Models.Auth;

namespace CVBanken.Data.Models
{
    public class ProfilePicture
    {
        public int Id { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}