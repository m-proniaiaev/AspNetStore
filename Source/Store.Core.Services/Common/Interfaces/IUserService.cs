using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Common.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync(CancellationToken cts);
        Task AddUserAsync(User user, CancellationToken cts);
        Task<User> GetUserAsync(Guid id, CancellationToken cts);
        Task DeleteUserAsync(Guid id, CancellationToken cts);
        Task UpdateUserAsync(User user, CancellationToken cts);
        Task ChangeUserPassword(User user, CancellationToken cts);
        Task ChangeUserRole(User user, CancellationToken cts);
        Task MarkUserAsDisabledAsync(Guid id, Guid editor, CancellationToken cts);
    }
}