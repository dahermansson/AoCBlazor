namespace AoC.Solvers.Y2024;

public class Day18(string input) : IDay
{
    public string Output => output;
    private string output = string.Empty;
    private List<Pos> Input { get; set; } = InputParsers.GetInputLines(input).Select(t => new Pos(int.Parse(t.Split(',')[0]), int.Parse(t.Split(',')[1]))).ToList();
    record Pos(int X, int Y);
    record Boundary(int XMax, int YMax, int XMin = 0, int YMin = 0)
    {
        public bool InBoundary(Pos pos) => pos.X >= XMin && pos.X <= XMax && pos.Y >= YMin && pos.Y <= YMax;
    }
    private readonly List<Pos> Dir = [new(0, -1), new(1, 0), new(0, 1), new(-1, 0)];

    public int Star1() => AStar(1024);

    public int Star2()
    {
        var chunk = Enumerable.Range(0, Input.Count).Chunk(300).SkipWhile(chunk => AStar(chunk.Last()) is > -1).Take(1).First();
        var blockingByte = Input[chunk.TakeWhile(i => AStar(i) is > -1).Last()];

        output = $"{blockingByte.X},{blockingByte.Y}";
        return -1;
    }
    private int AStar(int fallenBytes)
    {
        var boundary = new Boundary(70, 70);
        var memory = Input.Take(fallenBytes).ToHashSet();

        var start = new Pos(0, 0);
        var goal = new Pos(boundary.XMax, boundary.YMax);

        var gScore = new Dictionary<Pos, int>() { { start, 0 } };
        var hScore = new Dictionary<Pos, int>() { { start, Utils.ManhattanDistance(start.X, goal.X, start.Y, goal.Y) } };
        var parents = new Dictionary<Pos, Pos>();

        var closed = new HashSet<Pos>();
        var open = new PriorityQueue<Pos, int>();

        open.Enqueue(start, 0);
        while (open.Count > 0)
        {
            var current = open.Dequeue();
            if (current == goal)
            {
                return gScore[goal];
            }
            closed.Add(current);

            foreach (var next in Dir.Select(d => new Pos(current.X + d.X, current.Y + d.Y)).Where(t => boundary.InBoundary(t) && !memory.Contains(t) && !closed.Contains(t)))
            {
                var score = gScore[current] + 1;
                if (!gScore.TryGetValue(next, out int value) || score < value)
                {
                    gScore[next] = score;
                    hScore[next] = Utils.ManhattanDistance(next.X, goal.X, next.Y, goal.Y);

                    parents[next] = current;

                    open.Remove(next, out _, out _);
                    open.Enqueue(next, gScore[next] + hScore[next]);
                }
            }
        }
        return -1;
    }
}
