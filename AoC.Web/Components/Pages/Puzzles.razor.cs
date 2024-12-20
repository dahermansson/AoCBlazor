using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using AoC.Solvers.Interface;
using AoC.Solvers;
namespace AoC.Web.Components.Pages;

public partial class Puzzles: ComponentBase
{
    [Parameter]
    public int Year { get; set; }
    private List<string> Days { get; set; } = default!;
    public string ActiveDay { get; set; } = "";
    public IDay ActivePuzzle { get; set; } = default!;
    public string Star1 { get; set; } = string.Empty;
    public long Star1Ms {get; set; } = -1;
    public long Star2Ms {get; set; } = -1;
    private int _currentYear = -1;
    private string Star1Button { get; set; } = "[Run]";
    private string Star2Button { get; set; }= "[Run]";
    private bool Star1ButtonStatus { get; set; } = false;
    private bool Star2ButtonStatus { get; set; } = false;
    private string SourceLink { get; set; } = string.Empty;
    private readonly string BaseUrlSource = "https://raw.githubusercontent.com/dahermansson/AoCBlazor/main/AoC.Solvers/Y{0}/Day{1}.cs";
    private readonly string BaseUrlPuzzle = "https://adventofcode.com/{0}/day/{1}";
    private string PuzzleLink { get; set; } = string.Empty;
    public string Star2 { get; set; } = string.Empty;
    
    [Inject]
    private SolversManager solversManager {get; init;} = default!;

    protected override Task OnParametersSetAsync()
    {
        if (_currentYear != Year)
        {
            _currentYear = Year;
            ActiveDay = "";
            Days = SolversManager.GetDays(Year);
        }
        return Task.CompletedTask;
    }

    protected async Task ActivateDay(string day)
    {
        ActiveDay = day;
        ActivePuzzle = await solversManager.GetDay(Year, day) ?? throw new Exception("Day missing");
        Star1 = string.Empty;
        Star2 = string.Empty;
        Star1Ms = -1;
        Star2Ms = -1;
        SourceLink = string.Format(BaseUrlSource, Year, ActiveDay);
        PuzzleLink = string.Format(BaseUrlPuzzle, Year, int.Parse(ActiveDay));
    }
    protected async Task RunStar1()
    {
        Star1Button = "Running...";
        Star1ButtonStatus = true;
        var sw = new Stopwatch();
        sw.Start();
        var res = await Task.Run(() => ActivePuzzle.Star1());
        sw.Stop();
        Star1Ms = sw.ElapsedMilliseconds;
        Star1 = res != -1 ? res.ToString() : ActivePuzzle.Output;
        sw.Reset();
        Star1Button = "[Run]";
        Star1ButtonStatus = false;
    }

    protected async Task RunStar2()
    {
        Star2Button = "Running...";
        Star2ButtonStatus = true;
        var sw = new Stopwatch();
        sw.Start();
        var res = await Task.Run(() => ActivePuzzle.Star2());
        sw.Stop();
        Star2Ms = sw.ElapsedMilliseconds;
        Star2 = res != -1 ? res.ToString() : ActivePuzzle.Output;
        sw.Reset();
        Star2Button = "[Run]";
        Star2ButtonStatus = false;
    }

}