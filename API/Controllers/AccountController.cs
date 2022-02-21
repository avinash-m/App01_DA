using System.Security.Permissions;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

using API.Data;
using API.Entities;
using API.DTOs;
using API.Services;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController: BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
            
        }

        [HttpPost("register")]
        //public async Task<ActionResult<AppUser>> Register(string username, string password){
        //public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto){
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto){
            using var hmac = new HMACSHA512();

            if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            var user = new AppUser{
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //UserDto ud = new UserDto { Username = user.UserName, Token = _tokenService.CreateToken(user)}; 
            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
            var user = await _context. Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

             UserDto ud = new UserDto { Username = user.UserName, Token = _tokenService.CreateToken(user)}; 
            return ud;
        }

        private async Task<bool> UserExists(string username){
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}