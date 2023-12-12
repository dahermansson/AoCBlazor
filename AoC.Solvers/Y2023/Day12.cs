namespace AoC.Solvers.Y2023;

public class Day12 : IDay
{
    public Day12(string input) => Input = InputParsers.GetInputLines(input);
    public string Output => output;
    private string output { get; set; } = default!;
    private string[] Input { get; set; }

    public int Star1()
    {
        long sum = 0;
        foreach (var row in Input)
        {
            var spring = row.Split(" ")[0];
            var brokenGroups = row.Split(" ")[1].Split(',').Select(int.Parse).ToArray();
            sum += Cache(spring, brokenGroups);
        }
        output = sum.ToString();
        return -1;
    }
    public int Star2()
    {
        long sum = 0;
        foreach (var row in Input)
        {
            var spring = row.Split(" ")[0];
            var brokenGroups = row.Split(" ")[1].Split(',').Select(int.Parse).ToArray();
            spring = string.Join('?', Enumerable.Range(0, 5).Select(t => spring));
            brokenGroups = Enumerable.Repeat(brokenGroups, 5).SelectMany(t => t).ToArray();
            sum += Cache(spring, brokenGroups);
        }
        output = sum.ToString();
        return -1;
    }
    private Dictionary<string, long> Lut { get; set; } = new();
    private long Cache(string s, int[] groups)
    {
        var key = $"{s}{string.Join('.', groups)}";
        if (!Lut.ContainsKey(key))
            Lut[key] = CountValid(s, groups);
        return Lut[key];
    }

    private long CountValid(string s, int[] groups)
    {
        if (groups.Length == 0)
            return s.Contains("#") ? 0 : 1;
        if (s == string.Empty)
            return 0;
        if (s[0] == '.')
            return Cache(s.Substring(1), groups);
        if (s[0] == '?')
            return Cache($".{s[1..]}", groups) + Cache($"#{s[1..]}", groups);

        if (groups.Length == 0 || s.Length < groups[0] || s[..groups[0]].Contains('.'))
            return 0;
        if (groups.Length > 1)
        {
            if (s.Length < groups[0] + 1 || s[groups[0]] == '#')
                return 0;
            return Cache(s.Substring(groups[0] + 1), groups[1..]);
        }
        return Cache(s.Substring(groups[0]), groups[1..]);
    }
}
