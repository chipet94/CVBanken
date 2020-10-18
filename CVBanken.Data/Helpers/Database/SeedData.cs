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
            //_builder.Entity<ProfilePicture>().HasData(DefaultProfilePictures());
            //_builder.Entity<ProfilePicture>().HasData(ProfilePictureBuilder.NewDefault());
            _builder.Entity<Profile>().HasData(DefaultProfiles());

            var defaultProgs = DefaultProgrammes().ToArray();
            _builder.Entity<Programme>().HasData(
                defaultProgs
            );
            _builder.Entity<User>().HasData(
                DefaultUsers(defaultProgs)
            );
        }

        public static IEnumerable<Profile> DefaultProfiles()
        {
            var profiles = new List<Profile>();
            for (int i = 1; i < 51; i++)
            {
                profiles.Add(ProfileBuilder.NewSeedProfile(i));
            }
            profiles.Add(new Profile{Description = "I am a god.", ProfileId = 51, Url = ProfileBuilder.NewProfileUrl(25), UserId = 99999});
            return profiles;
        }
        // public static IEnumerable<ProfilePicture> DefaultProfilePictures()
        // {
        //     var profiles = new List<ProfilePicture>();
        //     for (int i = 2; i < 52; i++)
        //     {
        //         profiles.Add(new ProfilePicture{Id = i,ProfileId = i});
        //     }
        //     profiles.Add(new ProfilePicture{Id = 52, ProfileId = 99999});
        //     return profiles;
        // }
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
                users.Add(generateUser(i));
            }
            var admin = generateAdmin(99999);
            users.Add(admin);
            foreach (var user in users)
            {
                var random  = new Random();
                user.ProgrammeId = programmes[random.Next(1, programmes.Length)].Id;
            }
            return users;
        }

        private static User generateUser(int id)
        {
            CreatePasswordHash(defaultPassword, out var hash, out var salt);
            return new User
            {
                Id = id, FirstName = $"name{id}", LastName = $"Lname{id}", Email = $"user{id}@iths.se",
                Role = Role.User, PasswordHash = hash, PasswordSalt = salt,
            };

        }
        private static User generateAdmin(int id)
        {
            CreatePasswordHash(defaultPassword, out var hash, out var salt);
            return new User
            {
                Id = id, FirstName = $"admin", LastName = $"Lname{id}", Email = $"admin@iths.se",
                Role = Role.Admin, PasswordHash = hash, PasswordSalt = salt,
            };

        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}