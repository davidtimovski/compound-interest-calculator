namespace CompoundInterestCalculator.Web.Models
{
    public class CalculationResult
    {
        public int Year { get; set; }
        public double TotalDeposits { get; set; }
        public double YearInterest { get; set; }
        public double TotalInterest { get; set; }
        public double Balance { get; set; }
    }
}
