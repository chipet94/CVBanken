using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using API_CVPortalen.Models.Auth;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace CVBanken.Data.Helpers.Database
{
    public class SeedData
    {
        public static void Seed(ModelBuilder _builder)
        {
            var defaultProgs = DefaultProgrammes().ToArray();
            _builder.Entity<Programme>().HasData(
                defaultProgs
            );
            _builder.Entity<User>().HasData(
                DefaultUsers(defaultProgs)
            );
        }
        private static IEnumerable<Programme> DefaultProgrammes()
        {
            return new[]
            {
                new Programme{Id=1000, Category = Programme.ProgrammeCategory.Empty,Start = DateTime.Parse("2000-01-02"), End = DateTime.Parse("2002-01-02"), Name = "Default"},
                new Programme{Id=1001, Category = Programme.ProgrammeCategory.Webbutvecklare, Start = DateTime.Parse("2000-01-02"), End = DateTime.Parse("2002-01-02"), Name = "Webb02"},
                new Programme{Id=1002, Category = Programme.ProgrammeCategory.Net_Utvecklare, Start = DateTime.Parse("2000-01-02"), End = DateTime.Parse("2002-01-02"), Name = "DotNet02"},
                new Programme{Id=1003, Category = Programme.ProgrammeCategory.JavaUtvecklare, Start = DateTime.Parse("2000-01-02"), End = DateTime.Parse("2002-01-02"), Name = "Java02"},
            };
        }
        
        private static string defaultPassword = "password123";
        private static IEnumerable<User> DefaultUsers(Programme[] programmes)
        {
            var users = new List<User>();
            
            for (int i = 1; i < 51; i++)
            {
                var user = generateUser(i, programmes);
                user.Id = i;
                users.Add(user);
            }
            CreatePasswordHash(defaultPassword, out var hash, out var salt);
            var admin = new User
            {
                Id = 99999,
                ProgrammeId = 1000,
                Description = "I am a God, aka admin",
                Email = "admin@iths.se",
                FirstName = "Admin",
                LastName = "Adminsson",
                PasswordHash = hash,
                PasswordSalt = salt,
                Private = true,
                Searching = false,
                ProfilePicture = null,
                Role = Role.Admin,
                Url = ProfileBuilder.NewProfileUrl(25)

            };
            users.Add(admin);
            return users;
        }

        private static User generateUser(int id, Programme[] programmes)
        {
            var random  = new Random();
            return UserBuilder.NewUser(
            
                $"user{id}@iths.se", defaultPassword, $"name{id}", $"Lname{id}", programmes[random.Next(1, programmes.Length)].Id,
                Role.User
            );

        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}