using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Common;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Records.Queries.DeleteRecord
{
    public class DeleteCommand : IRequest, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class DeleteRecordCommandHandler : IRequestHandler<DeleteCommand>
    {
        private readonly IRecordService _recordService;
        private readonly ICacheService _cacheService;

        public DeleteRecordCommandHandler(IRecordService recordService, ICacheService cacheService)
        {
            _recordService = recordService;
            _cacheService = cacheService;
        }
        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var cachedRecord = await _cacheService.GetCacheAsync<Record>(request.Id.ToString(), cancellationToken);
            var record = cachedRecord ?? await _recordService.GetRecord(request.Id, cancellationToken);

            if (record == null)
                throw new ArgumentException($"Record {request.Id} is not found!");
            
            await _recordService.DeleteRecord(record.Id, cancellationToken);
            await _cacheService.DeleteCacheAsync<Record>(record.Id.ToString(), cancellationToken);
            
            return Unit.Value;
        }
    }
}