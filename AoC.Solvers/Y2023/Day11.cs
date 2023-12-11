namespace AoC.Solvers.Y2023;

public class Day11 : IDay
{
    public Day11(string input) => Input = InputParsers.GetInputLines(input);

    public Day11(string input, int expand)
    {
        Input = InputParsers.GetInputLines(input);
        Expand = expand;
    }
    public string Output => output;
    private string output = default!;
    private string[] Input { get; set; }
    private int Expand { get; set; } = -1;

    public int Star1()
    {
        output = Analyse(2);
        return -1;
    }

    public int Star2()
    {
        output = Analyse(Expand == -1 ? 1000000 : Expand);
        return -1;
    }

    private string Analyse(int expand)
    {
        int voidExpand = expand - 1;
        int gdx = 1;
        var galaxies = Input.Select((l, x) => l.Select((c, y) => (c, x: (long)x, y: (long)y)).Where(t => t.c == '#')).SelectMany(t => t).ToDictionary(key => gdx++, v => (v.x, v.y));

        var voidRows = Enumerable.Range(0, Input.Length).Where(i => galaxies.Values.All(t => t.x != i)).ToArray();
        var voidColumns = Enumerable.Range(0, Input[0].Length).Where(i => galaxies.Values.All(t => t.y != i)).ToArray();

        for (int i = 1; i <= galaxies.Count; i++)
        {
            var expandRow = voidRows.Count(t => t < galaxies[i].x);
            var expandCol = voidColumns.Count(t => t < galaxies[i].y);
            galaxies[i] = (galaxies[i].x + voidExpand * expandRow, galaxies[i].y + voidExpand * expandCol);
        }

        return galaxies.Keys.SelectMany(a => galaxies.Keys, (x, y) => (x, y)).Where(t => t.x < t.y)
            .Sum(p => Utils.ManhattanDistance(galaxies[p.x], galaxies[p.y])).ToString();
    }
}
