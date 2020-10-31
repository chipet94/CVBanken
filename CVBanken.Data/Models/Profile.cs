using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CVBanken.Data.Models.Auth;

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