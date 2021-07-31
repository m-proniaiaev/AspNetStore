using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Responses;
using Store.Core.Services.Records.Queries.GetRecords.Helpers;

namespace Store.Core.Services.Records.Queries.GetRecords
{
    public class GetRecordsQueryHandler : IRequestHandler<GetRecordsQuery, GetRecordsResponse>
    {
        private readonly IRecordService _recordService;

        public GetRecordsQueryHandler(IRecordService service)
        {
            _recordService = service;
        }

        public async Task<GetRecordsResponse> Handle(GetRecordsQuery request, CancellationToken cancellationToken)
        {
            var records = await _recordService.GetRecords(cancellationToken);
            
            if (records == null)
                throw new Exception("No records in database!");

            var recordsQuery = records.AsQueryable();
            
            recordsQuery = recordsQuery
                .FilterBySoldStatus(request.IsSold)
                .FilterByName(request.Name)
                .FilterBySeller(request.Seller)
                .FilterByPrice(request.Price)
                .FilterByCreated(request.CreatedFrom, request.CreatedTo)
                .FilterBySold(request.SoldFrom, request.SoldTo);
            

            recordsQuery = recordsQuery.SortBy(request.SortBy, request.SortOrder);
           
            var response = new GetRecordsResponse
            {
                Records = recordsQuery.ToList(),
                RecordCount = recordsQuery.Count()
            };
            
            return response;
        }
    }
}