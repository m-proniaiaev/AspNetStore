using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Contracts.Interfaces;

namespace Store.Core.Services.Records.Queries.DeleteRecord
{
    public class DeleteRecordCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    
    public class DeleteRecordCommandHandler : IRequestHandler<DeleteRecordCommand>
    {
        private readonly IRecordService _recordService;

        public DeleteRecordCommandHandler(IRecordService recordService)
        {
            _recordService = recordService;
        }
        public async Task<Unit> Handle(DeleteRecordCommand request, CancellationToken cancellationToken)
        {
            var record = await _recordService.GetRecord(request.Id);

            if (record == null)
                throw new ArgumentException($"Record {request.Id} is not found!");
            
            await _recordService.DeleteRecord(record.Id);
            
            return Unit.Value;
        }
    }
}