using BH.Api.Models;
using BH.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace BH.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(
            IEmployeeRepository employeeRepository,
            ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IActionResult> Get(int? page, int? limit)
        {
            _logger.LogInformation($"Get - {page} - {limit}");

            // To Do: Fluent Validator
            page = page ?? 1;
            limit = limit ?? 5;
            var result = await _employeeRepository.GetEmployees((int)page, (int)limit);

            return Ok(result);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<Employee> Post([FromBody] Employee employee)
        {
            // To Do: Fluent Validator
            _logger.LogInformation($"Post - {employee.FirstName} - {employee.LastName}");

            return await this._employeeRepository.AddEmployee(employee);
        }
    }
}