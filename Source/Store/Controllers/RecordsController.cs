using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Contracts.Models;
using Store.Core.Handlers.CreateRecord;
using Store.Core.Handlers.DeleteRecord;
using Store.Core.Handlers.GetRecords;
using Store.Core.Handlers.UpdateRecord;

namespace SomeStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecordsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetRecordsResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetRecordsResponse>> GetRecords([FromQuery] GetRecordsQuery query, CancellationToken cts)
        {
            var result = await _mediator.Send(query, cts);
            return Ok(result);
        }

        [HttpGet("getRecord/{id:guid}", Name = "GetRecord")]
        [ProducesResponseType(typeof(Record), StatusCodes.Status200OK)]
        public async Task<ActionResult<Record>> GetRecord([FromRoute] Guid id, CancellationToken cts)
        {
            var result = await _mediator.Send(new GetRecordByIdCommand {Id = id}, cts);
            return Ok(result);
        }

        [HttpPost("addRecord")]
        [ProducesResponseType(typeof(Record), StatusCodes.Status201Created)]
        public async Task<ActionResult<Record>> AddRecord([FromBody] CreateRecordQuery query, CancellationToken cts)
        {
            var result = await _mediator.Send(query, cts);
            return result;
        }

        [HttpPut("updateRecord")]
        [ProducesResponseType(typeof(Record), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateRecord([FromBody] UpdateRecordQuery query, CancellationToken cts)
        {
            var result = await _mediator.Send(query, cts);
            return Ok(result);
        }

        [HttpPut("markAsSold/{id:guid}",  Name = "MarkAsSold")]
        public async Task<NoContentResult> RecordMarkAsSold([FromRoute]Guid id, CancellationToken cts)
        {
            await _mediator.Send(new RecordMarkAsSoldQuery {Id = id}, cts);
            return NoContent();
        }
        
        [HttpDelete("deleteRecord/{id:guid}")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteRecord([FromRoute] Guid id, CancellationToken cts)
        {
            await _mediator.Send(new DeleteRecordQuery{ Id = id}, cts);
            return NoContent();
        }
    }
}
