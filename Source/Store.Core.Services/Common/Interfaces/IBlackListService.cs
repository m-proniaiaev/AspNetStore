using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Models;
using Store.Core.Services.Authorization.BlackList.Commands;

namespace Store.Core.Services.Common.Interfaces
{
    public interface IBlackListService
    {
        Task AddToBlackList(AddToBlackListCommand command, CancellationToken cancellationToken);
        Task RemoveFromBlackList(RemoveFromBlackListCommand command, CancellationToken cancellationToken);
        Task<BlackListRecord> FindBlackList(Guid id, CancellationToken cancellationToken);
    }
}