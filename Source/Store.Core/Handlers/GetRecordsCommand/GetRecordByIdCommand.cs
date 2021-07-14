using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Interfaces;
using Store.Core.Models;

namespace Store.Core.Handlers.GetRecordsCommand
{
    public class GetRecordByIdCommand : IRequest<Record>
    {
        public Guid Id { get; set; }
    }

    public class GetRecordsCommandHandler : IRequestHandler<GetRecordByIdCommand, Record>
    {
        private readonly IRecordService _recordService;
        
        public GetRecordsCommandHandler(IRecordService service)
        {
            _recordService = service;
        }
        public async Task<Record> Handle(GetRecordByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _recordService.GetRecord(request.Id);
            if (result == null)
                throw new ArgumentException($"Record {request.Id} does not exist!");

            return result;
        }
    }
}