using System.Data.Common;
using System.Data;
using System.Security.Claims;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.interfaces;
using API.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace API.Services
{
    public class Tokens : ITokens
    {
        private readonly SymmetricSecurityKey _key;
        public Tokens(IConfiguration config)
        {
             _key= new SymmetricSecurityKey (Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId  ,user.UserName)
            };
            var creds= new SigningCredentials(_key ,SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(7),
                SigningCredentials=creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var Token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(Token);
        }
    }
}