namespace PayrollServicePrototype.Models
{
    public abstract class Tax
    {
        public double TaxRate { get; protected set; }

        public abstract decimal GetDeduction(decimal grossSalary);
    }
}
