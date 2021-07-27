using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;

namespace Store.Core.Services.Records.Queries.CreateRecord
{
    public class CreateRecordCommand : IRequest<Record>
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }
    
    public class CreateRecordCommandHandler : IRequestHandler<CreateRecordCommand, Record>
    {
        private readonly IRecordService _recordService;

        public CreateRecordCommandHandler(IRecordService recordService)
        {
            _recordService = recordService;
        }
        
        public async Task<Record> Handle(CreateRecordCommand request, CancellationToken cancellationToken)
        {
            //TODO check for name and author
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