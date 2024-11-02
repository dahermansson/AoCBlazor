using AoC.InputHandling.Interfaces;

namespace AoC.InputHandling;

public class AOCDownloadService(IHttpClientFactory httpClient) : IAOCDownloadService
{
    public async Task<string> DownloadInput(int year, int day)
    {
        var client = httpClient.CreateClient("aocclient");
        return await client.GetStringAsync($"{client.BaseAddress}{year}/day/{day}/input");
    }
}
