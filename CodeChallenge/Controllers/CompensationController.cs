using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]

    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController>logger , ICompensationService compensationService)
        {
            _compensationService = compensationService;
            _logger = logger;
        }

        /// <summary>
        /// This method will handle http post requests to create a new compensation object for the database. Given a compensation object
        /// with a compensation id(id of the related employee), a salary, and effectiveDate in the format of yyyy-mm-dd, the compensation
        /// object will be placed into the database. 
        /// 
        /// If the employee does not exist in the system then a BadRequest is returned. Otherwise an create response is returned.
        /// </summary>
        /// <param name="compensation">Object to be added to the database</param>
        /// <returns>OK if object was accepted, otherwise BadRequest</returns>
        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received employee create request for '{compensation.CompensationId}'");
        
            var comp = _compensationService.Create(compensation);
            // Compensation should only be added to the database when the employee actually exists.
            if(comp != null)
                return CreatedAtRoute("getCompensationById", new {id = compensation.CompensationId}, compensation);
            return BadRequest();
        }

        /// <summary>
        /// Here we handle http get requests for compensation. The employee id received will
        /// be use to query for the compensation entity.
        /// 
        /// If the compensation was found then the compensation is returned with an OK response,
        /// otherwise a NotFound is returned.
        /// </summary>
        /// <param name="id">Employee id to query</param>
        /// <returns>OK if compensation exists, NotFound otherwise</returns>
        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(string id)
        {  
            _logger.LogDebug($"Received employee get request for '{id}'");

            var compensation = _compensationService.GetById(id);

            if(compensation == null){
                return NotFound();
            }

            return Ok(compensation);

        }
    }
}