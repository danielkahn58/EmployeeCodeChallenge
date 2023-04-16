using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<EmployeeController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation comp)
        {
            
            _logger.LogDebug($"Received compensation create request to add a salary of ' {comp.Salary} ' for ' {comp.EmployeeId}'");

            _compensationService.Create(comp);

            return CreatedAtRoute("GetByEmployeeId", new { employeeId = comp.EmployeeId }, comp);
        }


        [Route("byemployeeid/{employeeId}")]
        [HttpGet("{employeeId}", Name = "GetByEmployeeId")]
        public IActionResult GetByEmployeeId(String employeeId)
        {
            //_logger.LogDebug($"Received compensation create request to add a salary of ' {comp.Salary} ' for ' {comp.Employee.FirstName} {comp.Employee.LastName}'");

            Compensation comp = _compensationService.GetByEmployeeId(employeeId);

            if (comp == null)
                return NotFound();

            return Ok(comp);
        }
    }
}
