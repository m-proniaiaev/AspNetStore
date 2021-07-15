using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Contracts.Interfaces;
using Store.Contracts.Models;

namespace Store.Core.Handlers.UpdateRecord
{
    public class UpdateRecordQuery : IRequest<Record>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsSold { get; set; }
    }
    
    public class UpdateRecordHandler : IRequestHandler<UpdateRecordQuery, Record>
    {
        private readonly IRecordService _recordService;

        public UpdateRecordHandler(IRecordService recordService)
        {
            _recordService = recordService;
        }
        
        public async Task<Record> Handle(UpdateRecordQuery request, CancellationToken cancellationToken)
        {
            var record = await _recordService.GetRecord(request.Id);
            
            if (record == null)
                throw new Exception($"Record {request.Id} is not found!");
            
            if (request.Price <= 0)
                throw new ArgumentException("The price can only be positive, non-negative value!");

            if (string.IsNullOrEmpty(request.Name))
                throw new ArgumentException("Name can not be empty!");
            
            if (record.IsSold)
                throw new Exception("This record already has been sold!");

            var updatedRecord = new Record
            {
                Id = record.Id,
                Seller = record.Seller,
                Created = record.Created,
                Name = request.Name,
                Price = request.Price,
                IsSold = request.IsSold,
                SoldDate = DateTime.Now
            };

            return await _recordService.UpdateRecord(updatedRecord);
        }
    }
}