using CompoundInterestCalculator.Web.Models;

namespace CompoundInterestCalculator.Web.Services;

public class CalculatorService : ICalculatorService
{
    private readonly Dictionary<CompoundInterval, int> _compoundsPerYear = new()
    {
        { CompoundInterval.Monthly, 12 },
        { CompoundInterval.Quarterly, 4 },
        { CompoundInterval.SemiAnnually, 2 },
        { CompoundInterval.Annually, 1 }
    };

    public CalculationResult[] Calculate(double baseAmount, double interestRatePercent, CompoundInterval compoundInterval, double monthlyDeposit, short calcPeriodYrs)
    {
        var result = new CalculationResult[calcPeriodYrs];

        double interestRate = interestRatePercent / 100d;

        double totalDeposits = 0;
        double totalInterest = 0;
        double balance = baseAmount;

        for (var year = 0; year < calcPeriodYrs; year++)
        {
            double newBalance;
            if (interestRate > 0)
            {
                newBalance = CalculateCompoundInterest(balance, interestRate, _compoundsPerYear[compoundInterval], monthlyDeposit);
            }
            else
            {
                newBalance = balance + monthlyDeposit * 12;
            }

            double yearInterest = newBalance - balance - monthlyDeposit * 12;
            totalDeposits += monthlyDeposit * 12;
            totalInterest += yearInterest;
            balance = newBalance;

            result[year] = new CalculationResult(
                Year: year + 1,
                TotalDeposits: Math.Round(totalDeposits, 2),
                YearInterest: Math.Round(yearInterest, 2),
                TotalInterest: Math.Round(totalInterest, 2),
                Balance: Math.Round(balance, 2)
            );
        }

        return result;
    }

    private static double CalculateCompoundInterest(double principal, double rate, int compoundsPerYear, double monthlyDeposit)
    {
        // P(1+r/n)^(nt)
        var futureValue = FutureValueOfASeries(rate, compoundsPerYear, monthlyDeposit);
        return (principal * Math.Pow(1 + rate / compoundsPerYear, compoundsPerYear * 1)) + futureValue;
    }

    private static double FutureValueOfASeries(double rate, int compoundsPerYear, double monthlyDeposit)
    {
        const double depositsPerYear = 12;

        // rate = ((1+r/n)^(n/p))-1
        // r = interest rate
        // n = the number of compound periods per year
        // p = the number of payment periods per year
        var rateForPaymentPeriod = Math.Pow(1 + rate / compoundsPerYear, compoundsPerYear / depositsPerYear) - 1;

        // F=A(((1+rate)^(nper)-1)/rate)
        // A = the payment amount, added to the principal at the end of each period
        // rate = the rate per payment period
        // nper = the number of payments
        return monthlyDeposit * (Math.Pow(1 + rateForPaymentPeriod, depositsPerYear) - 1) / rateForPaymentPeriod;
    }
}
