using System.Text;

namespace AoC.Solvers.Y2023;

public class Day13 : IDay
{
    public Day13(string input)
    {
        Input = InputParsers.GetInputLines(input);
        CreatePatterns();
    }
    public string Output => throw new NotImplementedException();
    private string[] Input { get; set; }
    private List<List<string>> Patterns { get; set; } = default!;
    public int Star1() => Patterns.Sum(t => FindFold(t, 0));
    public int Star2() => Patterns.Sum(t => FindFold(t, 1));

    private static int FindFold(List<string> s, int allowedDiff)
    {
        var res = 0;
        for (int i = 1; i < s.Count; i++)
        {
            var pairs = s.Take(i).Reverse().Zip(s.Skip(i)).ToList();
            int diff = 0;
            foreach (var pair in pairs)
                diff += pair.First.Zip(pair.Second).Count(t => t.First != t.Second);
            if (diff == allowedDiff)
                res =  i * 100;
        }
        var c = GetColumns(s);
        for (int i = 1; i < c.Count; i++)
        {
            var pairs = c.Take(i).Reverse().Zip(c.Skip(i)).ToList();
            int diff = 0;
            foreach (var pair in pairs)
                diff += pair.First.Zip(pair.Second).Count(t => t.First != t.Second);
            if (diff == allowedDiff)
                res = i;
        }

        return res;
    }

    private static List<string> GetColumns(List<string> s)
    {
        var res = new List<string>();
        for (int i = 0; i < s[0].Length; i++)
        {
            StringBuilder sb = new();
            foreach (var line in s)
                sb.Append(line[i]);
            res.Add(sb.ToString());
        }
        return res;
    }

    private void CreatePatterns()
    {
        Patterns = [];
        var p = new List<string>();
        foreach (var line in Input)
        {
            if (line != string.Empty)
                p.Add(line);
            else
            {
                Patterns.Add(p);
                p = [];
            }
        }
        Patterns.Add(p);
    }
}
