using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Models;
using Store.Core.Services.Sellers.Queries.CreateSeller;
using Store.Core.Services.Sellers.Queries.UpdateSellerAsync;

namespace Store.Core.Common.Interfaces
{
    public interface ISellerService
    {
        Task<List<Seller>> GetSellersAsync(CancellationToken cts);
        Task UpdateSellerAsync(UpdateSellerCommand command, Seller origin, CancellationToken cts);
        Task<Seller> GetSellerAsync(Guid id, CancellationToken cts);
        Task CreateSellerAsync(CreateSellerCommand request, Guid id, CancellationToken cts);
        Task DeleteSellerAsync(Guid id, CancellationToken cts);
    }
}