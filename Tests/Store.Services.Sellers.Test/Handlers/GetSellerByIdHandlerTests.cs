using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Services.Sellers.Queries.GetSellers;
using Xunit;

namespace Store.Services.Sellers.Test.Handlers
{
    public class GetSellerByIdHandlerTests
    {
        private readonly Mock<ISellerService> _sellerService;
        private readonly Mock<ICacheService> _cacheService;

        public GetSellerByIdHandlerTests()
        {
            _sellerService = new Mock<ISellerService>();
            _cacheService = new Mock<ICacheService>();
        }

        [Fact]
        public async Task HandleQuery_ReturnsCorrectSeller_FromCache()
        {
            var request = new GetSellerByIdQuery() { Id = Guid.NewGuid() };
            var seller = new Seller() { Id = request.Id };

            _cacheService.Setup(x => x.GetCacheAsync<Seller>(request.Id.ToString(), CancellationToken.None))
                .ReturnsAsync(seller);

            var handler = new GetSellerByIdQueryHandler(_cacheService.Object, _sellerService.Object);

            var result = await handler.Handle(request, CancellationToken.None);
            
            result.Should().BeEquivalentTo(seller);
            _cacheService.Verify(x => x.GetCacheAsync<Seller>(request.Id.ToString(), CancellationToken.None), Times.Once);
            _sellerService.Verify(x=>x.GetSellerAsync(request.Id, CancellationToken.None),Times.Never);
        }
        
        [Fact]
        public async Task HandleQuery_ReturnsCorrectSeller_FromDb()
        {
            var request = new GetSellerByIdQuery() { Id = Guid.NewGuid() };
            var seller = new Seller() { Id = request.Id };

            _cacheService.Setup(x => x.GetCacheAsync<Seller>(request.Id.ToString(), CancellationToken.None))
                .ReturnsAsync((Seller)null);
            _sellerService.Setup(x => x.GetSellerAsync(request.Id, CancellationToken.None))
                .ReturnsAsync(seller);

            var handler = new GetSellerByIdQueryHandler(_cacheService.Object, _sellerService.Object);

            var result = await handler.Handle(request, CancellationToken.None);
            
            result.Should().BeEquivalentTo(seller);
            _cacheService.Verify(x => x.GetCacheAsync<Seller>(request.Id.ToString(), CancellationToken.None), Times.Once);
            _sellerService.Verify(x=>x.GetSellerAsync(request.Id, CancellationToken.None),Times.Once);
        }
    }
}