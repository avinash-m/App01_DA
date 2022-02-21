using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using API.Interfaces;
using API.Entities;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace API.Services
{
    public class TokenService: ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user) {
            // throw new System.NotImplementedException();
            var claims = new List<Claim>{
               new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
             };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesciptor);

            return tokenHandler.WriteToken(token);
        }
    }
}