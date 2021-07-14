using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Core.Interfaces;
using Store.Core.Models;

namespace SomeStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordsController : ControllerBase
    {
        private readonly ILogger<RecordsController> _logger;
        private readonly IRecordService _recordService;

        public RecordsController(ILogger<RecordsController> logger, IRecordService service)
        {
            _logger = logger;
            _recordService = service;
        }

        [HttpGet]
        public IActionResult GetRecords()
        {
            var result = _recordService.GetRecords();
            return Ok(result);
        }

        [HttpGet("getRecord/{id:guid}", Name = "GetRecord")]
        public IActionResult GetRecord(Guid id)
        {
            var result = _recordService.GetRecord(id);
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
