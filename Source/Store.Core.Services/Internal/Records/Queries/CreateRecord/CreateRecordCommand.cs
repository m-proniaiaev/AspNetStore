using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Host.Authorization.CurrentUser;
using Store.Core.Services.Internal.Sellers.Queries.GetSellers;

namespace Store.Core.Services.Internal.Records.Queries.CreateRecord
{
    public class CreateRecordCommand : IRequest<Record>
    {
        public string Name { get; set; }
        public string Seller { get; set; }
        public decimal Price { get; set; }
        public RecordType RecordType { get; set; }
    }
    
    public class CreateRecordCommandHandler : IRequestHandler<CreateRecordCommand, Record>
    {
        private readonly IRecordService _recordService;
        private readonly ICacheService _cacheService;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUser;

        public CreateRecordCommandHandler(IRecordService recordService, ICacheService cacheService, IMediator mediator, ICurrentUserService currentUser)
        {
            _recordService = recordService;
            _cacheService = cacheService;
            _mediator = mediator;
            _currentUser = currentUser;
        }
        
        public async Task<Record> Handle(CreateRecordCommand request, CancellationToken cancellationToken)
        {
            var seller = ( await _mediator.Send(new GetSellersQuery { Name = request.Seller }, cancellationToken) )
                .Sellers
                .FirstOrDefault();

            if (seller == null)
                throw new ArgumentException("There is no such seller!");

            bool recordTypeIsValid = seller.RecordType.Contains(request.RecordType);

            if (!recordTypeIsValid)
                throw new ArgumentException($"This seller can't have record with type {request.RecordType}");
                
            var id = Guid.NewGuid();
            var record = new Record
            {
                Id = id,
                Seller = request.Seller,
                Created = DateTime.Now,
                CreatedBy = _currentUser.Id,
                Name = request.Name,
                Price = request.Price,
                RecordType = request.RecordType,
                IsSold = false,
                SoldDate = null
            };
            await _recordService.AddRecordAsync(record, cancellationToken);
                
            var result = await _recordService.GetRecordAsync(id, cancellationToken);
            
            await _cacheService.AddCacheAsync(result, TimeSpan.FromMinutes(5), cancellationToken);
            
            return result;
        }
    }
}