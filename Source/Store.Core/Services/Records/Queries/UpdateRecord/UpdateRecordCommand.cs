using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Contracts.Interfaces;
using Store.Contracts.Models;

namespace Store.Core.Services.Records.Queries.UpdateRecord
{
    public class UpdateRecordCommand : IRequest<Record>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsSold { get; set; }
    }
    
    public class UpdateRecordCommandHandler : IRequestHandler<UpdateRecordCommand, Record>
    {
        private readonly IRecordService _recordService;

        public UpdateRecordCommandHandler(IRecordService recordService)
        {
            _recordService = recordService;
        }
        
        public async Task<Record> Handle(UpdateRecordCommand request, CancellationToken cancellationToken)
        {
            var record = await _recordService.GetRecord(request.Id);
            
            if (record == null)
                throw new Exception($"Record {request.Id} is not found!");
            
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