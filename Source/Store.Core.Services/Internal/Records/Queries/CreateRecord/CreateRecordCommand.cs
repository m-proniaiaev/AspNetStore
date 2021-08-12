using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Internal.Sellers.Queries.GetSellers;

namespace Store.Core.Internal.Records.Queries.CreateRecord
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

        public CreateRecordCommandHandler(IRecordService recordService, ICacheService cacheService, IMediator mediator)
        {
            _recordService = recordService;
            _cacheService = cacheService;
            _mediator = mediator;
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
            await _recordService.AddRecordAsync(request, id, cancellationToken);
                
            var result = await _recordService.GetRecordAsync(id, cancellationToken);
            
            await _cacheService.AddCacheAsync(result, TimeSpan.FromMinutes(5), cancellationToken);
            
            return result;
        }
    }
}