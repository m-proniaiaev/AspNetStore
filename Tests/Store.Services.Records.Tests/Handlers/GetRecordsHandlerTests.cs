using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Responses;
using Store.Core.Services.Records.Queries.GetRecords;
using Xunit;
using Record = Store.Core.Contracts.Models.Record;

namespace Store.Services.Records.Tests.Handlers
{
    public class GetRecordsHandlerTests
    {
        private readonly Mock<IRecordService> _recordService;

        public GetRecordsHandlerTests()
        {
            _recordService = new Mock<IRecordService>();
        }

        [Fact]
        public async Task HandleRequest_WithNoFilter_ReturnsAllRecords()
        {
            var recordOne = Guid.NewGuid();
            var recordTwo = Guid.NewGuid();
            var recordThree = Guid.NewGuid();

            var records = new List<Record>
            {
                new Record
                {
                    Id = recordOne,
                    Name = "Buhalteria",
                    IsSold = false
                },
                new Record
                {
                    Id = recordTwo,
                    Name = "boba",
                    IsSold = true,
                },
                new Record
                {
                    Id = recordThree,
                    Name = "Biba"
                }
            };

            var request = new GetRecordsQuery();

            var expectedResponse = new GetRecordsResponse()
            {
                Records = records,
                RecordCount = 3
            };

            _recordService.Setup(x => x.GetRecordsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(records);
            
            //Act
            var handler = new GetRecordsQueryHandler(_recordService.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.Records.Should().BeEquivalentTo(expectedResponse.Records);
            result.RecordCount.Should().Be(expectedResponse.RecordCount);
        }
        
        [Fact]
        public async Task HandleRequest_WithFilters_ReturnsCorrectCollection()
        {
            var recordOne = Guid.NewGuid();
            var recordTwo = Guid.NewGuid();
            var recordThree = Guid.NewGuid();

            var records = new List<Record>
            {
                new Record
                {
                    Id = recordOne,
                    Name = "Buhalteria",
                    IsSold = false
                },
                new Record
                {
                    Id = recordTwo,
                    Name = "boba",
                    IsSold = true,
                },
                new Record
                {
                    Id = recordThree,
                    Name = "Biba",
                    IsSold = false
                }
            };

            var request = new GetRecordsQuery()
            {
                Name = records[1].Name,
                IsSold = records[1].IsSold
            };

            var expectedResponse = new GetRecordsResponse()
            {
                Records = new List<Record> { records[1] },
                RecordCount = 1
            };

            _recordService.Setup(x => x.GetRecordsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(records);
            
            //Act
            var handler = new GetRecordsQueryHandler(_recordService.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.Records.Should().BeEquivalentTo(expectedResponse.Records);
            result.RecordCount.Should().Be(expectedResponse.RecordCount);
        }
        
        [Fact]
        public async Task HandleRequest_WithIncorrectFilters_ReturnsEmpty()
        {
            var recordOne = Guid.NewGuid();
            
            var records = new List<Record>
            {
                new Record
                {
                    Id = recordOne,
                    Name = "Buhalteria",
                    IsSold = false
                },
            };

            var request = new GetRecordsQuery()
            {
                Name = "x"
            };

            var expectedResponse = new GetRecordsResponse()
            {
                Records = new List<Record>(),
                RecordCount = 0
            };

            _recordService.Setup(x => x.GetRecordsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(records);
            
            //Act
            var handler = new GetRecordsQueryHandler(_recordService.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.Records.Should().BeEquivalentTo(expectedResponse.Records);
            result.RecordCount.Should().Be(expectedResponse.RecordCount);
        }
    }
}