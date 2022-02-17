using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Domain;
using Store.Core.Database.Repositories.Base;

namespace Store.Core.Database.Repositories.RoleRepository
{
    public interface IRoleRepository : ICrudRepository<Role>
    {
        Task DisableAsync(Guid id, Guid editor, CancellationToken cts);
    }
}