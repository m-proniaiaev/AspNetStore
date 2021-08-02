using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Core.Common.Interfaces;
using Store.Core.Contracts.Responses;
using Store.Core.Services.Sellers.Queries.GetSellers.Helpers;

namespace Store.Core.Services.Sellers.Queries.GetSellers
{
    public class GetSellersQueryHandler : IRequestHandler<GetSellersQuery, GetSellersResponse>
    {
        private readonly ISellerService _sellerService;

        public GetSellersQueryHandler(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }
        
        public async Task<GetSellersResponse> Handle(GetSellersQuery request, CancellationToken cancellationToken)
        {
            var sellers = await _sellerService.GetSellersAsync(cancellationToken);
            
            if (sellers is null) 
                throw new ArgumentException("No records in database!");
            
            var sellersQuery = sellers.AsQueryable();

            sellersQuery
                .FilterByName(request.Name)
                .FilterByTypes(request.RecordType)
                .FilterByCreatedBy(request.CreatedBy)
                .FilterByCreated(request.CreatedFrom, request.CreatedTo);
            
            sellersQuery = sellersQuery.SortBy(request.SortBy, request.SortOrder);

            var response = new GetSellersResponse
            {
                Records = sellersQuery.ToList(),
                SellerCount = sellersQuery.Count()
            };
            
            return response;
        }
    }
}