using AoC.InputHandling.Interfaces;
using Microsoft.Extensions.Logging;

namespace AoC.InputHandling;

public class AOCDownloadService(IHttpClientFactory httpClient, ILogger<AOCDownloadService> logger) : IAOCDownloadService
{
    public async Task<string> DownloadInput(int year, int day)
    {
        try
        {
            var client = httpClient.CreateClient("aocclient");
            return await client.GetStringAsync($"{client.BaseAddress}{year}/day/{day}/input");
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Fail to download input, fix your AOC_SESSION Environment variable");
            return "";
        }
    }
}
