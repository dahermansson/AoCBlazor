namespace AoC.Solvers.Y2023;

public class Day11: IDay
{
    public Day11(string input) => Input = InputParsers.GetInputLines(input);
    public string Output => output;
    private string output = default!;
    private string[] Input {get; set;}

    public int Star1()
    {
        Analyse(2);
        return -1;
    }

    public int Star2()
    {
        Analyse(1000000);
        return -1;
    }

    private void Analyse(int expand)
    {
        int voidExpand = expand-1;
        
        Dictionary<int, (long x, long y)> galaxies = [];
        int gdx = 1;
        for (int r = 0; r < Input.Length; r++)
            for (int c = 0; c < Input[0].Length; c++)
                if(Input[r][c] == '#')
                    galaxies.Add(gdx++, (r,c));
        
        var voidRows = Enumerable.Range(0,Input.Length).Where(i => galaxies.Values.All(t => t.x != i )).ToArray();
        var voidColumns = Enumerable.Range(0,Input[0].Length).Where(i => galaxies.Values.All(t => t.y != i )).ToArray();

        for (int i = 1; i <= galaxies.Count; i++)
        {
            var expandRow = voidRows.Count(t => t < galaxies[i].x);
            var expandCol = voidColumns.Count(t => t < galaxies[i].y);
            galaxies[i] = (galaxies[i].x + voidExpand * expandRow, galaxies[i].y + voidExpand * expandCol);
        }

        output = galaxies.Keys.SelectMany(a => galaxies.Keys, (x,y) => (x,y)).Where(t => t.x < t.y)
            .Sum(p => Utils.ManhattanDistance(galaxies[p.x], galaxies[p.y])).ToString();
    }
}
