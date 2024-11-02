namespace AoC.InputHandling.Interfaces;

public interface IAOCDownloadService
{
    public Task<string> DownloadInput(int year, int day);
}