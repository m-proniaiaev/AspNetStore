﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Contracts.Models;
using Store.Core.Contracts.Responses;
using Store.Core.Services.Records.Queries.CreateRecord;
using Store.Core.Services.Records.Queries.DeleteRecord;
using Store.Core.Services.Records.Queries.GetRecords;
using Store.Core.Services.Records.Queries.GetRecords.ById;
using Store.Core.Services.Records.Queries.UpdateRecord;

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
            var result = await _mediator.Send(new GetRecordByIdQuery {Id = id}, cts);
            return Ok(result);
        }

        [HttpPost("addRecord")]
        [ProducesResponseType(typeof(Record), StatusCodes.Status201Created)]
        public async Task<ActionResult<Record>> AddRecord([FromBody] CreateRecordCommand command, CancellationToken cts)
        {
            var result = await _mediator.Send(command, cts);
            return result;
        }

        [HttpPut("updateRecord")]
        [ProducesResponseType(typeof(Record), StatusCodes.Status200OK)]
        public async Task<ActionResult<Record>> UpdateRecord([FromBody] UpdateRecordCommand command, CancellationToken cts)
        {
            var result = await _mediator.Send(command, cts);
            return Ok(result);
        }

        [HttpPut("markAsSold/{id:guid}",  Name = "MarkAsSold")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        public async Task<NoContentResult> RecordMarkAsSold([FromRoute] Guid id, CancellationToken cts)
        {
            await _mediator.Send(new MarkAsSoldCommand {Id = id}, cts);
            return NoContent();
        }
        
        [HttpDelete("deleteRecord/{id:guid}")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        public async Task<NoContentResult> DeleteRecord([FromRoute] Guid id, CancellationToken cts)
        {
            await _mediator.Send(new DeleteRecordCommand{ Id = id}, cts);
            return NoContent();
        }
    }
}