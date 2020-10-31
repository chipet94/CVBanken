using System.Collections.Generic;
using System.Linq;
using CVBanken.Data.Models.Response;

namespace CVBanken.Data.Models
{
    public static class UserFileExtentions
    {
        public static IEnumerable<FileResponse> WithoutDatas(this IEnumerable<UserFile> files)
        {
            return files.Select(m => m.WithoutData()).ToArray();
        }

        public static FileResponse WithoutData(this UserFile file)
        {
            return FileResponse.FromUserFile(file);
        }
    }
}