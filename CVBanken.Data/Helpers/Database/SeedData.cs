using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace CVBanken.Data.Helpers.Database
{
    public class SeedData
    {
        private static readonly string defaultPassword = "password123";

        private static readonly string[] FirstNames =
        {
            "Anders", "Göran", "Adolf", "Simon", "Sanne", "Bodil", "Ove", "Olivia", "Emelie", "Rebacca",
            "Majvor", "Håkan", "Klas", "Anna", "Linnea", "Daniel", "Martin", "Annie", "Aaron", "Hanna",
            "Donald", "Joe", "Hillary", "John", "Jeffrey", "Bill"
        };

        private static readonly string[] LastNames =
        {
            "Epstein", "Trump", "Biden", "Clinton", "Podesta"
        };

        public static void Seed(ModelBuilder _builder)
        {
            var defaultCategories = DefaultCategories().ToArray();
            _builder.Entity<Category>().HasData(defaultCategories);
            var defaultProgs = DefaultProgrammes(defaultCategories).ToArray();
            _builder.Entity<Programme>().HasData(
                defaultProgs
            );
            _builder.Entity<User>().HasData(
                DefaultUsers(defaultProgs)
            );
        }

        private static IEnumerable<Category> DefaultCategories()
        {
            return new[]
            {
                new Category {Id = 1, Name = "Default"},
                new Category {Id = 2, Name = "Webbutvecklare"},
                new Category {Id = 3, Name = "Apputvecklare"},
                new Category {Id = 4, Name = "Javautvecklare"},
                new Category {Id = 5, Name = "Dotnetutvecklare"},
                new Category {Id = 6, Name = "Mjukvarutestare"},
                new Category {Id = 7, Name = "Frontendutvecklare"},
                new Category {Id = 8, Name = "IT-Projektledare"},
                new Category {Id = 9, Name = "JavaScriptutvecklare"},
                new Category {Id = 10, Name = "UX-designer"}
            };
        }

        private static IEnumerable<Programme> DefaultProgrammes(Category[] categories)
        {
            var programmes = new List<Programme>();
            var count = 1;
            for (var i = 2010; i < 2015; i++)
            {
                foreach (var category in categories)
                {
                    var start = new DateTime(i, 01, 23);
                    if (category.Id != 1)
                        programmes.Add(new Programme
                        {
                            Id = category.Id * 1000 + count,
                            Start = start,
                            End = start.AddYears(2),
                            Name = category.Name == "JavaScript utvecklare"
                                ? "Js" + start.Year
                                : category.Name.Substring(0, 4) + start.Year,
                            // Category = category,
                            CategoryId = category.Id
                        });
                }

                count++;
            }

            programmes.Add(new Programme
            {
                Id = 1000,
                Start = DateTime.Now,
                End = DateTime.Now.AddYears(2),
                Name = "Default",
                // Category = category,
                CategoryId = 1
            });

            return programmes;
        }

        private static IEnumerable<User> DefaultUsers(Programme[] programmes)
        {
            var users = new List<User>();

            for (var i = 1; i < 400; i++)
            {
                var user = generateUser(i, programmes.Where(p => p.Name != "Default").ToArray());
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
            foreach (var user in users)
            {
                var random = new Random();
                user.ProgrammeId = programmes[random.Next(1, programmes.Length)].Id;
            }

            return users;
        }

        private static User generateUser(int id, Programme[] programmes)
        {
            var random = new Random();
            return UserBuilder.NewUser(
                $"user{id}@iths.se", defaultPassword,
                FirstNames[random.Next(0, FirstNames.Length)],
                LastNames[random.Next(0, LastNames.Length)],
                programmes[random.Next(2, programmes.Length)].Id,
                Role.User
            );
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}