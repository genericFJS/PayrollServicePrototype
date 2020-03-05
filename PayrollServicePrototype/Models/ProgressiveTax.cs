namespace PayrollServicePrototype.Models
{
    public class ProgressiveTax : Tax
    {
        public decimal SalaryThreshold { get; }
        public double ThresholdTaxRate { get; }

        public ProgressiveTax(double taxRate, decimal salaryThreshold, double thresholdTaxRate)
        {
            TaxRate = taxRate;
            SalaryThreshold = salaryThreshold;
            ThresholdTaxRate = thresholdTaxRate;
        }

        public override decimal GetDeduction(decimal grossSalary)
        {
            decimal deduction = 0;

            var salaryBelowThreshold = grossSalary > SalaryThreshold ? SalaryThreshold : grossSalary;
            var salaryAboveThreshold = grossSalary > SalaryThreshold ? grossSalary - SalaryThreshold : 0;

            deduction += salaryBelowThreshold * (decimal)TaxRate;
            deduction += salaryAboveThreshold * (decimal)ThresholdTaxRate;

            return deduction;
        }
    }
}
