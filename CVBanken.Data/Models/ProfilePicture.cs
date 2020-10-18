using System;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CVBanken.Data.Models
{
    public class ProfilePicture
    {
        public int Id { get; set; }
        public string Url { get; set; } = ProfilePictureBuilder.NewUrl(15);
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        
        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }

    public static class ProfilePictureBuilder
    {
        public static ProfilePicture NewDefault()
        {
            return new ProfilePicture
            {
                Id = 1,
                Url = "Default_Picture",
                ImageData = File.ReadAllBytes("../CVBanken.Data/DefaultFiles/defaultProfilePicture.jpg"),
                ImageTitle = "Default"
            };
        }
        public static string NewUrl(int length)
        {
            var random = new Random();
            const string pool = "abcdefghjiklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(pool, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}