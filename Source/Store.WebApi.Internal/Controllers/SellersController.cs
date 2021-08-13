using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Contracts.Models;
using Store.Core.Contracts.Responses;
using Store.Core.Services.Internal.Sellers.Queries.CreateSeller;
using Store.Core.Services.Internal.Sellers.Queries.DeleteSeller;
using Store.Core.Services.Internal.Sellers.Queries.GetSellers;
using Store.Core.Services.Internal.Sellers.Queries.UpdateSellerAsync;

namespace Store.WebApi.Internal.Controllers
{
    [ApiController]
    [Route("api/internal/[controller]")]
    public class SellersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SellersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Seller), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetSellersResponse>> GetSellers([FromQuery] GetSellersQuery request ,CancellationToken cts)
        {
            var result = await _mediator.Send(request, cts);
            return Ok(result);
        }
        
        [HttpGet("getSeller/{id:guid}")]
        [ProducesResponseType(typeof(Seller), StatusCodes.Status200OK)]
        public async Task<ActionResult<Seller>> GetSellerById([FromRoute] Guid id ,CancellationToken cts)
        {
            var result = await _mediator.Send(new GetSellerByIdQuery { Id = id }, cts);
            return Ok(result);
        }

        [HttpPost("addSeller")]
        [ProducesResponseType(typeof(Seller), StatusCodes.Status201Created)]
        public async Task<ActionResult<Seller>> CreateSeller([FromBody] CreateSellerCommand request, CancellationToken cts)
        {
            var result = await _mediator.Send(request, cts);
            return result;
        }

        [HttpPut("updateSeller")]
        [ProducesResponseType(typeof(Seller), StatusCodes.Status200OK)]
        public async Task<ActionResult<Seller>> UpdateSeller([FromBody] UpdateSellerCommand request,
            CancellationToken cts)
        {
            var result = await _mediator.Send(request, cts);
            return Ok(result);
        }
        
        [HttpDelete("deleteSeller/{id:guid}")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        public async Task<NoContentResult> DeleteSeller([FromRoute] Guid id,
            CancellationToken cts)
        {
            await _mediator.Send(new DeleteSellerCommand { Id = id }, cts);
            return NoContent();
        }
    }
}