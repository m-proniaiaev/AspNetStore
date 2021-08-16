using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Services.Internal.Sellers.Commands.CreateSeller
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
        private readonly ICurrentUserService _currentUser;

        public CreateSellerCommandHandler(ISellerService sellerService, ICacheService cacheService, ICurrentUserService currentUser)
        {
            _sellerService = sellerService;
            _cacheService = cacheService;
            _currentUser = currentUser;
        }
        
        public async Task<Seller> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            
            var seller = new Seller
            {
                Id = id,
                Name = request.Name,
                RecordType = request.RecordType,
                Created = DateTime.Now,
                CreatedBy = _currentUser.Id
            };
            
            await _sellerService.CreateSellerAsync(seller, cancellationToken);

            var result = await _sellerService.GetSellerAsync(id, cancellationToken);
            
            await _cacheService.AddCacheAsync(result, TimeSpan.FromMinutes(15), cancellationToken);

            return result;
        }
    }
}