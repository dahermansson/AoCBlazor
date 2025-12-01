using AoC.Solvers;
using AoC.Solvers.Extensions;
using AoC.InputHandling.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureInputHandler()
    .ConfigureSolversManager()
    .ConfigureLogging(c => c.AddConsole())
    .Build();

const int YEAR = SolversManager.Y2025;
string dayToRun = 1.ToString("D2");

using var scope = host.Services.CreateScope();
var solverManager = scope.ServiceProvider.GetRequiredService<SolversManager>();

var day = await solverManager.GetDay(YEAR, dayToRun);
if (day != null)
{
    var star1 = day.Star1();
    var output = star1 == -1 ? day.Output : star1.ToString();

    Console.WriteLine($"Star1: {output}");
    var star2 = day.Star2();
    output = star2 == -1 ? day.Output : star2.ToString();

    Console.WriteLine($"Star2: {output}");
}
