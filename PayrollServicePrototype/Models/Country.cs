using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PayrollServicePrototype.Models
{
    public class Country
    {
        public string CountryCode { get; }
        private List<Tax> Taxes { get; }

        public Country(string countryCode, List<Tax> taxes)
        {
            CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
            Taxes = taxes ?? throw new ArgumentNullException(nameof(taxes));
        }

        public decimal CalculateDeductions(decimal grossSalary)
        {
            decimal deductions = 0;

            foreach (var tax in Taxes)
            {
                var deduction = tax.GetDeduction(grossSalary);
                Trace.WriteLine($"Deducting {deduction}");
                deductions += deduction;
            }

            return deductions;
        }
    }
}
