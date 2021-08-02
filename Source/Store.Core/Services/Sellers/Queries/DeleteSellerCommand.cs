using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Sellers.Queries
{
    public class DeleteSellerCommand : IRequest, IIdentity
    { 
        public Guid Id { get; set; }
    }
    
    public class DeleteSellerCommandHandler : IRequestHandler<DeleteSellerCommand>
    {
        private readonly ICacheService _cacheService;
        private readonly ISellerService _sellerService;

        public DeleteSellerCommandHandler(ICacheService cacheService, ISellerService sellerService)
        {
            _cacheService = cacheService;
            _sellerService = sellerService;
        }
        
        public async Task<Unit> Handle(DeleteSellerCommand request, CancellationToken cancellationToken)
        {
            var cachedSeller = await _cacheService.GetCacheAsync<Seller>(request.Id.ToString(), cancellationToken);
            var seller = cachedSeller ?? await _sellerService.GetSellerAsync(request.Id, cancellationToken);

            if (seller == null)
                throw new ArgumentException($"Seller {request.Id} does not exist!");
            
            await _sellerService.DeleteSellerAsync(seller.Id, cancellationToken);
            await _cacheService.DeleteCacheAsync<Seller>(seller.Id.ToString(), cancellationToken);
            
            return Unit.Value;
        }
    }
}