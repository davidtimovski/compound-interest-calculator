namespace CompoundInterestCalculator.Web.Models;

public sealed class CalculationInput
{
    public double? BaseAmount { get; set; }
    public double? InterestRatePercent { get; set; }
    public CompoundInterval CompoundInterval { get; set; }
    public double? MonthlyDeposit { get; set; }
    public short? CalcPeriodYrs { get; set; }


    public bool IsValid()
    {
        if (BaseAmount.HasValue && BaseAmount.Value < 0)
        {
            return false;
        }

        if (!InterestRatePercent.HasValue || InterestRatePercent.Value < 0.1)
        {
            return false;
        }

        if (MonthlyDeposit.HasValue && MonthlyDeposit.Value < 0)
        {
            return false;
        }

        if (!CalcPeriodYrs.HasValue || CalcPeriodYrs.Value < 1 || CalcPeriodYrs > 100)
        {
            return false;
        }

        return true;
    }
}
