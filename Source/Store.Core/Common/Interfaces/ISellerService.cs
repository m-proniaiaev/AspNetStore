using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Models;
using Store.Core.Services.Sellers.Queries.CreateSeller;

namespace Store.Core.Common.Interfaces
{
    public interface ISellerService
    {
        public Task<List<Seller>> GetSellersAsync(CancellationToken cts);
        public Task<Seller> GetSellerAsync(Guid id, CancellationToken cts);
        public Task CreateSellerAsync(CreateSellerCommand request, Guid id, CancellationToken cts);
        public Task DeleteSellerAsync(Guid id, CancellationToken cts);
    }
}