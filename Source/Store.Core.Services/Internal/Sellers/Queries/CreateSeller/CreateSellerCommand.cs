using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Services.Common.Interfaces;

namespace Store.Core.Services.Internal.Sellers.Queries.CreateSeller
{
    public class CreateSellerCommand : IRequest<Seller>
    {
        public string Name { get; set; }
        public RecordType[] RecordType { get; set; }
    }
    
    public class CreateSellerCommandHandler : IRequestHandler<CreateSellerCommand, Seller>
    {
        private readonly ISellerService _sellerService;
        private readonly ICacheService _cacheService;

        public CreateSellerCommandHandler(ISellerService sellerService, ICacheService cacheService)
        {
            _sellerService = sellerService;
            _cacheService = cacheService;
        }
        
        public async Task<Seller> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            await _sellerService.CreateSellerAsync(request, id, cancellationToken);

            var result = await _sellerService.GetSellerAsync(id, cancellationToken);
            
            await _cacheService.AddCacheAsync(result, TimeSpan.FromMinutes(15), cancellationToken);

            return result;
        }
    }
}