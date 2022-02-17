using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Database.Repositories.SellerRepository;
using Store.Core.Services.Internal.Sellers.Queries;

namespace Store.Core.Services.Internal.Sellers
{
    public class SellerService : ISellerService
    {
        private readonly ISellerRepository _sellerRepository;

        public SellerService(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
        }

        public async Task<List<Seller>> GetSellersAsync(CancellationToken cts)
        {
            var filter = new SellerFilter();
            return await _sellerRepository.FindManyAsync(filter, cts);
        }

        public async Task UpdateSellerAsync(Seller model, CancellationToken cts)
        {
            await _sellerRepository.UpdateAsync(model, cts);
        }

        public async Task<Seller> GetSellerAsync(Guid id, CancellationToken cts)
        {
            var filter = new SellerFilter()
            {
                Id = id,
                Limit = 1
            };
            return (await _sellerRepository.FindManyAsync(filter, cts)).FirstOrDefault();
        }

        public async Task CreateSellerAsync(Seller seller, CancellationToken cts)
        {
            await _sellerRepository.CreateAsync(seller, cts);
        }

        public async Task DeleteSellerAsync(Guid id, CancellationToken cts)
        {
            var filter = Builders<Seller>.Filter.Eq(s => s.Id, id);
            await _sellerRepository.DeleteAsync(filter, cts);
        }
    }
}