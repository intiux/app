using System;
using System.Threading.Tasks;
using app.API.Models;
using Microsoft.EntityFrameworkCore;

namespace app.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username.ToLower());
            
            if(user == null)
                return null;
            
            if(!VerifyPasswordHash(password, user.PasswordHashX, user.PasswordSaltX)) 
                return null;
            
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHashX, byte[] passwordSaltX)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSaltX))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHashX[i]) return false;
                }
                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHashX, passwordSaltX;

            createPasswordHash(password, out passwordHashX, out passwordSaltX);
            user.PasswordHashX = passwordHashX;
            user.PasswordSaltX = passwordSaltX;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }

        private void createPasswordHash(string password, out byte[] passwordHashX, out byte[] passwordSaltX)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSaltX = hmac.Key;
                passwordHashX = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            } 
        }

        public async Task<bool> UserExists(string username)
        {
            if( await _context.Users.AnyAsync(u => u.UserName == username))
                return true;
                
            return false;
        }
    }
}