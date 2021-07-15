using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Contracts.Interfaces;
using Store.Contracts.Models;

namespace Store.Core.Handlers.CreateRecord
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
            if (request.Price <= 0)
                throw new ArgumentException("The price can only be positive, non-negative value!");

            if (string.IsNullOrEmpty(request.Name))
                throw new ArgumentException("Name can not be empty!");
            
            var record = new Record
            {
                Id = Guid.NewGuid(),
                Author = request.Author,
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