using Microsoft.Extensions.Configuration;
using ServiceContracts1.ServiceContracts;
using ServiceContracts1.Services;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;

namespace CRUDTests;
public class SecurityTests
{
    [Fact]
    public void TokenGenerationRejectsMissingSigningKey()
    {
        var service = new JwtService(new ConfigurationBuilder().Build());
        Assert.Throws<InvalidOperationException>(() => service.CreateJwtToken(new ApplicationUser()));
    }

    [Fact]
    public void TokenGenerationUsesConfiguredKeyAndExpiration()
    {
        var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> {
            ["Jwt:Key"] = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)),
            ["Jwt:Issuer"] = "https://issuer.example",
            ["Jwt:Audience"] = "https://audience.example",
            ["Jwt:EXPIRATION_MINUTES"] = "10"
        }).Build();
        var result = new JwtService(config).CreateJwtToken(new ApplicationUser());
        var token = new JwtSecurityTokenHandler().ReadJwtToken(result.Token);
        Assert.Equal("https://issuer.example", token.Issuer);
        Assert.Contains("https://audience.example", token.Audiences);
        Assert.InRange(result.Expiration, DateTime.UtcNow.AddMinutes(9), DateTime.UtcNow.AddMinutes(11));
    }
}
