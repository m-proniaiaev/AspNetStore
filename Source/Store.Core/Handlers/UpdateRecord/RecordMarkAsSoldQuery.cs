using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Contracts.Interfaces;

namespace Store.Core.Handlers.UpdateRecord
{
    public class RecordMarkAsSoldQuery : IRequest
    {
        public Guid Id { get; set; }
    }
    
    public class RecordMarkAsSoldQueryHandler : IRequestHandler<RecordMarkAsSoldQuery>
    {
        private readonly IRecordService _recordService;

        public RecordMarkAsSoldQueryHandler(IRecordService recordService)
        {
            _recordService = recordService;
        }
        public async Task<Unit> Handle(RecordMarkAsSoldQuery request, CancellationToken cancellationToken)
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