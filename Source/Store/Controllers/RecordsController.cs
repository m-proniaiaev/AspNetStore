using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Core.Interfaces;

namespace SomeStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordsController : ControllerBase
    {
        private readonly ILogger<RecordsController> _logger;
        private readonly IRecordService _service;

        public RecordsController(ILogger<RecordsController> logger, IRecordService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IActionResult GetRecords()
        {
            var result = _service.GetRecords();
            return Ok(result);
        }
    }
}
