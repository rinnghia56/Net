using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data.Entities;
using DatingApp.Api.DTO;
using DatingApp.API.Data;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.APi.Controllers
{

    public class AuthController : BaseController
    {
        public DataContext _context;

        public AuthController(DataContext context)
        {
            _context = context;
        }
        [HttpPost("register")]
        public IActionResult Post([FromBody] AuthUserDto authUserDto)
        {
            authUserDto.Username = authUserDto.Username.ToLower();
            if (_context.AppUsers.Any(user => user.username == authUserDto.Username))
            {
                return BadRequest("this username is already taken");
            }

            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(authUserDto.Password);
            var user = new User
            {
                username = authUserDto.Username,
                PasswordHash = hmac.ComputeHash(passwordBytes),
                PasswordSalt = hmac.Key
            };
            _context.AppUsers.Add(user);
            _context.SaveChanges();
            return Ok(user.username);
        }



        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthUserDto authUserDto)
        {
            authUserDto.Username = authUserDto.Username.ToLower();

            var curentUser = _context.AppUsers.FirstOrDefault(u => u.username == authUserDto.Username);
            if (curentUser == null)
            {
                return Unauthorized("username already  in use");

            }
            using var hmac = new HMACSHA512(curentUser.PasswordSalt);
            var passwordBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(authUserDto.Password));
            for (int i = 0; i < curentUser.PasswordHash.Length; i++)
            {
                if (curentUser.PasswordHash[i] != passwordBytes[i])
                {
                    return Unauthorized("password is invalid");
                }
            }
            return Ok();
        }

    }
}