using Newtonsoft.Json;
using System;

namespace PayrollServicePrototype.Models
{
    public class Employee
    {
        // ID, name etc. not in requirements
        [JsonProperty("countryCode")]
        public string CountryCode { get { return Country.CountryCode; } }
        [JsonProperty("grossSalary")]
        public decimal GrossSalary { get; set; }
        [JsonProperty("taxesDeductions")]
        public decimal TaxesDeductions { get; set; }
        [JsonProperty("netSalary")]
        public decimal NetSalary { get; set; }
        private Country Country { get; }

        public Employee(Country country, decimal hoursWorked, decimal hourlyRate)
        {
            /// TODO: check for only positive hours/rates
            Country = country ?? throw new ArgumentNullException(nameof(country));

            GrossSalary = hoursWorked * hourlyRate;

            TaxesDeductions = Country.CalculateDeductions(GrossSalary);

            NetSalary = GrossSalary - TaxesDeductions;
        }
    }
}
