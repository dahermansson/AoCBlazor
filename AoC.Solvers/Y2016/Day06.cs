using AoC.AoCUtils;

namespace AoC.Solvers.Y2016;

public class Day06(string input) : IDay
{
    public string Output => output;
    private string output = string.Empty;
    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        output = string.Concat(GetFrequenses().Select(t => t.Value.OrderByDescending(p => p.Value).First().Key));
        return -1;
    }

    public int Star2()
    {
        output = string.Concat(GetFrequenses().Select(t => t.Value.OrderByDescending(p => p.Value).Last().Key));
        return -1;
    }

    private Dictionary<int, Dictionary<char, int>> GetFrequenses()
    {
        var frequenses = new Dictionary<int, Dictionary<char, int>>();
        foreach (var row in Input)
            for (int i = 0; i < row.Length; i++)
            {
                if(!frequenses.ContainsKey(i))
                    frequenses[i] = new Dictionary<char, int>();
                if(!frequenses[i].ContainsKey(row[i]))
                    frequenses[i][row[i]] = 1;
                else
                    frequenses[i][row[i]]++;
            }
        return frequenses;
    }
}
