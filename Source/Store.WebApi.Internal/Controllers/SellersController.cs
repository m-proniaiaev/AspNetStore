using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Responses;
using Store.Core.Host.Authorization;
using Store.Core.Services.Internal.Sellers.Commands.CreateSeller;
using Store.Core.Services.Internal.Sellers.Commands.DeleteSeller;
using Store.Core.Services.Internal.Sellers.Commands.UpdateSeller;
using Store.Core.Services.Internal.Sellers.Queries.GetSellers;

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

        [ActionRequired("Sellers-Get")]
        [HttpGet]
        [ProducesResponseType(typeof(Seller), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetSellersResponse>> GetSellers([FromQuery] GetSellersQuery request ,CancellationToken cts)
        {
            var result = await _mediator.Send(request, cts);
            return Ok(result);
        }
        
        [ActionRequired( "Seller-Get")]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Seller), StatusCodes.Status200OK)]
        public async Task<ActionResult<Seller>> GetSellerById([FromRoute] Guid id ,CancellationToken cts)
        {
            var result = await _mediator.Send(new GetSellerByIdQuery { Id = id }, cts);
            return Ok(result);
        }

        [ActionRequired("Seller-Create")]
        [HttpPost("Seller")]
        [ProducesResponseType(typeof(Seller), StatusCodes.Status201Created)]
        public async Task<ActionResult<Seller>> CreateSeller([FromBody] CreateSellerCommand request, CancellationToken cts)
        {
            var result = await _mediator.Send(request, cts);
            return result;
        }

        [ActionRequired("Seller-Update")]
        [HttpPut("Seller")]
        [ProducesResponseType(typeof(Seller), StatusCodes.Status200OK)]
        public async Task<ActionResult<Seller>> UpdateSeller([FromBody] UpdateSellerCommand request,
            CancellationToken cts)
        {
            var result = await _mediator.Send(request, cts);
            return Ok(result);
        }
        
        [ActionRequired("Seller-Delete")]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        public async Task<NoContentResult> DeleteSeller([FromRoute] Guid id,
            CancellationToken cts)
        {
            await _mediator.Send(new DeleteSellerCommand { Id = id }, cts);
            return NoContent();
        }
    }
}