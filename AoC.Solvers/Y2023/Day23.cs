namespace AoC.Solvers.Y2023;

public class Day23(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    public int Star1()
    {
        var start = new Pos(0, 1);
        var goal = new Pos(Input.Length - 1, Input[0].Length - 2);
        var paths = Hike(start, goal, true);
        return paths.Max(path => path.Count - 1);
    }

    public int Star2()
    {
        var junktions = FindJunktions();
        var start = new Pos(0, 1);
        var goal = new Pos(Input.Length - 1, Input[0].Length - 2);

        Dictionary<Pos, List<(Pos, int)>> graph = CreateGraph([.. junktions, start, goal]);
        return HikeGraph(graph, start, goal);
    }

    private static int HikeGraph(Dictionary<Pos, List<(Pos, int)>> graph, Pos start, Pos goal)
    {
        var queue = new Stack<List<(Pos, int)>>();
        int maxLength = 0;
        queue.Push([(start, 0)]);
        while (queue.TryPop(out var path))
        {
            var current = path.Last();
            if (current.Item1 == goal)
            {
                if (current.Item2 > maxLength)
                    maxLength = current.Item2;
                continue;
            }
            foreach (var next in graph[current.Item1])
                if (!path.Any(t => t.Item1 == next.Item1))
                    queue.Push([.. path, (next.Item1, current.Item2 + next.Item2 - 1)]);
        }
        return maxLength;
    }

    private List<List<Pos>> Hike(Pos start, Pos goal, bool star1)
    {
        List<List<Pos>> paths = [];
        var queue = new Stack<List<Pos>>();
        queue.Push([start]);
        while (queue.TryPop(out var path))
        {
            var current = path.Last();
            if (current == goal)
                paths.Add(path);

            foreach (Pos next in GetNext(current, star1 || current == start))
                if (!path.Contains(next))
                    queue.Push([.. path, next]);
        }
        return paths;
    }

    private Dictionary<Pos, List<(Pos, int)>> CreateGraph(List<Pos> junktions)
    {
        Dictionary<Pos, List<(Pos, int)>> graph = [];
        foreach (var j1 in junktions)
            foreach (var j2 in junktions.Where(t => t != j1))
            {
                var p = Hike(j1, j2, false);
                if (p.Count > 0)
                {
                    var longestPath = p.OrderBy(t => t.Count).Last();
                    if (graph.ContainsKey(j1))
                        graph[j1].Add((j2, longestPath.Count));
                    else
                        graph[j1] = [(j2, longestPath.Count)];
                }
            }
        return graph;
    }

    private List<Pos> FindJunktions()
    {
        List<Pos> junktions = [];
        for (int row = 0; row < Input.Length; row++)
            for (int col = 0; col < Input[row].Length; col++)
            {
                var p = new Pos(row, col);
                if (Input[p.Row][p.Col] != '#' && IsJunktion(p))
                    junktions.Add(p);
            }
        return junktions;
    }

    private bool IsJunktion(Pos pos)
    {
        var next = Moves.Select(m => new Pos(pos.Row + m.Row, pos.Col + m.Col))
                .Where(p => p.Row > 0 && p.Row < Input.Length && p.Col > 0 && p.Col < Input[p.Row].Length &&
                 Input[p.Row][p.Col] != '#').ToList();
        return next.Count > 2;
    }

    private List<Pos> GetNext(Pos current, bool star1)
    {
        if (star1)
        {
            if (Slopes.TryGetValue(Input[current.Row][current.Col], out var slope))
                return [new Pos(current.Row + Moves[slope].Row, current.Col + Moves[slope].Col)];
            else
            {
                var next = Moves.Select(m => new Pos(current.Row + m.Row, current.Col + m.Col))
                .Where(p => p.Row > 0 && p.Row < Input.Length && p.Col > 0 && p.Col < Input[p.Row].Length &&
                 Input[p.Row][p.Col] != '#').ToList();
                return next;
            }
        }
        else
        {
            var next = Moves.Select(m => new Pos(current.Row + m.Row, current.Col + m.Col))
                .Where(p => p.Row > 0 && p.Row < Input.Length && p.Col > 0 && p.Col < Input[p.Row].Length &&
                 Input[p.Row][p.Col] != '#').ToList();
            if (next.Count < 3)
                return next;
            return [];
        }
    }
    private Dictionary<char, int> Slopes { get; } = new Dictionary<char, int>{
        {'^', 0},
        {'>', 1},
        {'v', 2},
        {'<', 3}};
    private (int Row, int Col)[] Moves { get; } = [(-1, 0), (0, 1), (1, 0), (0, -1)];


    record Pos(int Row, int Col);
}
