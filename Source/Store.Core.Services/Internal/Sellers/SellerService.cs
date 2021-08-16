using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Models;
using Store.Core.Database.Database;
using Store.Core.Services.Common.Interfaces;
using Store.Core.Services.Internal.Sellers.Queries.CreateSeller;
using Store.Core.Services.Internal.Sellers.Queries.UpdateSellerAsync;

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

        public async Task UpdateSellerAsync(UpdateSellerCommand request, Seller origin, CancellationToken cts)
        {
            var seller = new Seller 
            {
                Name = request.Name,
                RecordType = request.RecordType,
                Id = origin.Id,
                CreatedBy = origin.CreatedBy,
                Created = origin.Created
            };
            
            await _sellers.ReplaceOneAsync(s => s.Id == seller.Id, seller, cancellationToken: cts);
        }

        public async Task<Seller> GetSellerAsync(Guid id, CancellationToken cts)
        {
            return await _sellers.Find(x => x.Id == id).FirstOrDefaultAsync(cts);
        }

        public async Task CreateSellerAsync(CreateSellerCommand request, Guid id, CancellationToken cts)
        {
            var seller = new Seller
            {
                Id = id,
                Name = request.Name,
                RecordType = request.RecordType,
                Created = DateTime.Now,
                CreatedBy = Guid.Empty //TODO
            };
            
            await _sellers.InsertOneAsync(seller, cancellationToken: cts);
        }

        public async Task DeleteSellerAsync(Guid id, CancellationToken cts)
        {
            await _sellers.DeleteOneAsync(x => x.Id == id, cts);
        }
    }
}