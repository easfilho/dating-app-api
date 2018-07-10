using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext dataContext;

        public AuthRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;

        }
        public async Task<User> Login(string username, string password)
        {
            var user = await this.dataContext.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (UserNullOrInvalidPassword(password, user))
            {
                return null;
            }

            return user;
        }

        private bool UserNullOrInvalidPassword(string password, User user)
        {
            return user == null || !VerifyPassword(password, user);
        }

        private bool VerifyPassword(string password, User user)
        {
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != user.PasswordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public async Task<User> Resgister(string username, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User(username, passwordHash, passwordSalt);
            await this.dataContext.AddAsync(user);
            await this.dataContext.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            return await this.dataContext.Users.AnyAsync(x => x.Username == username);
        }
    }
}