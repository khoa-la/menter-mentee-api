using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace _2MC.BusinessTier.Commons
{
    public class AccessTokenManager
    {
        public static string GenerateJwtToken(string name, string[] roles, int? userId,
            IConfiguration configuration)
        {
            var tokenConfig = configuration.GetSection("Token");
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            if (roles != null && roles.Length > 0)
            {
                foreach (string role in roles)
                {
                    permClaims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(permClaims),
                Expires = DateTime.Now.AddDays(7.0),
                SigningCredentials = credentials
            };
            var token = jwtSecurityTokenHandler.CreateToken(tokenDescription);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}