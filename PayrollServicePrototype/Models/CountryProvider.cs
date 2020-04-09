using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollServicePrototype.Models
{
    public class CountryProvider
    {
        private static readonly Lazy<CountryProvider> lazy = new Lazy<CountryProvider>(() => new CountryProvider());

        public static CountryProvider Instance { get { return lazy.Value; } }

        public List<Country> Countries { get; set; } = new List<Country>();

        private CountryProvider()
        {
        }

        public Country CountryByCode(string code)
        {
            return Countries.Where(country => country.CountryCode == code).FirstOrDefault();
        }
    }
}
