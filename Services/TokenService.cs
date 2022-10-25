using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dotnetRoutes.Models;
using Microsoft.IdentityModel.Tokens;

namespace dotnetRoutes.Services
{
    public class TokenService
    {        
        public static string GenerateToken(string password)
        {
            var creationDate = DateTime.Now;
            var expirationDate = DateTime.Now.AddHours(3);
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var key = Encoding.ASCII.GetBytes(password); //Encoding.ASCII.GetBytes("ysa868gOND7rwkHG5Z^a2rEyoi&");


            var tokenDescriptor = new SecurityTokenDescriptor
            {                
                Issuer = "http://localhost:5043",
                Audience = "http://localhost:5043",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),                
                NotBefore = creationDate,
                Expires = expirationDate,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, "Admin")
                }),

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
