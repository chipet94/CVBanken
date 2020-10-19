using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CVBanken.Services.UserServices
{
    public class ProfileService : IProfileService
    {
        private readonly Context _context;
        public ProfileService(Context context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Profile>> GetAll()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<Profile> GetById(int id)
        {
            return await _context.Profiles.FindAsync(id);
        }

        public async Task<Profile> GetByUser(int userId)
        {
            return await _context.Profiles.FirstAsync(p => p.UserId == userId);
        }

        public async Task<Profile> GetByUrl(string url)
        {
            return await _context.Profiles.FirstAsync(p => p.Url == url);
        }

        public async Task Update(int id, UpdateProfileRequest newProfile)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                throw new Exception("Profile not found.");
            }
            try
            {
                if (newProfile.User != null)
                {
                    await UpdateProfileUser(profile.UserId, newProfile.User);
                }
                if (!string.IsNullOrEmpty(newProfile.Description))
                {
                    profile.Description = newProfile.Description;
                }

                if (newProfile.Private != profile.Private)
                {
                    profile.Private = newProfile.Private;
                }

                if (newProfile.Searching != profile.Searching)
                {
                    profile.Searching = newProfile.Searching;
                }

                if (newProfile.ProfilePicture != null)
                {
                    await UpdatePicture(profile, newProfile.ProfilePicture);
                }

                _context.Profiles.Update(profile);
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private async Task UpdateProfileUser(int id, UpdateUser userParam)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new InvalidOperationException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Email) && userParam.Email != user.Email)
            {
                // throw error if the new username is already taken
                if (_context.Users.Any(x => x.Email == userParam.Email))
                    throw new InvalidOperationException("Email " + userParam.Email + " is already taken");

                user.Email = userParam.Email;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;
            if (userParam.ProgrammeId != user.ProgrammeId)
                user.ProgrammeId = userParam.ProgrammeId;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(userParam.Password))
            {
                UserService.CreatePasswordHash(userParam.Password, out var passwordHash, out var passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdatePicture( Profile profile, IFormFile picture)
        {
            if (profile.ProfilePicture == null)
            {
                profile.ProfilePicture = new ProfilePicture();
                profile.ProfilePicture.Profile = profile;
                profile.ProfilePicture.ProfileId = profile.ProfileId;
                profile.ProfilePicture.ImageTitle = "Profile Picture";
            }

            await using (var dataSource = new MemoryStream())
            {
                await picture.CopyToAsync(dataSource);
                profile.ProfilePicture.ImageData = dataSource.ToArray();
            }
        }
        public async Task UpdatePicture( int id, IFormFile picture)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                throw new Exception("No Profile found.");
            }
            if (profile.ProfilePicture == null)
            {
                profile.ProfilePicture = new ProfilePicture();
                profile.ProfilePicture.Profile = profile;
                profile.ProfilePicture.ProfileId = profile.ProfileId;
                profile.ProfilePicture.ImageTitle = "Profile Picture";
            }

            await using (var dataSource = new MemoryStream())
            {
                picture.CopyTo(dataSource);
                profile.ProfilePicture.ImageData = dataSource.ToArray();
            }
            try
            {
                _context.Profiles.Update(profile);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}