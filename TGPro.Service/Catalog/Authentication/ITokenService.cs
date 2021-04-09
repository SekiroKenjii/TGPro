using System.Collections.Generic;
using System.Security.Claims;

namespace TGPro.Service.Catalog.Authentication
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
