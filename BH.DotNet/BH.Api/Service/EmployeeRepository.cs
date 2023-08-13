using BH.Api.Models;
using Microsoft.Azure.Cosmos;

namespace BH.Api.Service
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Container _container;

        public EmployeeRepository()
        {

        }

        public EmployeeRepository(
            CosmosClient cosmosClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid().ToString();
            var item = await _container.CreateItemAsync<Employee>(employee, new PartitionKey(employee.Id));
            return item;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetEmployees(int page, int limit)
        {
            var offset = (page - 1) * limit;
            var cosmosQuery = $"SELECT * FROM c OFFSET {offset} LIMIT {limit}";
            var query = _container.GetItemQueryIterator<Employee>(new QueryDefinition(cosmosQuery));

            List<Employee> result = new List<Employee>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                result.AddRange(response);
            }

            return result;
        }
    }
}
