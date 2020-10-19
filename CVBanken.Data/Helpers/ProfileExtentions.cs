using System.Collections.Generic;
using System.IO;
using System.Linq;
using CVBanken.Data.Helpers;

namespace CVBanken.Data.Models
{
    public static class ProfileExtentions
    {
        public static IEnumerable<object> ToResponse(this IEnumerable<Profile> profiles)
        {
            return profiles?.Select(x => x.ToResponse());
        }
        public static object ToResponse(this Profile profile)
        {
            return new {profile.ProfileId, profile.Url, profile.Private, profile.Searching, profile.Description, profile.UserId,
                user = profile.User.ToSafeResponse()};
        }

        public static object ToPictureResponse(this ProfilePicture picture)
        {
            return new {picture.ImageData, picture.ImageTitle };
        }
        
    }
}