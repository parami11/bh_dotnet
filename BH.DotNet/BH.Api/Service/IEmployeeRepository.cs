using BH.Api.Models;

namespace BH.Api.Service
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees(int page, int limit);

        Task<Employee> GetEmployee(int id);
        
        Task<Employee> AddEmployee(Employee employee);
    }
}
