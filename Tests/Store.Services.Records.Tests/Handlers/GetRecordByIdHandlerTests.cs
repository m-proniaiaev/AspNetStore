using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Services.Internal.Records.Queries.GetRecords.ById;
using Xunit;
using Record = Store.Core.Contracts.Domain.Record;

namespace Store.Services.Records.Tests.Handlers
{
    public class GetRecordByIdHandlerTests
    {
        private readonly Mock<IRecordService> _recordService;
        private readonly Mock<ICacheService> _cacheService;

        public GetRecordByIdHandlerTests()
        {
            _cacheService = new Mock<ICacheService>();
            _recordService = new Mock<IRecordService>();
        }

        [Fact]
        public async Task HandleGetByIdRequest_GetsFromCache_WhenPresent()
        {
            var id = Guid.NewGuid();
            var request = new GetRecordByIdQuery
            {
                Id = id
            };
            
            var expectedRecord = new Record
            {
                Id = id,
                Name = "Biba",
            };

            _cacheService.Setup(arg => arg.GetCacheAsync<Record>(id.ToString(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedRecord);

            var handle = new GetRecordByIdQueryHandler(_recordService.Object, _cacheService.Object);

            var result = await handle.Handle(request, CancellationToken.None);
            
            result.Should().BeEquivalentTo(expectedRecord, x => x.ExcludingMissingMembers());
            
            _cacheService.Verify(x=>x.GetCacheAsync<Record>(id.ToString(), It.IsAny<CancellationToken>()), Times.Once);
            _recordService.Verify(x=>x.GetRecordAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        }
        
        [Fact]
        public async Task HandleGetByIdRequest_GetsFromDb_WhenNoCache()
        {
            var id = Guid.NewGuid();
            var request = new GetRecordByIdQuery
            {
                Id = id
            };
            
            var expectedRecord = new Record
            {
                Id = id,
                Name = "Biba",
            };

            _cacheService.Setup(arg => arg.GetCacheAsync<Record>(id.ToString(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Record) null);
            _recordService.Setup(x => x.GetRecordAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedRecord);

            var handle = new GetRecordByIdQueryHandler(_recordService.Object, _cacheService.Object);

            var result = await handle.Handle(request, CancellationToken.None);
            
            result.Should().BeEquivalentTo(expectedRecord, x => x.ExcludingMissingMembers());
            
            _cacheService.Verify(x=>x.GetCacheAsync<Record>(id.ToString(), It.IsAny<CancellationToken>()), Times.Once);
            _recordService.Verify(x=>x.GetRecordAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        
        [Fact]
        public void HandleGetByIdRequest_Throws_WhenNoRecord()
        {
            var id = Guid.NewGuid();
            var request = new GetRecordByIdQuery
            {
                Id = id
            };
            
            _cacheService.Setup(arg => arg.GetCacheAsync<Record>(id.ToString(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Record) null);
            _recordService.Setup(x => x.GetRecordAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Record)null);

            var handle = new GetRecordByIdQueryHandler(_recordService.Object, _cacheService.Object);

            Func<Task<Record>> result = async () => await handle.Handle(request, CancellationToken.None);
            result.Should().Throw<ArgumentException>();
        }
    }
}