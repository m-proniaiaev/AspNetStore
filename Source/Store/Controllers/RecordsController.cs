using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Contracts.Models;
using Store.Core.Handlers.GetRecords;
using Store.Core.Interfaces;

namespace SomeStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRecordService _recordService;

        public RecordsController(IMediator mediator, IRecordService service)
        {
            _mediator = mediator;
            _recordService = service;
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
        public async Task<IActionResult> AddRecord(Record record)
        {
            await _recordService.AddRecord(record);
            return CreatedAtRoute("GetRecord", new { id = record.Id }, record);
        }

        [HttpPut("updateRecord")]
        public async Task<IActionResult> UpdateRecord(Record record)
        {
            var result = await _recordService.UpdateRecord(record);
            return Ok(result);
        }

        [HttpPut("markAsSold/{id:guid}",  Name = "MarkAsSold")]
        public async Task<NoContentResult> RecordMarkAsSold(Guid id)
        {
            await _recordService.MarkRecordAsSold(id);
            return NoContent();
        }
        
        [HttpDelete("deleteRecord/{id:guid}")]
        public async Task<IActionResult> DeleteRecord(Guid id)
        {
            await _recordService.DeleteRecord(id);
            return NoContent();
        }
    }
}
