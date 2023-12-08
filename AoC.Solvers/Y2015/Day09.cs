using AoC.AoCUtils;

namespace AoC.Solvers.Y2015;

public class Day09: IDay
{
    public Day09(string input)
    {
        Input = InputParsers.GetInputLines(input);
        Distincts = Input.Select(t => new string[]{t.Split(" ")[0], t.Split(" ")[2]}).SelectMany(t => t).Distinct().ToList();
        Paths = Distincts.ToList().GetPermutations();
        Distances = Input.Select(t => Parse(t.Split(" "))).SelectMany(t => t).DistinctBy(t => t.Key).ToDictionary(k => k.Key, v => v.Value);
    }
    public string Output => throw new NotImplementedException();
    private string[] Input {get; set;}
    private List<string> Distincts {get; init;}
    private Dictionary<string, int> Distances {get; init;}
    private IEnumerable<IEnumerable<string>> Paths {get; init;}
    public int Star1() => Paths.Min(t => GetPermsDistances(t.ToArray()));    
    public int Star2() => Paths.Max(t => GetPermsDistances(t.ToArray()));
    private int GetPermsDistances(string[] l) => l.SkipLast(1).Select((t,i) => Distances[$"{l[i]}{l[i+1]}"]).Sum();
    
    private KeyValuePair<string, int>[] Parse(string[] s) => [
                new KeyValuePair<string, int>($"{s[0]}{s[2]}", int.Parse(s[4])), 
                new KeyValuePair<string, int>($"{s[2]}{s[0]}", int.Parse(s[4]))
            ];

}
