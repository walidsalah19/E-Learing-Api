using E_Learning.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_Learning.Interfaces.IServices
{
    public interface ITokenServices
    {
        Task<string> GenerateAccessToken(string userId);
        Task<RefreshToken> GenerateRefreshToken();
        Task<bool> ValidateRefreshToken(string token, string userId);
        Task<JwtSecurityToken> CreateToken(List<Claim> userClaims, ApplicationUser user);
        Task<List<Claim>> CreateCliams(ApplicationUser user);

    }
}
