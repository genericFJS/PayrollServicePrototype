using System.Collections.Generic;
using System.Linq;

namespace PayrollServicePrototype.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> Employees { get; }

        public EmployeeRepository(List<Employee> employees)
        {
            Employees = employees;
        }

        public List<Employee> GetByCountry(string countryCode)
        {
            return Employees.Where(employee => employee.CountryCode == countryCode).ToList();
        }
    }
}
