using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Domain;

namespace Store.Core.Contracts.Interfaces.Services
{
    public interface IBlackListService
    {
        Task AddToBlackList(Guid id, CancellationToken cancellationToken);
        Task RemoveFromBlackList(Guid id, CancellationToken cancellationToken);
        Task<BlackListRecord> FindBlackList(Guid id, CancellationToken cancellationToken);
    }
}