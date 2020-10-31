using System.Collections.Generic;
using System.Linq;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Data.Helpers
{
    public static class UserExtentions
    {
        public static IEnumerable<Models.Auth.User> WithoutPasswords(this IEnumerable<Models.Auth.User> users) 
        {
            if (users == null) return null;

            return Enumerable.Select(users, x => WithoutPassword(x));
        }

        public static Models.Auth.User WithoutPassword(this Models.Auth.User user) 
        {
            if (user == null) return null;    

            user.PasswordHash = null;
            user.PasswordSalt = null;
            return user;
        }
        public static IEnumerable<object> ToSafeResponse(this IEnumerable<User> users)
        {
            return users?.Select(x => x.ToSafeResponse());
        }
        public static object ToSafeResponse(this User user)
        {
            return new { user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Role,
                user.ProgrammeId, 
                categoryName = user.Programme.Category.Name,
                class_name = user.Programme.Name,
                user.Url,
                user.Description,
                gotCv= user.GotCv(),
                user.Private,
                user.Searching
                
            };
        }
        public static object ToProfileResponse(this User user)
        {
            return new { user.Id, user.FirstName, user.LastName,
                gotCv= user.GotCv(),
                user.Email, user.Role, user.ProgrammeId, 
                categoryName = user.Programme.Category.Name, class_name = user.Programme.Name, user.Url};
        }
        public static object ToAuthResponse(this User user)
        {
            return new { user.Id, user.FirstName, user.LastName, user.Email, user.Role, user.ProgrammeId, user.Token};
        }
        
    }
}