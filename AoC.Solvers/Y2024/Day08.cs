namespace AoC.Solvers.Y2024;

public class Day08(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    private int MaxX {get; set; } = InputParsers.GetInputLines(input).Length;
    private int MaxY {get; set; } = InputParsers.GetInputLines(input)[0].Length;

    public int Star1() => CountAntinode(false);
    public int Star2() => CountAntinode(true);
    
    private int CountAntinode(bool star2)
    {
        var frequencies = GetFrequencies();
        HashSet<(int x, int y)> antinode = [];
        foreach(var frequency in frequencies)
        {
            var pairs = frequency.Value.SelectMany(a => frequency.Value, (a, b) => (a: (a.x, a.y), b: (b.x, b.y))).Where(pair => pair.a != pair.b).ToList();
            _ = pairs.SelectMany(t => CreateAntinode(t.a, t.b, star2)).Select(antinode.Add).ToList();
        }
        return antinode.Count;
    }

    private Dictionary<char, List<(int x, int y)>> GetFrequencies() => 
        Input.SelectMany((row, x) => row.Select((c,y) => (x, y, c)))
            .Where(p => p.c != '.')
                .GroupBy(p => p.c).ToDictionary(g => g.Key, g => g.Select(c => (c.x, c.y)).ToList());

    private IEnumerable<(int x, int y)> CreateAntinode((int x, int y) p1, (int x, int y) p2, bool star2 = false)
    {
        if (star2) yield return (p1.x, p1.y);
        if (star2) yield return (p2.x, p2.y);
        var xDiff = p1.x - p2.x;
        var yDiff = p1.y - p2.y;

        int antinodeX = p1.x + xDiff;
        int antinodeY = p1.y + yDiff;
        while (antinodeX > -1 && antinodeX < MaxX && antinodeY > -1 && antinodeY < MaxY)
        {
            yield return (antinodeX, antinodeY);
            if(!star2) break;
            antinodeX += xDiff;
            antinodeY += yDiff;
        }

        antinodeX = p2.x - xDiff;
        antinodeY = p2.y - yDiff;
        while (antinodeX > -1 && antinodeX < MaxX && antinodeY > -1 && antinodeY < MaxY)
        {
            yield return (antinodeX, antinodeY);
            if(!star2) break;
            antinodeX -=xDiff;
            antinodeY -=yDiff;
        }
    }
}