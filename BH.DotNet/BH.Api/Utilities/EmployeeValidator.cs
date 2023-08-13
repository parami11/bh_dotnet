using BH.Api.Models;
using FluentValidation;

namespace BH.Api.Utilities
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.FirstName).Length(2, 50);
        }
    }
}
