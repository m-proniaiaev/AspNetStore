using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Common;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Records.Queries.UpdateRecord
{
    public class UpdateRecordCommand : IRequest<Record>, IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsSold { get; set; }
    }
    
    public class UpdateRecordCommandHandler : IRequestHandler<UpdateRecordCommand, Record>
    {
        private readonly IRecordService _recordService;
        private readonly ICacheService _cacheService;

        public UpdateRecordCommandHandler(IRecordService recordService)
        {
            _recordService = recordService;
        }
        
        public async Task<Record> Handle(UpdateRecordCommand request, CancellationToken cancellationToken)
        {
            var cachedRecord = await _cacheService.GetCacheAsync<Record>(request.Id.ToString(), cancellationToken);
            
            var record = cachedRecord ?? await _recordService.GetRecord(request.Id, cancellationToken);
            
            if (record == null)
                throw new ArgumentException($"Record {request.Id} is not found!");
            
            if (record.IsSold)
                throw new ArgumentException("You can not change records which already has been sold!");

            await _recordService.UpdateRecord(request, record, cancellationToken);
            
            var result = await _recordService.GetRecord(record.Id, cancellationToken);
            
            await _cacheService.AddCacheAsync(result, TimeSpan.FromMinutes(5), cancellationToken);

            return result;
        }
    }
}