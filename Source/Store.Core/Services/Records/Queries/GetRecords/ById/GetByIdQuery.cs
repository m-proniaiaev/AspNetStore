using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Records.Queries.GetRecords.ById
{
    public class GetByIdQuery : IRequest<Record>, IIdentity
    {
        public Guid Id { get; set; }
    }

    public class GetRecordsQueryHandler : IRequestHandler<GetByIdQuery, Record>
    {
        private readonly IRecordService _recordService;
        private readonly ICacheService _cacheService;
        
        public GetRecordsQueryHandler(IRecordService service, ICacheService cacheService)
        {
            _recordService = service;
            _cacheService = cacheService;
        }
        public async Task<Record> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var cachedRecord = await _cacheService.GetCacheAsync<Record>(request.Id.ToString(), cancellationToken);

            if (cachedRecord != null) return cachedRecord;
            
            var result = await _recordService.GetRecord(request.Id);
            
            if (result == null)
                throw new ArgumentException($"Record {request.Id} does not exist!");

            return result;
        }
    }
}