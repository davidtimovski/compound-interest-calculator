using CompoundInterestCalculator.Web.Models;
using CompoundInterestCalculator.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CompoundInterestCalculator.Web.Pages;

public class IndexBase : ComponentBase
{
    [Inject] ICalculatorService CalculatorService { get; set; }
    [Inject] IJSRuntime JSRuntime { get; set; }

    protected CalculationInput input = new();
    protected Dictionary<string, CompoundInterval> compoundIntervals = new()
    {
        { "Monthly", CompoundInterval.Monthly },
        { "Quarterly", CompoundInterval.Quarterly },
        { "Semi-annually", CompoundInterval.SemiAnnually },
        { "Annually", CompoundInterval.Annually }
    };
    protected bool totalDepositsColVisible;
    protected CalculationResult[] result = Array.Empty<CalculationResult>();

    protected async Task Calculate()
    {
        if (input.IsValid())
        {
            totalDepositsColVisible = input.MonthlyDeposit.HasValue && input.MonthlyDeposit.Value > 0;

            input.BaseAmount ??= 0;
            input.MonthlyDeposit ??= 0;

            result = CalculatorService.Calculate(
                input.BaseAmount.Value,
                input.InterestRatePercent.Value,
                input.CompoundInterval,
                input.MonthlyDeposit.Value,
                input.CalcPeriodYrs.Value);

            await JSRuntime.InvokeVoidAsync("jsFunctions.scrollToResults");
        }
    }
}
