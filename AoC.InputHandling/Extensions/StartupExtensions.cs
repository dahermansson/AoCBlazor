using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AoC.InputHandling.Interfaces;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;

namespace AoC.InputHandling.Extensions;

public static class StartupExtensions
{
    private static readonly string ENVIRONMENT_VARIABLE_AOC_SESSION_NAME = "AOC_SESSION";
    private static readonly string AZURE_STORAGEFILE_CONNECTIONSTRING_NAME = "AZURE_STORAGEFILE_CONNECTIONSTRING";
    public static IHostBuilder ConfigureInputHandler(this IHostBuilder hostBuilder) =>
        hostBuilder.ConfigureServices((settings, services) =>
        {
            if(settings.Configuration.GetValue("UseInputDownloader", true))
            {
                var AOC_Session = Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE_AOC_SESSION_NAME)
                    ?? throw new Exception("Misisng AOC_SESSION in Environment variables");

                services.AddHttpClient("aocclient", client =>
                {
                    client.BaseAddress = new Uri("https://adventofcode.com");
                    client.DefaultRequestHeaders.UserAgent.TryParseAdd("github.com/dahermansson/AoCBlazor");
                    client.DefaultRequestHeaders.Add("Cookie", $"session={AOC_Session}");
                });

                services.AddScoped<IAOCDownloadService, AOCDownloadService>();
            }
            else 
                services.AddScoped<IAOCDownloadService, ManualAOCService>();



            var azureStorageConnectionstring = new []{settings.Configuration.GetConnectionString("AzureStorage"), Environment.GetEnvironmentVariable(AZURE_STORAGEFILE_CONNECTIONSTRING_NAME)}.LastOrDefault(t => !string.IsNullOrEmpty(t));
            if (settings.HostingEnvironment.IsDevelopment())
            {
                if (!string.IsNullOrEmpty(azureStorageConnectionstring))
                    services.ConfigureAzureStorage(azureStorageConnectionstring);
                else
                    services.ConfigureLocalStorage(settings);
            }
            else
                services.ConfigureAzureStorage(azureStorageConnectionstring ?? throw new KeyNotFoundException("AzureStorage is missing"));
        });

    private static void ConfigureAzureStorage(this IServiceCollection services, string connectionString)
    {
        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddFileServiceClient(connectionString);
        });
        services.AddSingleton(new AzureStorageInputHandlerOptions("adventofcodeinputs"));
        services.AddScoped<IInputHandler, AzureStorageInputHandler>();
    }

    private static void ConfigureLocalStorage(this IServiceCollection services, HostBuilderContext settings)
    {
        services.AddSingleton(new LocalInputHandlerOptions(settings.Configuration.GetValue<string?>("InputPath") ?? "..\\Inputs"));
        services.AddScoped<IInputHandler, LocalInputHandler>();
    }
};

