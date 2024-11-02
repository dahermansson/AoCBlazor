using AoC.Solvers;
using AoC.Solvers.Extensions;
using AoC.InputHandling.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureInputHandler()
    .ConfiguresolversManager()
    .Build();

const int YEAR = SolversManager.Y2016;
string dayToRun = 3.ToString("D2");

var solverManager = host.Services.GetRequiredService<SolversManager>();

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
