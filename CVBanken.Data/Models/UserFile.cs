using System;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Data.Models
{
    public class UserFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string Ext { get; set; }
        
        public bool IsCv { get; set; }
        public long Size { get; set; }
        public DateTime Uploaded { get; set; }
        public int OwnerID { get; set; }
        public virtual User Owner { get; set; }
    }
}