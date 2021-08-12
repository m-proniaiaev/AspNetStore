using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Services.Common.Interfaces;

namespace Store.Core.Services.Internal.Sellers.Queries.UpdateSellerAsync
{
    public class UpdateSellerCommand : IRequest<Seller>, IIdentity
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RecordType[] RecordType { get; set; }
    }
    
    public class UpdateSellerCommandHandler : IRequestHandler<UpdateSellerCommand, Seller>
    {
        private readonly ICacheService _cacheService;
        private readonly ISellerService _sellerService;

        public UpdateSellerCommandHandler(ICacheService cacheService, ISellerService sellerService)
        {
            _cacheService = cacheService;
            _sellerService = sellerService;
        }

        public async Task<Seller> Handle(UpdateSellerCommand request, CancellationToken cancellationToken)
        {
            var cachedSeller = await _cacheService.GetCacheAsync<Seller>(request.Id.ToString(), cancellationToken);
            var seller = cachedSeller ?? await _sellerService.GetSellerAsync(request.Id, cancellationToken);
            
            if (seller == null)
                throw new ArgumentException($"Seller {request.Id} does not exist!");

            //TODO Check for existing records with different type
            
            await _sellerService.UpdateSellerAsync(request, seller, cancellationToken);
            
            var result = await _sellerService.GetSellerAsync(seller.Id, cancellationToken);
            
            await _cacheService.AddCacheAsync(result, TimeSpan.FromMinutes(15), cancellationToken);

            return result;
        }
    }
}