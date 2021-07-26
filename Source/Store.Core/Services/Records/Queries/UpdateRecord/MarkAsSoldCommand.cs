using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Contracts.Interfaces;

namespace Store.Core.Services.Records.Queries.UpdateRecord
{
    public class MarkAsSoldCommand : IRequest, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class RecordMarkAsSoldCommandHandler : IRequestHandler<MarkAsSoldCommand>
    {
        private readonly IRecordService _recordService;

        public RecordMarkAsSoldCommandHandler(IRecordService recordService)
        {
            _recordService = recordService;
        }
        public async Task<Unit> Handle(MarkAsSoldCommand request, CancellationToken cancellationToken)
        {
            var record = await _recordService.GetRecord(request.Id);

            if (record == null)
                throw new Exception($"Record {request.Id} is not found!");
            
            if (record.IsSold)
                throw new Exception("This record already has been sold!");
            
            await _recordService.MarkRecordAsSold(record.Id);
            
            return Unit.Value;
        }
    }
}