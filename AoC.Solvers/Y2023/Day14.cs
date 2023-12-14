using System.Text;

namespace AoC.Solvers.Y2023;

public class Day14: IDay
{
    public Day14(string input) => Input = InputParsers.GetInputLines(input).ToList();
    public string Output => throw new NotImplementedException();

    private List<string> Input {get; set;}

    public int Star1()
    {
        var columns = GetColumns(Input);
        var tilted = new List<string>();
        foreach (var column in columns)
        {
            tilted.Add(Tilt(column, false));
        }
        return tilted.Sum(c => c.IndexOfMany(k => k == 'O').Select(i => c.Length - i).Sum());
    }

    public int Star2()
    {
        Dictionary<string, int> cycles = [];
        var t = Input;
        var cyclesToRun = 1_000_000_000;
        var cyclesRemaning = 0;
        for (int i = 1; i < cyclesToRun; i++)
        {        
            t = Cycle(t);
            
            var cycle = string.Concat(t);
            if(cycles.ContainsKey(cycle))
            {
                var cycleStart = cycles[cycle];
                var cycleLength = i - cycleStart;
                cyclesRemaning = (cyclesToRun - cycleStart) % cycleLength;
                break;
            }
            cycles.Add(string.Concat(t), i);
            
        }
        for (int i = 0; i < cyclesRemaning; i++)
        {        
            t = Cycle(t);
        }
        
        return GetColumns(t).Sum(c => c.IndexOfMany(k => k == 'O').Select(i => c.Length - i).Sum());
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
    private static List<string> Cycle(List<string> s)
    {
        var norht = GetColumns(s);
        var tilted = Tilt(norht, false);
        var west = GetColumns(tilted);
        tilted = Tilt(west, false);
        var south = GetColumns(tilted);
        tilted = Tilt(south, true);
        var east = GetColumns(tilted);
        tilted = Tilt(east, true);
        
        return tilted;
    }

    private static List<string> Tilt(List<string> str, bool reverse)
    {
        var res = new List<string>();
        foreach (var s in str)
        {
            res.Add(Tilt(s, reverse));
        }
        return res;
    }
    private static string Tilt(string str, bool reverse)
    {
        var s = str.Split("#");
        var parts = new List<String>();
        foreach (var i in s)
            parts.Add(string.Join("", reverse ? i.OrderBy(t => t) : i.OrderByDescending(t => t)));

        return string.Join('#', parts);
    }

}
