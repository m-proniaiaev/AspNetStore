using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Interfaces.Models;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Host.Authorization.CurrentUser;

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
        private readonly ICurrentUserService _currentUser;

        public UpdateSellerCommandHandler(ICacheService cacheService, ISellerService sellerService, ICurrentUserService currentUser)
        {
            _cacheService = cacheService;
            _sellerService = sellerService;
            _currentUser = currentUser;
        }

        public async Task<Seller> Handle(UpdateSellerCommand request, CancellationToken cancellationToken)
        {
            var cachedSeller = await _cacheService.GetCacheAsync<Seller>(request.Id.ToString(), cancellationToken);
            var seller = cachedSeller ?? await _sellerService.GetSellerAsync(request.Id, cancellationToken);
            
            if (seller == null)
                throw new ArgumentException($"Seller {request.Id} does not exist!");

            //TODO Check for existing records with different type
            var updatedSeller = new Seller 
            {
                Name = request.Name,
                RecordType = request.RecordType,
                Id = seller.Id,
                CreatedBy = seller.CreatedBy,
                Created = seller.Created,
                Edited = DateTime.Now,
                EditedBy = _currentUser.Id
            };
            
            await _sellerService.UpdateSellerAsync(updatedSeller, cancellationToken);
            
            var result = await _sellerService.GetSellerAsync(seller.Id, cancellationToken);
            
            await _cacheService.AddCacheAsync(result, TimeSpan.FromMinutes(15), cancellationToken);

            return result;
        }
    }
}