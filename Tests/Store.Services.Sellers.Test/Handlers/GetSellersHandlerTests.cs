using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Models;
using Store.Core.Contracts.Responses;
using Store.Core.Internal.Sellers.Queries.GetSellers;
using Xunit;

namespace Store.Services.Sellers.Test.Handlers
{
    public class GetSellersHandlerTests
    {
        private readonly Mock<ISellerService> _sellerService;

        public GetSellersHandlerTests()
        {
            _sellerService = new Mock<ISellerService>();
        }
        
        
        [Fact]
        public async Task HandleRequest_WithNoFilter_ReturnsAllRecords()
        {
            var sellers = new List<Seller>()
            {
                new Seller()
                {
                    Id = Guid.NewGuid()
                },
                new Seller()
                {
                    Id = Guid.NewGuid()
                }
            };

            var request = new GetSellersQuery();

            var expectedResult = new GetSellersResponse()
            {
                Sellers = sellers,
                SellerCount = 2
            };

            _sellerService.Setup(x => x.GetSellersAsync(CancellationToken.None))
                .ReturnsAsync(sellers);

            var handler = new GetSellersQueryHandler(_sellerService.Object);

            var result = await handler.Handle(request, CancellationToken.None);
            
            result.Should().BeEquivalentTo(expectedResult);
        }
        
        [Fact]
        public async Task HandleRequest_ReturnsCorrectRecords_WhenFiltered()
        {
            var sellers = new List<Seller>()
            {
                new Seller()
                {
                    Id = Guid.NewGuid(),
                    Name = "biba"
                },
                new Seller()
                {
                    Id = Guid.NewGuid(),
                    Name = "boba"
                }
            };

            var request = new GetSellersQuery() { Name = sellers[0].Name };

            var expectedResult = new GetSellersResponse()
            {
                Sellers = new List<Seller> { sellers[0] },
                SellerCount = 1
            };

            _sellerService.Setup(x => x.GetSellersAsync(CancellationToken.None))
                .ReturnsAsync(sellers);

            var handler = new GetSellersQueryHandler(_sellerService.Object);

            var result = await handler.Handle(request, CancellationToken.None);
            
            result.Should().BeEquivalentTo(expectedResult);
        }
        
        [Fact]
        public async Task HandleRequest_ReturnsEmpty_WhenFiltersWrong()
        {
            var sellers = new List<Seller>()
            {
                new Seller()
                {
                    Id = Guid.NewGuid(),
                    Name = "biba"
                },
                new Seller()
                {
                    Id = Guid.NewGuid(),
                    Name = "boba"
                }
            };

            var request = new GetSellersQuery() { Name = "abobus" };

            var expectedResult = new GetSellersResponse()
            {
                Sellers = new List<Seller>(),
                SellerCount = 0
            };

            _sellerService.Setup(x => x.GetSellersAsync(CancellationToken.None))
                .ReturnsAsync(sellers); 

            var handler = new GetSellersQueryHandler(_sellerService.Object);

            var result = await handler.Handle(request, CancellationToken.None);
            
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}