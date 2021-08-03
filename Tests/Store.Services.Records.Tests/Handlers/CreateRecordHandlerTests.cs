using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Contracts.Responses;
using Store.Core.Services.Records.Queries.CreateRecord;
using Store.Core.Services.Sellers.Queries.GetSellers;
using Xunit;
using Record = Store.Core.Contracts.Models.Record;

namespace Store.Services.Records.Tests.Handlers
{
    public class CreateRecordHandlerTests
    {
        private readonly Mock<IRecordService> _recordService;
        private readonly Mock<ICacheService> _cacheService;
        private readonly Mock<IMediator> _mediator;

        public CreateRecordHandlerTests()
        {
            _cacheService = new Mock<ICacheService>();
            _recordService = new Mock<IRecordService>();
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task HandleCreateRecordRequest_WithCorrectRequest_CreatesRecord()
        {
            //Arrange
            var request = new CreateRecordCommand
            {
                Seller = "You",
                Name = "Thing",
                Price = 228,
                RecordType = RecordType.CPU
            };

            var expectedRecord = new Record
            {
                Seller = request.Seller,
                Name = request.Name,
                Price = request.Price
            };
            
            var seller = new GetSellersResponse() 
            { 
                Sellers = new List<Seller> 
                { new() 
                    { Name = request.Name, RecordType = new []
                        {
                            request.RecordType
                        } 
                    } 
                } 
            };
            
            _recordService.Setup(x => x.AddRecordAsync(request, It.IsAny<Guid>(), CancellationToken.None));
            
            _recordService.Setup(x => x.GetRecordAsync(It.IsAny<Guid>(), CancellationToken.None))
                .ReturnsAsync(expectedRecord);
            
            _cacheService.Setup(x => x.AddCacheAsync(It.IsAny<Record>(), default, It.IsAny<CancellationToken>()));
            
            _mediator.Setup(x => x.Send(It.IsAny<GetSellersQuery>(), CancellationToken.None)).ReturnsAsync(seller);
            
            var handler = new CreateRecordCommandHandler(_recordService.Object, _cacheService.Object, _mediator.Object);
            
            //Act
            Record result = await handler.Handle(request, CancellationToken.None);
            
            //Assert
            result.Should().BeEquivalentTo(expectedRecord, x=> x.ExcludingMissingMembers());
            
            _mediator.Verify(x => x.Send(It.IsAny<GetSellersQuery>(), CancellationToken.None), Times.Once);
            _recordService.Verify(x=>x.AddRecordAsync(request, It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            _recordService.Verify(x=>x.GetRecordAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            _cacheService.Verify(x=>x.AddCacheAsync(result, TimeSpan.FromMinutes(5),
                It.IsAny<CancellationToken>()), Times.Once);
        }
        
        [Fact]
        public void HandleCreateRecordRequest_FailsIf_NoSuchSeller()
        {
            //Arrange
            var request = new CreateRecordCommand
            {
                Seller = "You",
                Name = "Thing",
                Price = 228,
                RecordType = RecordType.CPU
            };
            
            _mediator.Setup(x => x.Send(It.IsAny<GetSellersQuery>(), CancellationToken.None))
                .ReturnsAsync(new GetSellersResponse());
            
            var handler = new CreateRecordCommandHandler(_recordService.Object, _cacheService.Object, _mediator.Object);
            
            //Act
            Func<Task<Record>> result = async () => await handler.Handle(request, CancellationToken.None);
            
            //Assert
            result.Should().Throw<ArgumentException>();
            _mediator.Verify(x => x.Send(It.IsAny<GetSellersQuery>(), CancellationToken.None), Times.Once);
        }
        
        [Fact]
        public void HandleCreateRecordRequest_FailsIf_NoRecordTypeIsViolated()
        {
            //Arrange
            var request = new CreateRecordCommand
            {
                Seller = "You",
                Name = "Thing",
                Price = 228,
                RecordType = RecordType.CPU
            };
            
            
            var seller = new GetSellersResponse() 
            { 
                Sellers = new List<Seller> 
                { new() 
                    { Name = request.Name, RecordType = new []
                        {
                            RecordType.GPU
                        } 
                    } 
                } 
            };
            
            
            _mediator.Setup(x => x.Send(It.IsAny<GetSellersQuery>(), CancellationToken.None)).ReturnsAsync(seller);
            
            var handler = new CreateRecordCommandHandler(_recordService.Object, _cacheService.Object, _mediator.Object);
            
            //Act
            Func<Task<Record>> result = async () => await handler.Handle(request, CancellationToken.None);
            
            //Assert
            result.Should().Throw<ArgumentException>();
            _mediator.Verify(x => x.Send(It.IsAny<GetSellersQuery>(), CancellationToken.None), Times.Once);
        }
    }
}