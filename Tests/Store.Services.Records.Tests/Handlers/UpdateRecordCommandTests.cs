using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Interfaces;
using Store.Core.Services.Records.Queries.UpdateRecord;
using Xunit;
using Record = Store.Core.Contracts.Models.Record;

namespace Store.Services.Records.Tests.Handlers
{
    public class UpdateRecordCommandTests
    {
        private readonly Mock<IRecordService> _recordService;
        private readonly Mock<ICacheService> _cacheService;

        public UpdateRecordCommandTests()
        {
            _recordService = new Mock<IRecordService>();
            _cacheService = new Mock<ICacheService>();
        }

        [Fact]
        public void HandleRequest_ThrowsErrorFor_SoldRecord()
        {
            var id = Guid.NewGuid();
            var record = new Record()
            {
                Id = id,
                IsSold = true
            };

            var request = new UpdateRecordCommand
            {
                Id = id,
                IsSold = false
            };

            _cacheService.Setup(x => x.GetCacheAsync<Record>(id.ToString(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(record);

            var handler = new UpdateRecordCommandHandler(_recordService.Object, _cacheService.Object);

            Func<Task<Record>> handle = async () => await handler.Handle(request, It.IsAny<CancellationToken>());

            handle.Should().Throw<ArgumentException>();
        }
        
        [Fact]
        public async Task HandleRequest_UpdatesRecord_ForCorrectRequest()
        {
            //Arrange
            var id = Guid.NewGuid();
            var record = new Record()
            {
                Id = id,
                IsSold = false
            };

            var request = new UpdateRecordCommand
            {
                Id = id,
                IsSold = true
            };

            var updatedRecord = new Record()
            {
                Id = request.Id,
                IsSold = request.IsSold
            };

            _cacheService.Setup(x => x.GetCacheAsync<Record>(id.ToString(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(record);
            _recordService.Setup(x => x.UpdateRecordAsync(request, record, It.IsAny<CancellationToken>()));
            _recordService.Setup(x => x.GetRecordAsync(request.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(updatedRecord);
            _cacheService.Setup(x =>
                x.AddCacheAsync(updatedRecord, It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()));

            //Act
            var handler = new UpdateRecordCommandHandler(_recordService.Object, _cacheService.Object);

            var result = await handler.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(updatedRecord, x=>x.ExcludingMissingMembers());
            
            _cacheService.Verify(x => x.GetCacheAsync<Record>(id.ToString(), It.IsAny<CancellationToken>()), Times.Once);
            _recordService.Verify(x => x.UpdateRecordAsync(request, record, It.IsAny<CancellationToken>()), Times.Once);
            _recordService.Verify(x => x.GetRecordAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once);
            _cacheService.Verify(x =>
                x.AddCacheAsync(updatedRecord, It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        
        [Fact]
        public async Task HandleRequest_UpdatesRecord_ForCorrectRequest_WhenRecordIsFromDb()
        {
            //Arrange
            var id = Guid.NewGuid();
            var record = new Record()
            {
                Id = id,
                IsSold = false
            };

            var request = new UpdateRecordCommand
            {
                Id = id,
                IsSold = false
            };

            var updatedRecord = new Record()
            {
                Id = request.Id,
                IsSold = request.IsSold
            };

            _cacheService.Setup(x => x.GetCacheAsync<Record>(id.ToString(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Record) null);
            
            _recordService.Setup(x => x.UpdateRecordAsync(request, record, It.IsAny<CancellationToken>()));
            
            _recordService.Setup(x => x.GetRecordAsync(record.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(updatedRecord);
            
            _cacheService.Setup(x =>
                x.AddCacheAsync(updatedRecord, It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()));

            //Act
            var handler = new UpdateRecordCommandHandler(_recordService.Object, _cacheService.Object);

            var result = await handler.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(updatedRecord, x=>x.ExcludingMissingMembers());
            
            _cacheService.Verify(x => x.GetCacheAsync<Record>(id.ToString(), It.IsAny<CancellationToken>()), Times.Once);
            
            _recordService.Verify(x => x.UpdateRecordAsync(request, updatedRecord, It.IsAny<CancellationToken>()), Times.Once);
            
            _recordService.Verify(x => x.GetRecordAsync(request.Id, It.IsAny<CancellationToken>()), Times.Exactly(2));
            
            _cacheService.Verify(x =>
                x.AddCacheAsync(updatedRecord, It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}