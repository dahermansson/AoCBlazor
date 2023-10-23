using Microsoft.AspNetCore.Components;
using AoC.Utils;
using System.Diagnostics;
using AoC.Solvers;
namespace AoC.Web.Components.Pages;

public partial class Puzzles
{
    [Parameter]
    public int Year { get; set; }
    private List<string> Days { get; set; } = default!;
    public string ActiveDay { get; set; } = "";
    public IDay ActivePuzzle { get; set; } = default!;
    public string Star1 { get; set; } = string.Empty;
    public long Star1Ms {get; set; } = -1;
    public long Star2Ms {get; set; } = -1;

    public string Star2 { get; set; } = string.Empty;
    protected override Task OnInitializedAsync()
    {
        Days = SolversManager.GetDays(2016);
        return Task.CompletedTask;
    }

    protected Task ActivateDay(string day)
    {
        ActiveDay = day;
        ActivePuzzle = SolversManager.GetDay(Year, day) ?? throw new Exception("Day missing");
        Star1 = string.Empty;
        Star2 = string.Empty;
        Star1Ms = -1;
        Star2Ms = -1;
        return Task.CompletedTask;
    }
    protected Task RunStar1()
    {
        var sw = new Stopwatch();
        sw.Start();
        var res = ActivePuzzle.Star1();
        sw.Stop();
        Star1Ms = sw.ElapsedMilliseconds;
        Star1 = res != -1 ? res.ToString() : ActivePuzzle.Output;
        sw.Reset();
        return Task.CompletedTask;
    }

    protected Task RunStar2()
    {
        var sw = new Stopwatch();
        sw.Start();
        var res = ActivePuzzle.Star2();
        sw.Stop();
        Star2Ms = sw.ElapsedMilliseconds;
        Star2 = res != -1 ? res.ToString() : ActivePuzzle.Output;
        sw.Reset();
        return Task.CompletedTask;
    }

}