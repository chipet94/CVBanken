using System.Collections.Generic;
using System.Linq;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Auth;
using CVBanken.Data.Models.Response;

namespace CVBanken.Data.Helpers
{
    public static class UserExtentions
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            if (users == null) return null;

            return users.Select(x => WithoutPassword(x));
        }

        public static User WithoutPassword(this User user)
        {
            if (user == null) return null;

            user.PasswordHash = null;
            user.PasswordSalt = null;
            return user;
        }

        public static IEnumerable<UserResponse> ToSafeResponse(this IEnumerable<User> users)
        {
            return users?.Select(x => x.ToSafeResponse());
        }

        public static UserResponse ToSafeResponse(this User user)
        {
            return UserResponse.FromUser(user);
        }

        public static SessionResponse ToAuthResponse(this User user)
        {
            return SessionResponse.FromUser(user);
        }
    }
}