using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Contracts.Interfaces;

namespace Store.Core.Handlers.DeleteRecord
{
    public class DeleteRecordQuery : IRequest
    {
        public Guid Id { get; set; }
    }
    
    public class DeleteRecordQueryHandler : IRequestHandler<DeleteRecordQuery>
    {
        private readonly IRecordService _recordService;

        public DeleteRecordQueryHandler(IRecordService recordService)
        {
            _recordService = recordService;
        }
        public async Task<Unit> Handle(DeleteRecordQuery request, CancellationToken cancellationToken)
        {
            var record = await _recordService.GetRecord(request.Id);

            if (record == null)
                throw new ArgumentException($"Record {request.Id} is not found!");
            
            await _recordService.DeleteRecord(record.Id);
            
            return Unit.Value;
        }
    }
}