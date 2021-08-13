using System;
using System.Security.Claims;

namespace Store.Core.Host.Authorization.JWT
{
    public interface IAuthManager
    {
        string GenerateToken(Claim[] claims);
    }
}