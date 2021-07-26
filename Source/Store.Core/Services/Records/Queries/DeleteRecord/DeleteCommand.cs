using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Interfaces;

namespace Store.Core.Services.Records.Queries.DeleteRecord
{
    public class DeleteCommand : IRequest, IIdentity
    {
        public Guid Id { get; set; }
    }
    
    public class DeleteRecordCommandHandler : IRequestHandler<DeleteCommand>
    {
        private readonly IRecordService _recordService;

        public DeleteRecordCommandHandler(IRecordService recordService)
        {
            _recordService = recordService;
        }
        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var record = await _recordService.GetRecord(request.Id);

            if (record == null)
                throw new ArgumentException($"Record {request.Id} is not found!");
            
            await _recordService.DeleteRecord(record.Id);
            
            return Unit.Value;
        }
    }
}