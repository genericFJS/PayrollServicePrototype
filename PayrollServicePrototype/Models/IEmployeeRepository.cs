using System.Collections.Generic;

namespace PayrollServicePrototype.Models
{
    public interface IEmployeeRepository
    {
        List<Employee> GetByCountry(string countryCode);
    }
}
