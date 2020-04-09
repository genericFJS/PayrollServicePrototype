using PayrollServicePrototype.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace PayrollServicePrototype.Controllers
{
    public class PayrollServiceController : ApiController
    {
        // This line of code is a problem!
        IEmployeeRepository Repository { get; }
        public PayrollServiceController(IEmployeeRepository repository)
        {
            Repository = repository;
        }

        public List<Employee> Get(string countryCode)
        {
            return Repository.GetByCountry(countryCode);
        }

        public Employee Get(string countryCode, decimal hoursWorked, decimal hourlyRate)
        {
            return new Employee(CountryProvider.Instance.CountryByCode(countryCode), hoursWorked, hourlyRate);
        }
    }
}
