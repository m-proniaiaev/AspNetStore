using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Contracts.Domain;
using Store.Core.Contracts.Responses;
using Store.Core.Host.Authorization;
using Store.Core.Services.Internal.Records.Commands.CreateRecord;
using Store.Core.Services.Internal.Records.Commands.DeleteRecord;
using Store.Core.Services.Internal.Records.Commands.UpdateRecord;
using Store.Core.Services.Internal.Records.Queries.GetRecords;
using Store.Core.Services.Internal.Records.Queries.GetRecords.ById;

namespace Store.WebApi.Internal.Controllers
{
    [ApiController]
    [Route("api/internal/[controller]")]
    public class RecordsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecordsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ActionRequired("Records-Get")]
        [HttpGet]
        [ProducesResponseType(typeof(GetRecordsResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetRecordsResponse>> GetRecords([FromQuery] GetRecordsQuery query, CancellationToken cts)
        {
            var result = await _mediator.Send(query, cts);
            return Ok(result);
        }

        [ActionRequired( "Record-Get")]
        [HttpGet("getRecord/{id:guid}", Name = "GetRecord")]
        [ProducesResponseType(typeof(Record), StatusCodes.Status200OK)]
        public async Task<ActionResult<Record>> GetRecord([FromRoute] Guid id, CancellationToken cts)
        {
            var result = await _mediator.Send(new GetRecordByIdQuery {Id = id}, cts);
            return Ok(result);
        }

        [ActionRequired("Record-Create")]
        [HttpPost("addRecord")]
        [ProducesResponseType(typeof(Record), StatusCodes.Status201Created)]
        public async Task<ActionResult<Record>> AddRecord([FromBody] CreateRecordCommand command, CancellationToken cts)
        {
            var result = await _mediator.Send(command, cts);
            return result;
        }

        [ActionRequired("Record-Update")]
        [HttpPut("updateRecord")]
        [ProducesResponseType(typeof(Record), StatusCodes.Status200OK)]
        public async Task<ActionResult<Record>> UpdateRecord([FromBody] UpdateRecordCommand command, CancellationToken cts)
        {
            var result = await _mediator.Send(command, cts);
            return Ok(result);
        }

        [ActionRequired("Record-Sell")]
        [HttpPut("markAsSold/{id:guid}",  Name = "MarkAsSold")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        public async Task<NoContentResult> RecordMarkAsSold([FromRoute] Guid id, CancellationToken cts)
        {
            await _mediator.Send(new MarkAsSoldCommand {Id = id}, cts);
            return NoContent();
        }
        
        [ActionRequired("Record-Delete")]
        [HttpDelete("deleteRecord/{id:guid}")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        public async Task<NoContentResult> DeleteRecord([FromRoute] Guid id, CancellationToken cts)
        {
            await _mediator.Send(new DeleteRecordCommand{ Id = id}, cts);
            return NoContent();
        }
    }
}
