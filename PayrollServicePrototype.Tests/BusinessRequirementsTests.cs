using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayrollServicePrototype.Controllers;
using PayrollServicePrototype.Models;
using System.Collections.Generic;
using System.Linq;

namespace PayrollServicePrototype.Tests
{
    [TestClass]
    public class BusinessRequirementsTests
    {
        List<Country> countries = new List<Country>()
        {
            new Country("SPA", new List<Tax>()
            {
                new ProgressiveTax(0.25, 600, 0.4), new ProgressiveTax(0.07, 500, 0.08), new FlatTax(0.04)
            }),
            new Country("ITA", new List<Tax>()
            {
                new FlatTax(0.25), new FlatTax(0.0919)
            }),
            new Country("DEU", new List<Tax>()
            {
                new ProgressiveTax(0.25, 400, 0.32), new FlatTax(0.02)
            })
        };

        /// <summary>
        /// Business requirement 1
        /// </summary>
        [TestMethod]
        public void DEU_ReturnsCorrectGrossSalary()
        {
            var repository = new EmployeeRepository(new List<Employee>()
            {
                new Employee(countries.Where(country => country.CountryCode == "DEU").FirstOrDefault(), 40, 10)
            });

            var controller = new PayrollServiceController(repository);

            var employee = controller.Get("DEU").FirstOrDefault();

            Assert.AreEqual(400, employee.GrossSalary);
        }

        /// <summary>
        /// Business requiremnt 2-4
        /// </summary>
        [TestMethod]
        public void SPA_BelowIncomeTax_BelowSocial_ReturnsCorrectDeduction()
        {
            var repository = new EmployeeRepository(new List<Employee>()
            {
                new Employee(countries.Where(country => country.CountryCode == "SPA").FirstOrDefault(), 40, 10)
            });

            var controller = new PayrollServiceController(repository);

            var employee = controller.Get("SPA").FirstOrDefault();
            var gs = employee.GrossSalary;

            Assert.AreEqual((gs * 0.25m) + (gs * 0.07m) + (gs * 0.04m), employee.TaxesDeductions);
        }

        /// <summary>
        /// Business requiremnt 2-4
        /// </summary>
        [TestMethod]
        public void SPA_BelowIncomeTax_AboveSocial_ReturnsCorrectDeduction()
        {
            var repository = new EmployeeRepository(new List<Employee>()
            {
                new Employee(countries.Where(country => country.CountryCode == "SPA").FirstOrDefault(), 55, 10)
            });

            var controller = new PayrollServiceController(repository);

            var employee = controller.Get("SPA").FirstOrDefault();
            var gs = employee.GrossSalary;

            Assert.AreEqual((gs * 0.25m) + (500 * 0.07m + (gs - 500m) * 0.08m) + (gs * 0.04m), employee.TaxesDeductions);
        }

        /// <summary>
        /// Business requiremnt 2-4
        /// </summary>
        [TestMethod]
        public void SPA_AboveIncomeTax_AboveSocial_ReturnsCorrectDeduction()
        {
            var repository = new EmployeeRepository(new List<Employee>()
            {
                new Employee(countries.Where(country => country.CountryCode == "SPA").FirstOrDefault(), 70, 10)
            });

            var controller = new PayrollServiceController(repository);

            var employee = controller.Get("SPA").FirstOrDefault();
            var gs = employee.GrossSalary;

            Assert.AreEqual((600 * 0.25m + (gs - 600) * 0.4m) + (500 * 0.07m + (gs - 500) * 0.08m) + (gs * 0.04m), employee.TaxesDeductions);
        }

        /// <summary>
        /// Business requiremnt 5-6
        /// </summary>
        [TestMethod]
        public void ITA_ReturnsCorrectDeduction()
        {
            var repository = new EmployeeRepository(new List<Employee>()
            {
                new Employee(countries.Where(country => country.CountryCode == "ITA").FirstOrDefault(), 70, 10)
            });

            var controller = new PayrollServiceController(repository);

            var employee = controller.Get("ITA").FirstOrDefault();
            var gs = employee.GrossSalary;

            Assert.AreEqual((gs * 0.25m) + (gs * 0.0919m), employee.TaxesDeductions);
        }

        /// <summary>
        /// Business requiremnt 7-8
        /// </summary>
        [TestMethod]
        public void DEU_BelowIncome_ReturnsCorrectDeduction()
        {
            var repository = new EmployeeRepository(new List<Employee>()
            {
                new Employee(countries.Where(country => country.CountryCode == "DEU").FirstOrDefault(), 35, 10)
            });

            var controller = new PayrollServiceController(repository);

            var employee = controller.Get("DEU").FirstOrDefault();
            var gs = employee.GrossSalary;

            Assert.AreEqual((gs * 0.25m) + (gs * 0.02m), employee.TaxesDeductions);
        }

        /// <summary>
        /// Business requiremnt 7-8
        /// </summary>
        [TestMethod]
        public void DEU_AboveIncome_ReturnsCorrectDeduction()
        {
            var repository = new EmployeeRepository(new List<Employee>()
            {
                new Employee(countries.Where(country => country.CountryCode == "DEU").FirstOrDefault(), 35, 30)
            });

            var controller = new PayrollServiceController(repository);

            var employee = controller.Get("DEU").FirstOrDefault();
            var gs = employee.GrossSalary;

            Assert.AreEqual((400 * 0.25m + (gs - 400) * 0.32m) + (gs * 0.02m), employee.TaxesDeductions);
        }

        /// <summary>
        /// Check NetSalary
        /// </summary>
        [TestMethod]
        public void DEU_ReturnsCorrectNetSalary()
        {
            var repository = new EmployeeRepository(new List<Employee>()
            {
                new Employee(countries.Where(country => country.CountryCode == "DEU").FirstOrDefault(), 35, 30)
            });

            var controller = new PayrollServiceController(repository);

            var employee = controller.Get("DEU").FirstOrDefault();
            var gs = employee.GrossSalary;

            Assert.AreEqual(gs - ((400 * 0.25m + (gs - 400) * 0.32m) + (gs * 0.02m)), employee.NetSalary);
        }
    }
}
