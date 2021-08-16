using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Host.Authorization.JWT
{
    public class AuthManager : IAuthManager
    {
        private readonly JwtConfig _config;

        public AuthManager(IOptions<JwtConfig> config)
        {
            _config = config.Value;
        }

        public string GenerateToken(Claim[] claims)
        {
            var token = new JwtSecurityToken(
                _config.Issuer,
                "",
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_config.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.Secret)), SecurityAlgorithms.HmacSha256Signature)
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return accessToken;
        }
        
    }
}