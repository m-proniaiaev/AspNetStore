using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Models;

namespace Store.Core.Common.Interfaces
{
    public interface ISellerService
    {
        public Task<List<Seller>> GetSellers(CancellationToken cts);
        Task<Seller> GetSeller(Guid id, CancellationToken cts);
        Task DeleteSeller(Guid id, CancellationToken cts);
    }
}