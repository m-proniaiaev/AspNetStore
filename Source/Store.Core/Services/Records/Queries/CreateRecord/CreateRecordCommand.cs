using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Records.Queries.CreateRecord
{
    public class CreateRecordCommand : IRequest<Record>
    {
        public string Name { get; set; }
        public string Seller { get; set; }
        public decimal Price { get; set; }
    }
    
    public class CreateRecordCommandHandler : IRequestHandler<CreateRecordCommand, Record>
    {
        private readonly IRecordService _recordService;
        private readonly ICacheService _cacheService;

        public CreateRecordCommandHandler(IRecordService recordService, ICacheService cacheService)
        {
            _recordService = recordService;
            _cacheService = cacheService;
        }
        
        public async Task<Record> Handle(CreateRecordCommand request, CancellationToken cancellationToken)
        {
            //TODO check for name and author
            var id = Guid.NewGuid();
            await _recordService.AddRecordAsync(request, id);
                
            var result = await _recordService.GetRecord(id);
            
            await _cacheService.AddCacheAsync(result, TimeSpan.FromMinutes(5), cancellationToken);
            
            return result;
        }
    }
}