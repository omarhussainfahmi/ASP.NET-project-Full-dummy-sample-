using System;
using ServiceContracts1.DTO;


namespace ServiceContracts1.ServiceContracts
{
    public interface  IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
    }
}
