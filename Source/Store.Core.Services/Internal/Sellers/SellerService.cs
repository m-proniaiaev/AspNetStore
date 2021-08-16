using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Database.Database;

namespace Store.Core.Services.Internal.Sellers
{
    public class SellerService : ISellerService
    {
        private readonly IMongoCollection<Seller> _sellers;

        public SellerService(IDbClient client)
        {
            _sellers = client.GetSellersCollection();
        }
        
        public async Task<List<Seller>> GetSellersAsync(CancellationToken cts)
        {
            return await _sellers.Find(x => true).ToListAsync(cts);
        }

        public async Task UpdateSellerAsync(Seller model, CancellationToken cts)
        {
            await _sellers.ReplaceOneAsync(s => s.Id == model.Id, model, cancellationToken: cts);
        }

        public async Task<Seller> GetSellerAsync(Guid id, CancellationToken cts)
        {
            return await _sellers.Find(x => x.Id == id).FirstOrDefaultAsync(cts);
        }

        public async Task CreateSellerAsync(Seller seller, CancellationToken cts)
        {
            await _sellers.InsertOneAsync(seller, cancellationToken: cts);
        }

        public async Task DeleteSellerAsync(Guid id, CancellationToken cts)
        {
            await _sellers.DeleteOneAsync(x => x.Id == id, cts);
        }
    }
}