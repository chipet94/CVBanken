using CVBanken.Data.Models.Auth;

namespace CVBanken.Data.Models.Response
{
    public class SessionResponse
    {
        public string Role { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public static SessionResponse FromUser(User user)
        {
            return new SessionResponse
            {
                Role = user.Role,
                Token = user.Token,
                Name = user.Student.FullName(),
                Url = user.Student.Url
            };
        }
    }
}