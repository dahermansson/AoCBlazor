using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AoC.InputHandling.Interfaces;

namespace AoC.InputHandling.Extensions;

public static class StartupExtensions
{
    public static IHostBuilder ConfigureInputHandler(this IHostBuilder hostBuilder) =>
        hostBuilder.ConfigureServices(services => {
            services.AddHttpClient("aocclient", client =>
            {
                client.BaseAddress = new Uri("https://adventofcode.com");
                client.DefaultRequestHeaders.Add("Cookie",
                $"session={Environment.GetEnvironmentVariable("AOC_Session", EnvironmentVariableTarget.User)}");
            });
                services.AddScoped<IAOCDownloadService, AOCDownloadService>();
                services.AddScoped<IInputHandler, LocalInputHandler>();
            });
};

