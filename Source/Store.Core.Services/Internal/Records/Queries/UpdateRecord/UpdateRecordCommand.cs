using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Host.Authorization.CurrentUser;
using Store.Core.Services.Common.Interfaces;

namespace Store.Core.Services.Internal.Records.Queries.UpdateRecord
{
    public class UpdateRecordCommand : IRequest<Record>, IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsSold { get; set; }
    }
    
    public class UpdateRecordCommandHandler : IRequestHandler<UpdateRecordCommand, Record>
    {
        private readonly IRecordService _recordService;
        private readonly ICacheService _cacheService;
        private readonly ICurrentUserService _currentUser;

        public UpdateRecordCommandHandler(IRecordService recordService, ICacheService cacheService, ICurrentUserService currentUser)
        {
            _recordService = recordService;
            _cacheService = cacheService;
            _currentUser = currentUser;
        }
        
        public async Task<Record> Handle(UpdateRecordCommand request, CancellationToken cancellationToken)
        {
            var cachedRecord = await _cacheService.GetCacheAsync<Record>(request.Id.ToString(), cancellationToken);
            
            var record = cachedRecord ?? await _recordService.GetRecordAsync(request.Id, cancellationToken);
            
            if (record == null)
                throw new ArgumentException($"Record {request.Id} is not found!");
            
            if (record.IsSold)
                throw new ArgumentException("You can not change records which already has been sold!");

            DateTime? includeSoldDate = request.IsSold ? DateTime.Now : null;
            
            record.Name = request.Name;
            record.Price = request.Price;
            record.IsSold = request.IsSold;
            record.SoldDate = includeSoldDate;
            record.Edited = DateTime.Now;
            record.EditedBy = _currentUser.Id;

            await _recordService.UpdateRecordAsync(record, cancellationToken);
            
            var result = await _recordService.GetRecordAsync(record.Id, cancellationToken);
            
            await _cacheService.AddCacheAsync(result, TimeSpan.FromMinutes(5), cancellationToken);

            return result;
        }
    }
}