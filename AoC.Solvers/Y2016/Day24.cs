namespace AoC.Solvers.Y2016;

public class Day24(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        var map = Input.Select(t => t.ToCharArray()).ToArray();

        var targets = new List<Pos>();

        for (int r = 0; r < map.Length; r++)
            for (int c = 0; c < map[r].Length; c++)
                if (char.IsNumber(map[r][c]))
                    targets.Add(new Pos(r, c));

        var start = targets.Single(t => map[t.X][t.Y] == '0');


        var distances = targets.Select(a => (start: a, dist: targets.Where(b => a != b).Select(b => (start: a, target: b, cost: BFS(a, b, map))))).SelectMany(t => t.dist).ToArray();
        var paths = targets.Permutations().ToArray();

        var distancOfAllPaths = paths.Where(t => t.First() == start).Select(f => f.SkipLast(1).Select((p, index) => distances.Single(k => k.start == p && k.target == f.ElementAt(index + 1)).cost).Sum());
        return distancOfAllPaths.Min();
    }

    public int Star2()
    {
        var map = Input.Select(t => t.ToCharArray()).ToArray();

        var targets = new List<Pos>();

        for (int r = 0; r < map.Length; r++)
            for (int c = 0; c < map[r].Length; c++)
                if (char.IsNumber(map[r][c]))
                    targets.Add(new Pos(r, c));

        var start = targets.Single(t => map[t.X][t.Y] == '0');

        var distances = targets.Select(a => (start: a, dist: targets.Where(b => a != b).Select(b => (start: a, target: b, cost: BFS(a, b, map))))).SelectMany(t => t.dist).ToArray();
        var paths = targets.Permutations().Select(t => new List<Pos>(t) { start }).ToList();

        var distanceOfAllPaths = paths.Where(t => t.First() == start).Select(f => f.SkipLast(1).Select((p, index) => distances.Single(k => k.start == p && k.target == f.ElementAt(index + 1)).cost).Sum());
        return distanceOfAllPaths.Min();
    }

    private record Pos(int X, int Y)
    {
        internal IEnumerable<Pos> Neigbours(char[][] map)
        {
            if (map[(X + 1) % map.Length][Y] != '#') yield return new Pos(X + 1, Y);
            if (map[(X - 1) % map.Length][Y] != '#') yield return new Pos(X - 1, Y);
            if (map[X][(Y + 1) % map[0].Length] != '#') yield return new Pos(X, Y + 1);
            if (map[X][(Y - 1) % map[0].Length] != '#') yield return new Pos(X, Y - 1);
        }
    }

    private static int BFS(Pos start, Pos goal, char[][] map)
    {
        var q = new Queue<Pos>();
        var dist = new Dictionary<Pos, int>();
        var prev = new Dictionary<Pos, Pos>();
        var visited = new HashSet<Pos>();
        q.Enqueue(start);
        dist[start] = 0;

        while (q.Count > 0)
        {
            var current = q.Dequeue();
            if (current == goal)
                return dist[current];

            foreach (Pos neigbour in current.Neigbours(map).Where(t => !visited.Contains(t)))
            {
                var d = dist[current] + 1;
                if (!dist.TryGetValue(neigbour, out int value) || d < value)
                {
                    q.Enqueue(neigbour);
                    value = d;
                    dist[neigbour] = value;
                    prev[neigbour] = current;
                }
                visited.Add(current);
            }
        }
        return 0;
    }
}
