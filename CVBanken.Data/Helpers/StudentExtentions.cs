using System.Collections.Generic;
using System.Linq;
using CVBanken.Data.Models.Auth;
using CVBanken.Data.Models.Response;

namespace CVBanken.Data.Helpers
{
    public static class StudentExtentions
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

        public static IEnumerable<StudentResponse> ToSafeResponse(this IEnumerable<Student> users)
        {
            return users?.Select(x => x.ToSafeResponse());
        }

        public static StudentResponse ToSafeResponse(this Student user)
        {
            return StudentResponse.FromUser(user);
        }

        public static SessionResponse ToAuthResponse(this User user)
        {
            return SessionResponse.FromUser(user);
        }
    }
}