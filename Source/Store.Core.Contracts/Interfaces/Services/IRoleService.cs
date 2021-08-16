using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Domain;

namespace Store.Core.Contracts.Interfaces.Services
{
    public interface IRoleService
    {
        Task<List<Role>> GetRolesAsync(CancellationToken cts);
        Task<Role> GetRoleAsync(Guid id, CancellationToken cts);
        Task CreateRoleAsync(Role model, CancellationToken cts);
        Task UpdateRoleAsync(Role model, CancellationToken cts);
        Task DisableRoleAsync(Guid id, Guid editor, CancellationToken cts);
        Task DeleteRoleAsync(Guid id, CancellationToken cts);
    }
}