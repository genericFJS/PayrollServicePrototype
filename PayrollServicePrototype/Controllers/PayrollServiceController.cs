using PayrollServicePrototype.Models;
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

        public IHttpActionResult Get(string countryCode)
        {
            var product = Repository.GetByCountry(countryCode);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
