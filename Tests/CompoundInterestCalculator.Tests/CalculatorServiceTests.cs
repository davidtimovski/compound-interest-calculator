using System.Linq;
using CompoundInterestCalculator.Web.Models;
using CompoundInterestCalculator.Web.Services;
using Xunit;

namespace CompoundInterestCalculator.Tests;

public class CalculatorServiceTests
{
    private const double BaseAmount = 5000;
    private const double InterestRatePercent = 7.32;
    private const double MonthlyDeposit = 180;
    private const short CalPeriodYrs = 5;

    private readonly CalculatorService _sut = new();

    [Theory]
    [InlineData(0, InterestRatePercent, CompoundInterval.Monthly, MonthlyDeposit, CalPeriodYrs, 10800, 831.24, 2194.02, 12994.02)]
    [InlineData(BaseAmount, InterestRatePercent, CompoundInterval.Monthly, MonthlyDeposit, CalPeriodYrs, 10800, 1338.09, 4395.79, 20195.79)]
    public void CalculationIsCorrect_BaseAmount(double baseAmount, double interestRatePercent, CompoundInterval compoundInterval, 
        double monthlyDeposit, short calcPeriodYrs, double lastExpectedTotalDeposits, double lastExpectedYearInterest, 
        double lastExpectedTotalInterest, double lastExpectedBalance)
    {
        CalculationResult[] result = _sut.Calculate(baseAmount, interestRatePercent, compoundInterval, monthlyDeposit, calcPeriodYrs);
        CalculationResult lastYear = result.Last();

        Assert.Equal(lastExpectedTotalDeposits, lastYear.TotalDeposits);
        Assert.Equal(lastExpectedYearInterest, lastYear.YearInterest);
        Assert.Equal(lastExpectedTotalInterest, lastYear.TotalInterest);
        Assert.Equal(lastExpectedBalance, lastYear.Balance);
    }

    [Theory]
    [InlineData(BaseAmount, 0, CompoundInterval.Monthly, MonthlyDeposit, CalPeriodYrs, 10800, 0, 0, 15800)]
    [InlineData(BaseAmount, InterestRatePercent, CompoundInterval.Monthly, MonthlyDeposit, CalPeriodYrs, 10800, 1338.09, 4395.79, 20195.79)]
    public void CalculationIsCorrect_InterestRate(double baseAmount, double interestRatePercent, CompoundInterval compoundInterval,
        double monthlyDeposit, short calcPeriodYrs, double lastExpectedTotalDeposits, double lastExpectedYearInterest,
        double lastExpectedTotalInterest, double lastExpectedBalance)
    {
        CalculationResult[] result = _sut.Calculate(baseAmount, interestRatePercent, compoundInterval, monthlyDeposit, calcPeriodYrs);
        CalculationResult lastYear = result.Last();

        Assert.Equal(lastExpectedTotalDeposits, lastYear.TotalDeposits);
        Assert.Equal(lastExpectedYearInterest, lastYear.YearInterest);
        Assert.Equal(lastExpectedTotalInterest, lastYear.TotalInterest);
        Assert.Equal(lastExpectedBalance, lastYear.Balance);
    }

    [Theory]
    [InlineData(BaseAmount, InterestRatePercent, CompoundInterval.Monthly, MonthlyDeposit, CalPeriodYrs, 10800, 1338.09, 4395.79, 20195.79)]
    [InlineData(BaseAmount, InterestRatePercent, CompoundInterval.Quarterly, MonthlyDeposit, CalPeriodYrs, 10800, 1328.18, 4365.09, 20165.09)]
    [InlineData(BaseAmount, InterestRatePercent, CompoundInterval.SemiAnnually, MonthlyDeposit, CalPeriodYrs, 10800, 1313.65, 4320.07, 20120.07)]
    [InlineData(BaseAmount, InterestRatePercent, CompoundInterval.Annually, MonthlyDeposit, CalPeriodYrs, 10800, 1285.76, 4233.47, 20033.47)]
    public void CalculationIsCorrect_CompoundInterval(double baseAmount, double interestRatePercent, CompoundInterval compoundInterval,
        double monthlyDeposit, short calcPeriodYrs, double lastExpectedTotalDeposits, double lastExpectedYearInterest,
        double lastExpectedTotalInterest, double lastExpectedBalance)
    {
        CalculationResult[] result = _sut.Calculate(baseAmount, interestRatePercent, compoundInterval, monthlyDeposit, calcPeriodYrs);
        CalculationResult lastYear = result.Last();

        Assert.Equal(lastExpectedTotalDeposits, lastYear.TotalDeposits);
        Assert.Equal(lastExpectedYearInterest, lastYear.YearInterest);
        Assert.Equal(lastExpectedTotalInterest, lastYear.TotalInterest);
        Assert.Equal(lastExpectedBalance, lastYear.Balance);
    }

    [Theory]
    [InlineData(BaseAmount, InterestRatePercent, CompoundInterval.Monthly, 0, CalPeriodYrs, 0, 506.85, 2201.76, 7201.76)]
    [InlineData(BaseAmount, InterestRatePercent, CompoundInterval.Monthly, MonthlyDeposit, CalPeriodYrs, 10800, 1338.09, 4395.79, 20195.79)]
    public void CalculationIsCorrect_MonthlyDeposit(double baseAmount, double interestRatePercent, CompoundInterval compoundInterval,
        double monthlyDeposit, short calcPeriodYrs, double lastExpectedTotalDeposits, double lastExpectedYearInterest,
        double lastExpectedTotalInterest, double lastExpectedBalance)
    {
        CalculationResult[] result = _sut.Calculate(baseAmount, interestRatePercent, compoundInterval, monthlyDeposit, calcPeriodYrs);
        CalculationResult lastYear = result.Last();

        Assert.Equal(lastExpectedTotalDeposits, lastYear.TotalDeposits);
        Assert.Equal(lastExpectedYearInterest, lastYear.YearInterest);
        Assert.Equal(lastExpectedTotalInterest, lastYear.TotalInterest);
        Assert.Equal(lastExpectedBalance, lastYear.Balance);
    }

    [Theory]
    [InlineData(BaseAmount, InterestRatePercent, CompoundInterval.Monthly, MonthlyDeposit, 2, 4320, 650.28, 1102.77, 10422.77)]
    [InlineData(BaseAmount, InterestRatePercent, CompoundInterval.Monthly, MonthlyDeposit, CalPeriodYrs, 10800, 1338.09, 4395.79, 20195.79)]
    public void CalculationIsCorrect_CalPeriodYrs(double baseAmount, double interestRatePercent, CompoundInterval compoundInterval,
        double monthlyDeposit, short calcPeriodYrs, double lastExpectedTotalDeposits, double lastExpectedYearInterest,
        double lastExpectedTotalInterest, double lastExpectedBalance)
    {
        CalculationResult[] result = _sut.Calculate(baseAmount, interestRatePercent, compoundInterval, monthlyDeposit, calcPeriodYrs);
        CalculationResult lastYear = result.Last();

        Assert.Equal(lastExpectedTotalDeposits, lastYear.TotalDeposits);
        Assert.Equal(lastExpectedYearInterest, lastYear.YearInterest);
        Assert.Equal(lastExpectedTotalInterest, lastYear.TotalInterest);
        Assert.Equal(lastExpectedBalance, lastYear.Balance);
    }
}
