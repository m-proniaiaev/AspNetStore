using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Host.Authorization.CurrentUser;
using Store.Core.Services.Internal.Sellers.Queries.UpdateSellerAsync;
using Xunit;

namespace Store.Services.Sellers.Test.Handlers
{
    public class UpdateSellerHandlerTests
    {
        private readonly Mock<ISellerService> _sellerService;
        private readonly Mock<ICacheService> _cacheService;
        private readonly Mock<ICurrentUserService> _currentUser;

        public UpdateSellerHandlerTests()
        {
            _currentUser = new Mock<ICurrentUserService>();
            _sellerService = new Mock<ISellerService>();
            _cacheService = new Mock<ICacheService>();
        }

        [Fact]
        public async Task HandleUpdateSellerRequest_UpdatesSeller_IfRequest()
        {
            var request = new UpdateSellerCommand()
            {
                Id = Guid.NewGuid(),
                Name = "Abobus"
            };

            var origin = new Seller()
            {
                Id = request.Id,
                Name = "Kek"
            };

            var expected = new Seller()
            {
                Id = request.Id,
                Name = request.Name
            };

            _sellerService.Setup(x => x.UpdateSellerAsync(It.IsAny<Seller>(), CancellationToken.None));
            _cacheService.Setup(x => x.GetCacheAsync<Seller>(request.Id.ToString(), CancellationToken.None))
                .ReturnsAsync(origin);
            _sellerService.Setup(x => x.GetSellerAsync(request.Id, CancellationToken.None))
                .ReturnsAsync(expected);

            var handler = new UpdateSellerCommandHandler(_cacheService.Object, _sellerService.Object, _currentUser.Object);

            var result = await handler.Handle(request, CancellationToken.None);
            
            result.Should().BeEquivalentTo(expected);
            
            _sellerService.Verify(x=>x.UpdateSellerAsync(It.IsAny<Seller>(), It.IsAny<CancellationToken>()), Times.Once);
            _sellerService.Verify(x=>x.GetSellerAsync(request.Id, CancellationToken.None), Times.Once);
        }

    }
}
