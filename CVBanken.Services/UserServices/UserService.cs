using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CVBanken.Data.Helpers;
using CVBanken.Data.Models;
using CVBanken.Data.Models.Auth;
using CVBanken.Data.Models.Database;
using CVBanken.Data.Models.Requests.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CVBanken.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly Context _context;
    
        public UserService(IConfiguration config, Context context)
        {
            _config = config;
            _context = context;
        }
    
        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
    
            // return null if user not found
            if (user == null)
            {
                return null; 
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
                
            }
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = GetKey();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            await _context.SaveChangesAsync();
            return user.WithoutPassword();
        }

        public async Task<bool> ConfirmPassword(int id, string password)
        {
            var user = await _context.Users.FindAsync(id);
            CreatePasswordHash(password, out var hash, out var salt);
            return user.PasswordHash == hash && user.PasswordSalt == salt;
        }
    
    
        public async Task<IEnumerable<User>> GetAll()
        {
            return (await _context.Users.ToListAsync()).Where(u => u.Private != true);
        }
        public async Task<IEnumerable<User>> AdminGetAll()
        {
            return (await _context.Users.ToListAsync()).WithoutPasswords();
        }
    
        public async Task<User> GetById(int id)
        {
            return (await _context.Users.FindAsync(id)).WithoutPassword();
        }
        public async Task<User> GetByUrl(string url)
        {
            var user = await _context.Users.FirstAsync(u => u.Url == url);

            return user;
        }
        public async Task Create(RegisterRequest user)
        {
            // validation
            if (await _context.Users.AnyAsync(x => x.Email == user.Email))
                throw new InvalidOperationException("Email \"" + user.Email + "\" is already taken");
            try
            {
                await _context.AddAsync(user.ToUser());
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task CreateNew(RegisterRequest user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new InvalidOperationException("Password is required");
            if (user.ProgrammeId == 0 )
            {
                throw new InvalidOperationException("Programme is required");
            }

            if (await _context.Users.AnyAsync(x => x.Email == user.Email))
                throw new InvalidOperationException("Email \"" + user.Email + "\" is already taken");
            
        }

        public async Task<bool> Update(int id, UpdateUserRequest userParam)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new InvalidOperationException("User not found");


            foreach (var prop in userParam.GetType().GetProperties())
            {
                var value = prop.GetValue(userParam, null);
                Console.WriteLine(prop.Name + " - " + value);
                
                if (prop.Name != "OldPassword" && prop.GetValue(userParam, null) != null)
                {
                    var oldProp = user.GetType().GetProperty(prop.Name);
                    if (oldProp != null && value != null)
                    {
                        if (prop.Name == "Password")
                        {
                            CreatePasswordHash((string)value, out var hash, out var salt);
                            user.PasswordSalt = salt;
                            user.PasswordHash = hash;
                            
                            Console.WriteLine("Updated password for " + user.Email +" To: " + value);
                        }
                        else
                        {
                            oldProp.SetValue(user,value);
                        }
                    }
                }

            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<IEnumerable<User>> GetAllUsersInProgramme(int id)
        {
            var users = await _context.Users.Where(u => u.ProgrammeId == id && u.Private == false).ToArrayAsync();
            return users;
        }
        public async Task<IEnumerable<User>> GetAllUserInCategory(int category)
        {
            var users = await _context.Users.Where(u => (int)u.Programme.Category == category && u.Private == false).ToArrayAsync();
            return users;
        }

        //todo Krav för bilder
        public async Task UpdatePicture(int id, IFormFile picture)
        {
            var user = await _context.Users.FindAsync(id);
            if (user.ProfilePicture == null)
            {
                user.ProfilePicture = new ProfilePicture();
                user.ProfilePicture.User = user;
                user.ProfilePicture.UserId = user.Id;
                user.ProfilePicture.ImageTitle = "Profile Picture";
            }

            await using (var dataSource = new MemoryStream())
            {
                await picture.CopyToAsync(dataSource);
                user.ProfilePicture.ImageData = dataSource.ToArray();
            }

            _context.Update(user);
            await _context.SaveChangesAsync();
        }
        // helper methods
        public async Task<string> GenerateJwtToken(User user)
        {
            var token = await Task.Run(() => generateJwtToken(user));
            return token;
        }
        
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = GetKey();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public byte[] GetKey()
        {
            return Encoding.ASCII.GetBytes(_config["Jwt:Secret"]);
        }
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}