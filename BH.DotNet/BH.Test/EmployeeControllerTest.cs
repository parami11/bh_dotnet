using BH.Api.Controllers;
using BH.Api.Models;
using BH.Api.Service;
using BH.Api.Utilities;
using Microsoft.Extensions.Logging;
using Moq;

namespace BH.Test
{
    public class EmployeeControllerTest
    {
        private const string InvalidFirstNameMessage = "'First Name' must be between 2 and 50 characters.";

        [Fact]
        public async void InvalidFirstName()
        {
            var empValidator = new EmployeeValidator();
            Mock<EmployeeRepository> employeeRepository = new Mock<EmployeeRepository>();
            Mock<ILogger<EmployeeController>> logger = new Mock<ILogger<EmployeeController>>();

            var employeeController
                = new EmployeeController(employeeRepository.Object, logger.Object, empValidator);
            var employeeModel
                = new Employee() { Id = "1", FirstName = "a", LastName = "modarage", Description = "testDescription" };

            try
            {
                await employeeController.Post(employeeModel);
            }
            catch (Exception e)
            {
                var validationException = ((FluentValidation.ValidationException)e);
                Assert.Single(validationException.Errors);
                Assert.Contains(EmployeeControllerTest.InvalidFirstNameMessage, validationException.Message);
            }
        }

    }
}