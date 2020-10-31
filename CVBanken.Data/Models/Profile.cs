using System;
using System.Linq;

namespace CVBanken.Data.Models
{
    public static class ProfileBuilder
    {
        public static string NewProfileUrl(int length)
        {
            var random = new Random();
            const string pool = "abcdefghjiklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(pool, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}