using System.Net;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.DTOs;
using API.Entities;
using API.interfaces;


namespace API.Controllers
{
    public class AccountControllers : BaseAPIController
    {
        private readonly ITokens _Token;
        private readonly DataContext _Context;

        
        public AccountControllers( DataContext Context, ITokens Token)
        {
            _Token = Token;
            _Context = Context;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDtos>> Register(RegisterDtos registerDtos)
        {
            if(await UserExists (registerDtos.Username)) return BadRequest("Username is Already Taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDtos.Username.ToLower(),
                PasswordHash= hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDtos.Password)),
                PasswordSalt= hmac.Key

            };
            _Context.Users.Add(user);
            await _Context.SaveChangesAsync();

            return new UserDtos
            {
               Username = user.UserName,
               Token =_Token.CreateToken(user)
            };
            
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDtos>> login(LoginDtos loginDtos)
        {
            var user = await _Context.Users.SingleOrDefaultAsync(x=>x.UserName==loginDtos.Username);
            if(user== null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
           var ComputedHash= hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDtos.Password));
             for (int i=0 ;i<ComputedHash.Length;i++)
             {
                 if(ComputedHash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid password");
             }
             //return user
             return new UserDtos
            {
               Username = user.UserName,
               Token = _Token.CreateToken(user)
            };

        }



        private async Task<bool> UserExists(string Username)
        {
            return await _Context.Users.AnyAsync(x=> x.UserName == Username.ToLower());
        }
    }
}