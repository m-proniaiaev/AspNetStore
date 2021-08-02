using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Records.Queries.GetRecords.ById
{
    public class GetRecordByIdQuery : IRequest<Record>, IIdentity
    {
        public Guid Id { get; set; }
    }

    public class GetRecordByIdQueryHandler : IRequestHandler<GetRecordByIdQuery, Record>
    {
        private readonly IRecordService _recordService;
        private readonly ICacheService _cacheService;
        
        public GetRecordByIdQueryHandler(IRecordService service, ICacheService cacheService)
        {
            _recordService = service;
            _cacheService = cacheService;
        }
        public async Task<Record> Handle(GetRecordByIdQuery request, CancellationToken cancellationToken)
        {
            var cachedRecord = await _cacheService.GetCacheAsync<Record>(request.Id.ToString(), cancellationToken);

            if (cachedRecord != null) return cachedRecord;
            
            var result = await _recordService.GetRecordAsync(request.Id, cancellationToken);
            
            if (result == null)
                throw new ArgumentException($"Record {request.Id} does not exist!");

            return result;
        }
    }
}