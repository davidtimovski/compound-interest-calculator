using CompoundInterestCalculator.Web.Models;

namespace CompoundInterestCalculator.Web.Services
{
    public interface ICalculatorService
    {
        CalculationResult[] Calculate(double baseAmount, double interestRate, CompoundInterval compoundInterval, double monthlyDeposit, short calcPeriodYrs);
    }
}
