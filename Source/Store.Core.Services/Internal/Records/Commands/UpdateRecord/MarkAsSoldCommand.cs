using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces.Models;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Host.Authorization.CurrentUser;

namespace Store.Core.Services.Internal.Records.Commands.UpdateRecord
{
    public class MarkAsSoldCommand : IRequest, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class RecordMarkAsSoldCommandHandler : IRequestHandler<MarkAsSoldCommand>
    {
        private readonly IRecordService _recordService;
        private readonly ICacheService _cacheService;
        private readonly ICurrentUserService _currentUser;

        public RecordMarkAsSoldCommandHandler(IRecordService recordService, ICacheService cacheService, ICurrentUserService currentUser)
        {
            _recordService = recordService;
            _cacheService = cacheService;
            _currentUser = currentUser;
        }
        public async Task<Unit> Handle(MarkAsSoldCommand request, CancellationToken cancellationToken)
        {
            var cacheRecord = await _cacheService.GetCacheAsync<Record>(request.Id.ToString(), cancellationToken);
            
            var record = cacheRecord ?? await _recordService.GetRecordAsync(request.Id, cancellationToken);

            if (record == null)
                throw new ArgumentException($"Record {request.Id} is not found!");
            
            if (record.IsSold)
                throw new ArgumentException("This record already has been sold!");

            await _recordService.MarkRecordAsSoldAsync(record.Id,_currentUser.Id, cancellationToken);
            
            var result = await _recordService.GetRecordAsync(record.Id, cancellationToken);
            await _cacheService.AddCacheAsync(result, 
                TimeSpan.FromMinutes(5), cancellationToken);
            
            return Unit.Value;
        }
    }
}