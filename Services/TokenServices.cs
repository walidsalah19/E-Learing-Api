using E_Learning.Interfaces.IServices;
using E_Learning.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace E_Learning.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration config;
        private readonly UserManager<ApplicationUser> userManager;

        public TokenServices(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            this.config = config;
            this.userManager = userManager;
        }

        public Task<string> GenerateAccessToken(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<RefreshToken> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    ExpireOn = DateTime.UtcNow.AddDays(1),
                    CreateOn=DateTime.UtcNow,
                };
            }
        }

        public Task<bool> ValidateRefreshToken(string token, string userId)
        {
            throw new NotImplementedException();
        }
        public async Task<JwtSecurityToken> CreateToken(List<Claim> userClaims,ApplicationUser user)
        {
            var symmetric = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecritKey"]));
            SigningCredentials signing = new SigningCredentials(symmetric, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurity = new JwtSecurityToken(
                 audience: config["JWT:AudienceIP"],
                 issuer: config["JWT:IssuerIP"],
                 expires: DateTime.Now.AddDays(7),
                 claims: userClaims,
                 signingCredentials: signing
             );
            //if(user.RefreshTokens.Any(x=>x.IsActive))
            //{
            //    var activeToken = user.RefreshTokens.FirstOrDefault(x => x.IsActive);

            //}
            return jwtSecurity;
        }
        public async Task<List<Claim>> CreateCliams(ApplicationUser user)
        {
            List<Claim> userClaims = new List<Claim>();
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            userClaims.Add(new Claim(ClaimTypes.Email, user.Email));
            userClaims.Add(new Claim(ClaimTypes.Name, user.UserName));
            userClaims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
            var role = await userManager.GetRolesAsync(user);
            foreach (var item in role)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, item));
            }
            return userClaims;
        }
    }
}
