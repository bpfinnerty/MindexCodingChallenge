using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;


namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reportingStructure")]

    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;
    
        public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
        }

        /// <summary>
        /// Here we handle http get requests for reporting structures. This will cause the entire employee 
        /// and the number of reports to be displayed to the screen. An employee id will be used
        /// to query for the appropriate reporting structure.
        /// 
        /// If the employee was found then the reporting structure is returned with an OK response,
        /// otherwise a NotFound is returned.
        /// </summary>
        /// <param name="id">Employee id to query</param>
        /// <returns>OK if employee exists, NotFound otherwise</returns>
        [HttpGet("{id}", Name = "getReportingStructureById")]
        public IActionResult GetReportingStructureById(String id){
            _logger.LogDebug($"Received reportingStructure get request for '{id}'");

            var reportingStructure = _reportingStructureService.GetById(id);

            if(reportingStructure == null)
                return NotFound();
            return Ok(reportingStructure);
        }

    }
}