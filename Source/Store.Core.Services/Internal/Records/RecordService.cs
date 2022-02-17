using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Database.Repositories.RecordRepository;

namespace Store.Core.Services.Internal.Records
{
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _recordRepository;
        private readonly ICurrentUserService _currentUserService;

        public RecordService(IRecordRepository recordRepository, ICurrentUserService currentUserService)
        {
            _recordRepository = recordRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<Record>> GetRecordsAsync(CancellationToken cts)
        {
            var filter = new RecordFilter();

            if (_currentUserService.RoleType != RoleType.Administrator)
            {
                filter.IsSold = false;
            }

            return await _recordRepository.FindManyAsync(filter, cts);
        }
        
        public async Task AddRecordAsync(Record record, CancellationToken cts)
        {
            await _recordRepository.CreateAsync(record, cts);
        }

        public async Task<Record> GetRecordAsync(Guid id, CancellationToken cts)
        {
            var filter = new RecordFilter
            {
                Id = id,
                Limit = 1
            };

            if (_currentUserService.RoleType != RoleType.Administrator)
            {
                filter.IsSold = false;
            }
            return (await _recordRepository.FindManyAsync(filter, cts)).FirstOrDefault();
        }

        public async Task DeleteRecordAsync(Guid id, CancellationToken cts)
        {
            var filter = Builders<Record>.Filter.Eq(r => r.Id, id);
            await _recordRepository.DeleteAsync(filter, cts);
        }

        public async Task UpdateRecordAsync(Record record, CancellationToken cts)
        {
            await _recordRepository.UpdateAsync(record, cts);
        }

        public async Task MarkRecordAsSoldAsync(Guid id, Guid editor, CancellationToken cts)
        {
            await _recordRepository.SellAsync(id, editor, cts);
        }
    }
}