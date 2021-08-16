using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Enums;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Host.Authorization.CurrentUser;
using Store.Core.Services.Internal.Sellers.Commands.CreateSeller;
using Xunit;

namespace Store.Services.Sellers.Test.Handlers
{
    public class CreateSellerHandlerTests
    {
        
        private readonly Mock<ISellerService> _sellerService;
        private readonly Mock<ICacheService> _cacheService;
        private readonly Mock<ICurrentUserService> _currentUser;

        public CreateSellerHandlerTests()
        {
            _sellerService = new Mock<ISellerService>();
            _cacheService = new Mock<ICacheService>();
            _currentUser = new Mock<ICurrentUserService>();
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
            _sellerService.Setup(x => x.CreateSellerAsync(It.IsAny<Seller>(), CancellationToken.None));
            _sellerService.Setup(x => x.GetSellerAsync(It.IsAny<Guid>(), CancellationToken.None))
                .ReturnsAsync(result);
            _currentUser.Setup(x => x.Id).Returns(Guid.NewGuid);

            var handler = new CreateSellerCommandHandler(_sellerService.Object, _cacheService.Object, _currentUser.Object);
            
            var response = await handler.Handle(request, CancellationToken.None);
            
            response.Should().BeEquivalentTo(result);
            
            _cacheService.Verify(x => x.AddCacheAsync(result, It.IsAny<TimeSpan>(), CancellationToken.None), Times.Once);
            _sellerService.Verify(x => x.CreateSellerAsync(It.IsAny<Seller>(), CancellationToken.None), Times.Once);
            _sellerService.Verify(x => x.GetSellerAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
        }
    }
}