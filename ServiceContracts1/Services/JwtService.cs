using System;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceContracts1.DTO;
using ServiceContracts1.ServiceContracts;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace ServiceContracts1.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService (IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AuthenticationResponse CreateJwtToken(ApplicationUser user)
        {
           DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                );
            SigningCredentials signingCredentials = new SigningCredentials( securityKey, SecurityAlgorithms.HmacSha256 );

            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                expires:expiration,
                signingCredentials: signingCredentials
                );
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
           string token = tokenHandler.WriteToken(tokenGenerator);

            return new AuthenticationResponse() { Token = token, Expiration = expiration };
        }
    }
}
