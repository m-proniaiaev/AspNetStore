using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Interfaces;
using Store.Core.Services.Records.Queries.CreateRecord;
using Xunit;
using Record = Store.Core.Contracts.Models.Record;

namespace Store.Services.Records.Tests.Handlers
{
    public class CreateRecordHandlerTests
    {
        private readonly Mock<IRecordService> _recordService;
        private readonly Mock<ICacheService> _cacheService;

        public CreateRecordHandlerTests()
        {
            _cacheService = new Mock<ICacheService>();
            _recordService = new Mock<IRecordService>();
        }

        [Fact]
        public async Task HandleCreateRecordRequest_WithCorrectRequest_CreatesRecord()
        {
            //Arrange
            var request = new CreateRecordCommand
            {
                Seller = "You",
                Name = "Thing",
                Price = 228
            };

            var expectedRecord = new Record
            {
                Seller = request.Seller,
                Name = request.Name,
                Price = request.Price
            };
            
            _recordService.Setup(x => x.AddRecordAsync(request, It.IsAny<Guid>()));
            
            _recordService.Setup(x => x.GetRecord(It.IsAny<Guid>()))
                .ReturnsAsync(expectedRecord);
            
            _cacheService.Setup(x => x.AddCacheAsync(It.IsAny<Record>(), default, It.IsAny<CancellationToken>()));
            
            var handler = new CreateRecordCommandHandler(_recordService.Object, _cacheService.Object);
            
            //Act
            Record result = await handler.Handle(request, CancellationToken.None);
            
            //Assert
            result.Should().BeEquivalentTo(expectedRecord, x=> x.ExcludingMissingMembers());
            
            _recordService.Verify(x=>x.AddRecordAsync(request, It.IsAny<Guid>()), Times.Once);
            _recordService.Verify(x=>x.GetRecord(It.IsAny<Guid>()), Times.Once);
            _cacheService.Verify(x=>x.AddCacheAsync(result, TimeSpan.FromMinutes(5),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}