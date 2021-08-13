using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Common.Interfaces
{
    public interface IBlackListService
    {
        Task AddToBlackList(BlackListRecord record, CancellationToken cancellationToken);
        Task RemoveFromBlackList(BlackListRecord record, CancellationToken cancellationToken);
        Task<BlackListRecord> FindBlackList(Guid id, CancellationToken cancellationToken);
    }
}