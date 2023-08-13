using BH.Api.Models;
using BH.Api.Service;
using BH.Api.Utilities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BH.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IValidator<Employee> _employeeValidator;

        public EmployeeController(
            IEmployeeRepository employeeRepository,
            ILogger<EmployeeController> logger,
            IValidator<Employee> employeeValidator)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _employeeValidator = employeeValidator;
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
            try
            {
                _employeeValidator.ValidateAndThrow(employee);
                _logger.LogInformation($"Post - {employee.FirstName} - {employee.LastName}");
                return await this._employeeRepository.AddEmployee(employee);
            }
            catch (ValidationException e)
            {
                _logger.LogInformation($"Post - " + e.Message);
                throw e;
            }
        }
    }
}