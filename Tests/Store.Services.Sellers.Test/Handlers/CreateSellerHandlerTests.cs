using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Internal.Sellers.Queries.CreateSeller;
using Xunit;

namespace Store.Services.Sellers.Test.Handlers
{
    public class CreateSellerHandlerTests
    {
        
        private readonly Mock<ISellerService> _sellerService;
        private readonly Mock<ICacheService> _cacheService;

        public CreateSellerHandlerTests()
        {
            _sellerService = new Mock<ISellerService>();
            _cacheService = new Mock<ICacheService>();
        }
        
        [Fact]
        public async Task HandleRequest_CreatesSeller_WhenRequestIsCorrect()
        {
            var request = new CreateSellerCommand
            {
                Name = "Test",
                RecordType = new [] { RecordType.CPU }
            };

            var result = new Seller
            {
                Name = request.Name,
                RecordType = request.RecordType
            };

            _cacheService.Setup(x => x.AddCacheAsync(result, It.IsAny<TimeSpan>(), CancellationToken.None));
            _sellerService.Setup(x => x.CreateSellerAsync(request, It.IsAny<Guid>(), CancellationToken.None));
            _sellerService.Setup(x => x.GetSellerAsync(It.IsAny<Guid>(), CancellationToken.None))
                .ReturnsAsync(result);

            var handler = new CreateSellerCommandHandler(_sellerService.Object, _cacheService.Object);
            
            var response = await handler.Handle(request, CancellationToken.None);
            
            response.Should().BeEquivalentTo(result);
            
            _cacheService.Verify(x => x.AddCacheAsync(result, It.IsAny<TimeSpan>(), CancellationToken.None), Times.Once);
            _sellerService.Verify(x => x.CreateSellerAsync(request, It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            _sellerService.Verify(x => x.GetSellerAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
        }
    }
}