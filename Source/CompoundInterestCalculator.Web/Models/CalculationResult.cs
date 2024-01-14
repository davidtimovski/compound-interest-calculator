namespace CompoundInterestCalculator.Web.Models;

public sealed record CalculationResult(int Year, double TotalDeposits, double YearInterest, double TotalInterest, double Balance);
