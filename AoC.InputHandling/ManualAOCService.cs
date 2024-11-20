using AoC.InputHandling.Interfaces;

namespace AoC.InputHandling;

public class ManualAOCService : IAOCDownloadService
{
    public Task<string> DownloadInput(int year, int day) => Task.FromResult("");
}