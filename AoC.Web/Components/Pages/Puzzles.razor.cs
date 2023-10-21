using Microsoft.AspNetCore.Components;
using AoC.Utils;
namespace AoC.Web.Components.Pages;

public partial class Puzzles
{
    [Parameter]
    public int Year { get; set; }
    private Dictionary<int, IDay?> Days { get; set; } = default!;
    public int ActiveDay { get; set; } = default;
    public IDay ActivePuzzle { get; set; } = default!;
    public string Star1 { get; set; } = string.Empty;

    public string Star2 { get; set; } = string.Empty;

    protected override Task OnInitializedAsync()
    {
        Days = AoC.Puzzles.Puzzles.GetDays(2016);
        return Task.CompletedTask;
    }

    protected Task ActivateDay(int day)
    {
        ActiveDay = day;
        ActivePuzzle = Days[ActiveDay]?? throw new Exception("Missing Day to collect star");
        Star1 = string.Empty;
        Star2 = string.Empty;
        return Task.CompletedTask;
    }
    protected Task RunStar1()
    {
        var res = ActivePuzzle.Star1();
        Star1 = res != -1 ? res.ToString() : ActivePuzzle.Output;
        return Task.CompletedTask;
    }

    protected Task RunStar2()
    {
        var res = ActivePuzzle.Star2();
        Star2 = res != -1 ? res.ToString() : ActivePuzzle.Output;
        return Task.CompletedTask;
    }

}