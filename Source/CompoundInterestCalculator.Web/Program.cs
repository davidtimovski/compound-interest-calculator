using CompoundInterestCalculator.Web.Services;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CompoundInterestCalculator.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddTransient<ICalculatorService, CalculatorService>();
            builder.RootComponents.Add<App>("app");

            var host = builder.Build();
            await host.RunAsync();
        }
    }
}
