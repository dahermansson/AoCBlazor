using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AoC.Solvers.Extensions;

public static class StartupExtensions
{
    public static IHostBuilder ConfigureSolversManager(this IHostBuilder hostBuilder) =>
    hostBuilder.ConfigureServices(services => 
        services.AddScoped<SolversManager>()
        );
};

