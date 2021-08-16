using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces.Models;
using Store.Core.Contracts.Interfaces.Services;

namespace Store.Core.Services.Internal.Records.Commands.DeleteRecord
{
    public class DeleteRecordCommand : IRequest, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class DeleteRecordCommandHandler : IRequestHandler<DeleteRecordCommand>
    {
        private readonly IRecordService _recordService;
        private readonly ICacheService _cacheService;

        public DeleteRecordCommandHandler(IRecordService recordService, ICacheService cacheService)
        {
            _recordService = recordService;
            _cacheService = cacheService;
        }
        public async Task<Unit> Handle(DeleteRecordCommand request, CancellationToken cancellationToken)
        {
            var cachedRecord = await _cacheService.GetCacheAsync<Record>(request.Id.ToString(), cancellationToken);
            var record = cachedRecord ?? await _recordService.GetRecordAsync(request.Id, cancellationToken);

            if (record == null)
                throw new ArgumentException($"Record {request.Id} is not found!");
            
            await _recordService.DeleteRecordAsync(record.Id, cancellationToken);
            await _cacheService.DeleteCacheAsync<Record>(record.Id.ToString(), cancellationToken);
            
            return Unit.Value;
        }
    }
}