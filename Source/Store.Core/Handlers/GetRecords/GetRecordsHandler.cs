using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Contracts.Interfaces;

namespace Store.Core.Handlers.GetRecords
{
    public class GetRecordsHandler : IRequestHandler<GetRecordsQuery, GetRecordsResponse>
    {
        private readonly IRecordService _recordService;
        
        public GetRecordsHandler(IRecordService service)
        {
            _recordService = service;
        }

        public async Task<GetRecordsResponse> Handle(GetRecordsQuery request, CancellationToken cancellationToken)
        {
            var records = await _recordService.GetRecords();

            if (records == null)
                throw new Exception("No records in database!");
            
            //TODO move to queryable filtering helpers
            if (request.IsSold.HasValue)
                    records = request.IsSold.Value
                    ? records.Select(x => x).Where(x => x.IsSold).ToList()
                    : records.Select(x => x).Where(x => x.IsSold != true).ToList();


            var response = new GetRecordsResponse
            {
                Records = records,
                RecordCount = records.Count
            };
            
            return response;
        }
    }
}