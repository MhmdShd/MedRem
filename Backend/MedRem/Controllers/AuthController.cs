using MedRem.Models;
using MedRem.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace MedRem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly MedRemDbContext _context;

        public AuthController(MedRemDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User userDto)
        {
            if (await UserExists(userDto.Username))
                return BadRequest("Username is already taken");

            using var hmac = new HMACSHA512();

            var user = new User
            {
                Username = userDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(User loginDto)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Username == loginDto.Username.ToLower());

            if (user == null)
                return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid password");
            }

            // Assume you have a method to create a token
            string token = CreateToken(user);

            return new User
            {
                Username = user.Username,
                Token = token
            };
        }

        private string CreateToken(User user)
        {
            // Token creation logic goes here
            // You need to implement this according to your token provider or JWT settings.
            return "testToken";
        }

    }
}