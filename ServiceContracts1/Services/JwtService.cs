using System;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceContracts1.DTO;
using ServiceContracts1.ServiceContracts;

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
           var key = _configuration["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(key) || Encoding.UTF8.GetByteCount(key) < 32)
                throw new InvalidOperationException("Configure a strong JWT signing key outside source control before using token generation.");
            DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key)
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
