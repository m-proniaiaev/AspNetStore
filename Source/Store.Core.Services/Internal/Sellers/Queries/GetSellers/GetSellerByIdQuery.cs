using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Interfaces.Models;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Services.Internal.Sellers.Queries.GetSellers
{
    public class GetSellerByIdQuery : IRequest<Seller>, IIdentity
    {
        public Guid Id { get; set; }
    }

    public class GetSellerByIdQueryHandler : IRequestHandler<GetSellerByIdQuery, Seller>
    {
        private readonly ICacheService _cacheService;
        private readonly ISellerService _sellerService;

        public GetSellerByIdQueryHandler(ICacheService cacheService, ISellerService sellerService)
        {
            _cacheService = cacheService;
            _sellerService = sellerService;
        }
        
        public async Task<Seller> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
        {
            var cachedSeller = await _cacheService.GetCacheAsync<Seller>(request.Id.ToString(), cancellationToken);
            
            if (cachedSeller != null) return cachedSeller;
            
            var result = await _sellerService.GetSellerAsync(request.Id, cancellationToken);

            if (result == null)
                throw new ArgumentException($"Seller with Id: {request.Id} does not exist!");

            return result;
        }
    }
}