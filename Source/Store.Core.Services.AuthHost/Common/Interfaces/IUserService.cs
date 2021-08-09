using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.AuthHost.Common.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync(CancellationToken cts);
        Task AddUserAsync(Guid id, CancellationToken cts);
        Task<User> GetUserAsync(Guid id, CancellationToken cts);
        Task DeleteUserAsync(Guid id, CancellationToken cts);
        Task UpdateUserAsync(CancellationToken cts);
        Task ChangeUserPassword(CancellationToken cts);
        Task ChangeUserRole(CancellationToken cts);
        Task MarkUserAsDisabledAsync(Guid id, CancellationToken cts);
    }
}