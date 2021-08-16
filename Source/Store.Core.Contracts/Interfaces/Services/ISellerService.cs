using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Domain;

namespace Store.Core.Contracts.Interfaces.Services
{
    public interface ISellerService
    {
        Task<List<Seller>> GetSellersAsync(CancellationToken cts);
        Task UpdateSellerAsync(Seller model, CancellationToken cts);
        Task<Seller> GetSellerAsync(Guid id, CancellationToken cts);
        Task CreateSellerAsync(Seller seller, CancellationToken cts);
        Task DeleteSellerAsync(Guid id, CancellationToken cts);
    }
}