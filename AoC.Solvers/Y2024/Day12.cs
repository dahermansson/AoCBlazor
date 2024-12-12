namespace AoC.Solvers.Y2024;

public class Day12(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);


    private static readonly List<(int x, int y)> Dir = [(-1,0), (0, 1), (1, 0), (0,-1)];
    private List<ValuePos> GetNeigbours(ValuePos valuePos) => 
        Dir.Where(d => valuePos.X + d.x < Input.Length &&
            valuePos.X + d.x > -1 && 
            valuePos.Y + d.y < Input[0].Length &&
            valuePos.Y + d.y > -1).Select(d => 
            new ValuePos(valuePos.X + d.x, valuePos.Y + d.y, Input[valuePos.X + d.x][valuePos.Y + d.y])
            ).ToList();

    record ValuePos(int X, int Y, char Value)
    {
        public (int X, int Y) Pos => (X, Y);
    }
    
    public int Star1()
    {
        Input = """
        RRRRIICCFF
        RRRRIICCCF
        VVRRRCCFFF
        VVRCCCJFFF
        VVVVCJJCFE
        VVIVCCJJEE
        VVIIICJJEE
        MIIIIIJJEE
        MIIISIJEEE
        MMMISSJEEE
        """.Split(Environment.NewLine);
        var garden = Input.Select((row, x) => (row, x)).SelectMany(row => row.row.Select((c, y) => new ValuePos(row.x, y, row.row[y]))).ToDictionary(key => key.Pos, value => value);

        var regions = new List<List<ValuePos>>();
        foreach(var g in garden.GroupBy(t => t.Value.Value))
        {
            var k = g.Aggregate(new List<List<ValuePos>>(), (r, plot) => {
                var n = GetNeigbours(plot.Value).Where(t => t.Value == plot.Value.Value);
                try{

                
                var nRegion = r.Where(t => n.Any(c => t.Contains(c))).SingleOrDefault();
                if(nRegion != null)
                {
                    nRegion.Add(plot.Value);
                }
                else
                {
                    nRegion = [plot.Value];
                    r.Add(nRegion);
                }
                }
                catch(Exception e)
                {
                    
                }
                return r;
            });
            regions.AddRange(k);
        }


        return regions.Sum(t => t.Count * t.Sum(n => 4 - GetNeigbours(n).Count(c => c.Value == n.Value)));
    }

    public int Star2()
    {
        return 0;
    }
}
