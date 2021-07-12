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
        private readonly IRecordRepository _recordRepository;

        public RecordsController(ILogger<RecordsController> logger, IRecordRepository service)
        {
            _logger = logger;
            _recordRepository = service;
        }

        [HttpGet]
        public IActionResult GetRecords()
        {
            var result = _recordRepository.GetRecords();
            return Ok(result);
        }

        [HttpGet("{id:guid}", Name = "GetRecord")]
        public IActionResult GetRecord(Guid id)
        {
            var result = _recordRepository.GetRecord(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddRecord(Record record)
        {
            _recordRepository.AddRecord(record);
            return CreatedAtRoute("GetRecord", new { id = record.Id }, record);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteRecord(Guid id)
        {
            _recordRepository.DeleteRecord(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateRecord(Record record)
        {
            var result = _recordRepository.UpdateRecord(record);
            return Ok(result);
        }
    }
}
