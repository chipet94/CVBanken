using System.Collections.Generic;
using System.Linq;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Data.Models
{
    public static class UserFileExtentions
    {
        public static IEnumerable<object> WithoutDatas(this IEnumerable<UserFile> files)
        {
            return files.Select(m => m.WithoutData()).ToArray();
        }
        public static object WithoutData(this UserFile file)
        {
            return new {file.Id, file.OwnerID, file.Name, file.Ext, file.Size, file.Uploaded };
        }
    }
}