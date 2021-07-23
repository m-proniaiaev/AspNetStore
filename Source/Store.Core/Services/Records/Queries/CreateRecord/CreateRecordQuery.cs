using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Contracts.Interfaces;
using Store.Contracts.Models;

namespace Store.Core.Services.Records.Queries.CreateRecord
{
    public class CreateRecordQuery : IRequest<Record>
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }
    
    public class CreateRecordQueryHandler : IRequestHandler<CreateRecordQuery, Record>
    {
        private readonly IRecordService _recordService;

        public CreateRecordQueryHandler(IRecordService recordService)
        {
            _recordService = recordService;
        }
        
        public async Task<Record> Handle(CreateRecordQuery request, CancellationToken cancellationToken)
        {
            
            var record = new Record
            {
                Id = Guid.NewGuid(),
                Seller = request.Author,
                Created = DateTime.Now,
                Name = request.Name,
                Price = request.Price,
                IsSold = false,
                SoldDate = null
            };
            
            return await _recordService.AddRecord(record);
        }
    }
}