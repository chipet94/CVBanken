using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Data.Models
{
    public class Profile
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ProfileId { get; set; }

        public bool Private { get; set; } = false;
        
        public bool Searching { get; set; } = false;
        public string Url { get; set; }
        public string Description { get; set; } = "Skriv något om dig själv.";
        public virtual ProfilePicture ProfilePicture { get; set; }

    }

    public static class ProfileBuilder
    {
        public static Profile NewProfile()
        {
            return new Profile{Url = NewProfileUrl(25)};
        }
        public static Profile NewSeedProfile(int userid)
        {
            return new Profile{ ProfileId = userid, UserId = userid, Url = NewProfileUrl(25)};
        }

        public static string NewProfileUrl(int length)
        {
            var random = new Random();
            const string pool = "abcdefghjiklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(pool, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
    }
}