using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.entities;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly AppDbContext _context;
        private readonly API.Interfaces.ITokenService _tokenService;

        public AccountController(AppDbContext context, API.Interfaces.ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var normalizedEmail = dto.Email?.Trim().ToLower() ?? string.Empty;
            if (await IsEmailTaken(normalizedEmail)) return BadRequest("Email is already taken");

            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                DisplayName = dto.DisplayName,
                Email = normalizedEmail,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { user.Id, user.DisplayName, user.Email });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var normalizedEmail = dto.Email?.Trim().ToLower() ?? string.Empty;
            var user = await _context.AppUsers.SingleOrDefaultAsync(u => u.Email == normalizedEmail);
            if (user == null) return Unauthorized("Invalid credentials");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            if (!computedHash.SequenceEqual(user.PasswordHash)) return Unauthorized("Invalid credentials");

            var token = _tokenService.CreateToken(user);
            var result = new API.DTOs.UserDto
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = token
            };

            return result;
        }

        private Task<bool> IsEmailTaken(string? email) =>
            string.IsNullOrWhiteSpace(email)
                ? Task.FromResult(false)
                : _context.AppUsers.AnyAsync(u => u.Email == email!.Trim().ToLower());
    }
}
