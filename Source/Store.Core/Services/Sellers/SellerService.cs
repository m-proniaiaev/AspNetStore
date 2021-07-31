using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Database.Database;

namespace Store.Core.Services.Sellers
{
    public class SellerService : ISellerService
    {
        private readonly IMongoCollection<Seller> _sellers;

        public SellerService(IDbClient client)
        {
            _sellers = client.GetSellersCollection();
        }
        
        public async Task<List<Seller>> GetSellers(CancellationToken cts)
        {
            return await _sellers.Find(x => true).ToListAsync(cts);
        }

        public async Task<Seller> GetSeller(Guid id, CancellationToken cts)
        {
            return await _sellers.Find(x => x.Id == id).FirstOrDefaultAsync(cts);
        }

        public async Task DeleteSeller(Guid id, CancellationToken cts)
        {
            await _sellers.DeleteOneAsync(x => x.Id == id, cts);
        }
    }
}