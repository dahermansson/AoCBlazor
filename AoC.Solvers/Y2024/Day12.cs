namespace AoC.Solvers.Y2024;

public class Day12(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    private static readonly List<(int x, int y)> Dir = [(-1,0), (0, 1), (1, 0), (0,-1)];
    private List<Plot> GetNeighbors(Plot valuePos) => 
        Dir.Where(d => valuePos.X + d.x < Input.Length &&
            valuePos.X + d.x > -1 && 
            valuePos.Y + d.y < Input[0].Length &&
            valuePos.Y + d.y > -1 && Input[valuePos.X + d.x][valuePos.Y + d.y] == valuePos.Value).Select(d => 
            new Plot(valuePos.X + d.x, valuePos.Y + d.y, Input[valuePos.X + d.x][valuePos.Y + d.y])
            ).ToList();

    record Plot(int X, int Y, char Value)
    {
        public (int X, int Y) Pos => (X, Y);
    }

    private List<List<Plot>> GetRegions()
    {
        IEnumerable<Plot> CreateRegion(Plot current, HashSet<Plot> garden, HashSet<(int x, int Y)> notInRegions)
        {
            if(garden.Count == 0)
            {
                garden.Add(current);
                notInRegions.Remove(current.Pos);
            }

            var neighbors = GetNeighbors(current).Where(n => notInRegions.Contains(n.Pos) && !garden.Contains(n)).ToList();
            
            if(neighbors.Count == 0)
                return garden;

            foreach (var neighbor in neighbors)
            {
                garden.Add(neighbor);
                notInRegions.Remove(neighbor.Pos);
                CreateRegion(neighbor, garden, notInRegions);
            }
            return garden;
        }

        var garden = Input.Select((row, x) => (row, x)).SelectMany(row => row.row.Select((c, y) => new Plot(row.x, y, row.row[y]))).ToDictionary(key => key.Pos, value => value);
        var regions = new List<List<Plot>>();
        HashSet<(int X, int Y)> notInRegions = [];
        garden.Keys.ToList().ForEach(p => notInRegions.Add(p));
        while(notInRegions.Count != 0)
        {
            regions.Add(CreateRegion(garden[notInRegions.First()], [], notInRegions).ToList());
        }
        return regions;
    }
    
    public int Star1()
    {
        var regions = GetRegions();
        return regions.Sum(t => t.Count * t.Sum(n => 4 - GetNeighbors(n).Count(c => c.Value == n.Value)));
    }

    public int Star2()
    {
        return 0;
    }
}
