using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Domain;
using Store.Core.Database.Repositories.Base;

namespace Store.Core.Database.Repositories.UserRepository
{
    public interface IUserRepository : ICrudRepository<User>
    {
        Task ChangePassword(User user, CancellationToken cts);
        Task ChangeRole(User user, CancellationToken cts);
        Task MarkAsDisabledAsync(Guid id, Guid editor, CancellationToken cts);
    }
}