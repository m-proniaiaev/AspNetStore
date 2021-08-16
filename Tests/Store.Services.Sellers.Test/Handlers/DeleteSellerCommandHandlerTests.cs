using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Services.Common.Interfaces;
using Store.Core.Services.Internal.Sellers.Queries.DeleteSeller;
using Xunit;

namespace Store.Services.Sellers.Test.Handlers
{
    public class DeleteSellerCommandHandlerTests
    {
        private readonly Mock<ISellerService> _sellerService;
        private readonly Mock<ICacheService> _cacheService;

        public DeleteSellerCommandHandlerTests()
        {
            _sellerService = new Mock<ISellerService>();
            _cacheService = new Mock<ICacheService>();
        }

        [Fact]
        public async Task HandleRequest_DeletesSeller_WhenRequestIsCorrect()
        {
            var request = new DeleteSellerCommand()
            {
                Id = Guid.NewGuid()
            };

            var seller = new Seller() { Id = request.Id };
            
            _sellerService.Setup(x => x.GetSellerAsync(It.IsAny<Guid>(), CancellationToken.None))
                .ReturnsAsync(seller);
            _sellerService.Setup(x => x.DeleteSellerAsync(request.Id, CancellationToken.None));
            _cacheService.Setup(x => x.DeleteCacheAsync<Seller>(request.Id.ToString(), CancellationToken.None));

            var handler = new DeleteSellerCommandHandler(_cacheService.Object, _sellerService.Object);

            await handler.Handle(request, CancellationToken.None);

            _sellerService.Verify(x => x.GetSellerAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
            _sellerService.Verify(x => x.DeleteSellerAsync(request.Id, CancellationToken.None), Times.Once);
            _cacheService.Verify(x => x.DeleteCacheAsync<Seller>(request.Id.ToString(), CancellationToken.None), Times.Once);
        }
        
        
        [Fact]
        public void HandleRequest_Throws_WhenNoSuchSeller()
        {
            var request = new DeleteSellerCommand()
            {
                Id = Guid.NewGuid()
            };

            var seller = new Seller() { Id = request.Id };
            
            _sellerService.Setup(x => x.GetSellerAsync(It.IsAny<Guid>(), CancellationToken.None))
                .ReturnsAsync((Seller) null);

            var handler = new DeleteSellerCommandHandler(_cacheService.Object, _sellerService.Object);

            Func<Task<Unit>> result = async () => await handler.Handle(request, CancellationToken.None);

            result.Should().Throw<ArgumentException>();
            
            _sellerService.Verify(x => x.DeleteSellerAsync(request.Id, CancellationToken.None), Times.Never);
            _cacheService.Verify(x => x.DeleteCacheAsync<Seller>(request.Id.ToString(), CancellationToken.None), Times.Never);
        }
    }
}