using System;
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

        [HttpGet("{id:guid}", Name = "GetRecord")]
        public IActionResult GetRecord(Guid id)
        {
            var result = _recordService.GetRecord(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddRecord(Record record)
        {
            _recordService.AddRecord(record);
            return CreatedAtRoute("GetRecord", new { id = record.Id }, record);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteRecord(Guid id)
        {
            _recordService.DeleteRecord(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateRecord(Record record)
        {
            var result = _recordService.UpdateRecord(record);
            return Ok(result);
        }

        [HttpPut("markAsSold/{id:guid}",  Name = "MarkAsSold")]
        public IActionResult RecordMarkAsSold(Guid id)
        {
            _recordService.MarkRecordAsSold(id);
            return NoContent();
        }
    }
}
