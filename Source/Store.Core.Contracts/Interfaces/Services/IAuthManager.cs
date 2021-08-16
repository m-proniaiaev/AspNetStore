using System.Security.Claims;

namespace Store.Core.Contracts.Interfaces.Services
{
    public interface IAuthManager
    {
        string GenerateToken(Claim[] claims);
    }
}