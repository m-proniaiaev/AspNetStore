using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Contracts.Interfaces;
using Store.Contracts.Models;

namespace Store.Core.Services.Records.Queries.GetRecords.ById
{
    public class GetByIdQuery : IRequest<Record>, IIdentity
    {
        public Guid Id { get; set; }
    }

    public class GetRecordsQueryHandler : IRequestHandler<GetByIdQuery, Record>
    {
        private readonly IRecordService _recordService;
        
        public GetRecordsQueryHandler(IRecordService service)
        {
            _recordService = service;
        }
        public async Task<Record> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _recordService.GetRecord(request.Id);
            if (result == null)
                throw new ArgumentException($"Record {request.Id} does not exist!");

            return result;
        }
    }
}