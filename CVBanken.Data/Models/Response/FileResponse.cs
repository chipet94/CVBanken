using System.Globalization;

namespace CVBanken.Data.Models.Response
{
    public class FileResponse
    {
        public int Id { get; set; }
        public int OwnerID { get; set; }

        public string Name { get; set; }
        public string Ext { get; set; }
        public string Uploaded { get; set; }

        public bool IsCv { get; set; }

        public long Size { get; set; }

        public static FileResponse FromUserFile(UserFile file)
        {
            return new FileResponse
            {
                Id = file.Id,
                OwnerID = file.OwnerID,
                Name = file.Name,
                Ext = file.Ext,
                Size = file.Size,
                Uploaded = file.Uploaded.ToString(CultureInfo.CurrentCulture),
                IsCv = file.IsCv
            };
        }
    }
}