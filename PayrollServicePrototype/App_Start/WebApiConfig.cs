using PayrollServicePrototype.Models;
using PayrollServicePrototype.Util;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web.Http;
using Unity;

namespace PayrollServicePrototype
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // JSON Default
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            var countryProvider = CountryProvider.Instance;
            countryProvider.Countries.Add(new Country("SPA", new List<Tax>()
                {
                    new ProgressiveTax(0.25, 600, 0.4), new ProgressiveTax(0.07, 500, 0.08), new FlatTax(0.04)
                }));
            countryProvider.Countries.Add(new Country("ITA", new List<Tax>()
                {
                    new FlatTax(0.25), new FlatTax(0.0919)
                }));
            countryProvider.Countries.Add(new Country("DEU", new List<Tax>()
                {
                    new ProgressiveTax(0.25, 400, 0.32), new FlatTax(0.02)
                }));

            // Dependency Injection Container
            var container = new UnityContainer();
            container.RegisterInstance<IEmployeeRepository>(new EmployeeRepository(new List<Employee>()
            {
                new Employee(countryProvider.CountryByCode("DEU"), 20, 10), // from description
                new Employee(countryProvider.CountryByCode("DEU"), 35, 30), // example
                new Employee(countryProvider.CountryByCode("SPA"), 35, 30), // example
                new Employee(countryProvider.CountryByCode("ITA"), 35, 30), // example
            }));
            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{countryCode}",
                defaults: new { countryCode = RouteParameter.Optional }
            );
        }
    }
}
