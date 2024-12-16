namespace AoC.Solvers.Y2024;

public class Day16(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string Input { get; set; } = input;
    private Dictionary<(int X, int Y), char> Map = InputParsers.GetInputLines(input).SelectMany((row, rowIndex) => 
        row.Select((c, colIndex) => new KeyValuePair<(int X, int Y), char>((X: rowIndex, Y: colIndex), c))).ToDictionary();

    private List<(int X, int Y)> Dirs = [(0, 1), (1, 0), (0, -1), (-1,0)];

    public int Star1()
    {
        List<int> GoalScores = [];
        var start = Map.Single(t => t.Value == 'S');

        var cost = new Dictionary<(int X, int Y), int>(Map.Where(t => t.Value is '.' or 'E').Select(t => new KeyValuePair<(int X, int Y), int>((t.Key.X, t.Key.Y), int.MaxValue)))
        {
            [start.Key] = 0
        };

        IEnumerable<((int X, int Y) Pos, int Dir)> GetNextTiles(((int X, int Y) Pos, int Dir) current) => current.Dir switch{
        0 => new int[]{ 0, 1, 3 }.Select(d => ((current.Pos.X + Dirs[d].X, current.Pos.Y + Dirs[d].Y), d)).Where(t => cost.ContainsKey(t.Item1)),
        1 => new int[]{ 0, 1, 2 }.Select(d => ((current.Pos.X + Dirs[d].X, current.Pos.Y + Dirs[d].Y), d)).Where(t => cost.ContainsKey(t.Item1)),
        2 => new int[]{ 1, 2, 3 }.Select(d => ((current.Pos.X + Dirs[d].X, current.Pos.Y + Dirs[d].Y), d)).Where(t => cost.ContainsKey(t.Item1)),
        3 => new int[]{ 0, 2, 3 }.Select(d => ((current.Pos.X + Dirs[d].X, current.Pos.Y + Dirs[d].Y), d)).Where(t => cost.ContainsKey(t.Item1)),
        _ => []
        };

        var queue = new PriorityQueue<((int X, int Y) Pos, int Dir), int>();
        queue.Enqueue(((start.Key.X, start.Key.Y), 0), 0);
        while(queue.Count > 0)
        {
            var current = queue.Dequeue();
            if(Map[current.Pos] == 'E')
                return cost[current.Pos];

            var currentCost = cost[current.Pos];

            foreach(var tile in GetNextTiles(current))
            {
                var oldCost = cost[tile.Pos];
                var newCost = tile.Dir != current.Dir ? currentCost + 1001 : currentCost + 1;
                if(newCost < oldCost)
                {
                    cost[tile.Pos] = newCost;
                    queue.Remove(tile, out _, out _);
                    queue.Enqueue(tile, newCost);
                }
            }
        }
        return 0;
    }

    public int Star2()
    {
       List<int> GoalScores = [];
        var start = Map.Single(t => t.Value == 'S');

        var cost = new Dictionary<(int X, int Y, int Dir), (int Cost, HashSet<(int X, int Y)> Prev)>(Map.Where(t => t.Value is '.' or 'E').SelectMany(t => Dirs.Select((_, dir) =>
                new KeyValuePair<(int X, int Y, int Dir), (int Cost, HashSet<(int X, int Y)> Prev)>((t.Key.X, t.Key.Y, dir), (int.MaxValue, new HashSet<(int X, int Y)>())))))
        {
            [(start.Key.X, start.Key.Y, 0)] = (0, [start.Key])
        };
        
        IEnumerable<((int X, int Y, int Dir) tile, bool turns)> GetNextTiles((int X, int Y, int Dir) current) => current.Dir switch{
        0 => new int[]{ 0, 1, 3 }.Select(d => (tile: (X: current.X + Dirs[d].X, Y: current.Y + Dirs[d].Y, d), d != current.Dir)).Where(t => cost.ContainsKey(t.tile)),
        1 => new int[]{ 0, 1, 2 }.Select(d => (tile: (X: current.X + Dirs[d].X, Y: current.Y + Dirs[d].Y, d), d != current.Dir)).Where(t => cost.ContainsKey(t.tile)),
        2 => new int[]{ 1, 2, 3 }.Select(d => (tile: (X: current.X + Dirs[d].X, Y: current.Y + Dirs[d].Y, d), d != current.Dir)).Where(t => cost.ContainsKey(t.tile)),
        3 => new int[]{ 0, 2, 3 }.Select(d => (tile: (X: current.X + Dirs[d].X, Y: current.Y + Dirs[d].Y, d), d != current.Dir)).Where(t => cost.ContainsKey(t.tile)),
        _ => []
        };
        var paths = new List<(List<(int X, int Y)>, int Cost)>();
        var queue = new PriorityQueue<(int X, int Y, int Dir, HashSet<(int X, int Y)> visited), int>();
        queue.Enqueue((start.Key.X, start.Key.Y, 0, [(start.Key.X, start.Key.Y)]), 0);
        while(queue.Count > 0)
        {
            var current = queue.Dequeue();
            if(Map[(current.X, current.Y)] == 'E')
            { 
                paths.Add(([..current.visited, (current.X, current.Y)], cost[(current.X, current.Y, current.Dir)].Cost ));
            }

            var currentCost = cost[(current.X, current.Y, current.Dir)].Cost;

            foreach(var tile in GetNextTiles((current.X, current.Y, current.Dir)))
            {
                var oldCost = cost[tile.tile].Cost;
                var newCost = tile.turns ? currentCost + 1001 : currentCost + 1;
                if(newCost <= oldCost)
                {
                    cost[tile.tile] = cost[tile.tile] with{ Cost = newCost, Prev = [..cost[tile.tile].Prev, (current.X, current.Y)]};
                    queue.Enqueue((tile.tile.X, tile.tile.Y, tile.tile.Dir, [..current.visited, (tile.tile.X, tile.tile.Y)]), newCost);
                }
            }
        }
        var min = paths.Min(t=> t.Cost);
        foreach (var k in paths.Where(t => t.Cost == min))
        {
            k.Item1.ForEach(t => Map[(t.X, t.Y)] = 'O');
        }
        
        Console.WriteLine(Map.ToPrintableString());


        return paths.Where(t => t.Cost == min).SelectMany(t => t.Item1.Select(p => (p.X, p.Y))).Distinct().Count();
    }
}
