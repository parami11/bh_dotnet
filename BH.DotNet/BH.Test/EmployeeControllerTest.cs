using BH.Api.Controllers;
using BH.Api.Service;
using BH.Api.Utilities;
using BH.Test.Mocks;
using Microsoft.Extensions.Logging;
using Moq;

namespace BH.Test
{
    public class EmployeeControllerTest
    {
        private const string InvalidFirstNameMessage = "'First Name' must be between 2 and 50 characters.";
        private const string InvalidLastNameMessage = "'Last Name' must be between 2 and 50 characters.";
        private const string InvalidDescriptionMessage = "'Description' must be between 2 and 50 characters.";

        private EmployeeValidator validator = new EmployeeValidator();
        private Mock<EmployeeRepository> employeeRepository = new Mock<EmployeeRepository>();
        private Mock<ILogger<EmployeeController>> logger = new Mock<ILogger<EmployeeController>>();

        [Fact]
        public async void InvalidFirstName()
        {
            var employeeController
                = new EmployeeController(employeeRepository.Object, logger.Object, validator);
            
            try
            {
                await employeeController.Post(MockEmployee.FirstNameInvalidEmployee);
            }
            catch (Exception e)
            {
                var validationException = ((FluentValidation.ValidationException)e);
                Assert.Single(validationException.Errors);
                Assert.Contains(EmployeeControllerTest.InvalidFirstNameMessage, validationException.Message);
            }
        }

        [Fact]
        public async void InvalidLastName()
        {
            var employeeController
                = new EmployeeController(employeeRepository.Object, logger.Object, validator);

            try
            {
                await employeeController.Post(MockEmployee.LastNameInvalidEmployee);
            }
            catch (Exception e)
            {
                var validationException = ((FluentValidation.ValidationException)e);
                Assert.Single(validationException.Errors);
                Assert.Contains(EmployeeControllerTest.InvalidLastNameMessage, validationException.Message);
            }
        }

        [Fact]
        public async void InvalidDescription()
        {
            var employeeController
                = new EmployeeController(employeeRepository.Object, logger.Object, validator);

            try
            {
                await employeeController.Post(MockEmployee.DescriptionInvalidEmployee);
            }
            catch (Exception e)
            {
                var validationException = ((FluentValidation.ValidationException)e);
                Assert.Single(validationException.Errors);
                Assert.Contains(EmployeeControllerTest.InvalidDescriptionMessage, validationException.Message);
            }
        }
    }
}