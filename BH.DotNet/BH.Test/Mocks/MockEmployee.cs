using BH.Api.Models;

namespace BH.Test.Mocks
{
    internal class MockEmployee
    {
        public static Employee FirstNameInvalidEmployee
            = new Employee()
            {
                Id = "1",
                FirstName = "A",
                LastName = "Modarage",
                Description = "Description"
            };

        public static Employee LastNameInvalidEmployee
            = new Employee()
            {
                Id = "1",
                FirstName = "Parami",
                LastName = "A",
                Description = "Description"
            };

        public static Employee DescriptionInvalidEmployee
            = new Employee()
            {
                Id = "1",
                FirstName = "Parami",
                LastName = "Modarage",
                Description = "A"
            };
    }
}
