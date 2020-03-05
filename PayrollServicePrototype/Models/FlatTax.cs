namespace PayrollServicePrototype.Models
{
    public class FlatTax : Tax
    {
        public FlatTax(double taxRate)
        {
            TaxRate = taxRate;
        }

        public override decimal GetDeduction(decimal grossSalary)
        {
            return grossSalary * (decimal)TaxRate;
        }
    }
}
