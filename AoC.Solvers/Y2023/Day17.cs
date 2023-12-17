namespace AoC.Solvers.Y2023;

public class Day17 : IDay
{
    public Day17(string input)
    {
        Input = InputParsers.GetInputLines(input);
        Map = [];
        for (int row = 0; row < Input.Length; row++)
            for (int col = 0; col < Input[row].Length; col++)
                Map[(row, col)] = int.Parse(Input[row][col].ToString());
        Goal = (Map.Keys.Max(k => k.X), Map.Keys.Max(k => k.Y));
    }
    private Dictionary<(int X, int Y), int> Map { get; init; }
    public string Output => throw new NotImplementedException();
    private (int X, int Y) Goal { get; init; }
    private string[] Input { get; init; }

    public int Star1() => MinimizeHeat(new(0, 0, -1, -1), false);
    public int Star2() => MinimizeHeat(new(0, 0, -1, -1), true);

    private int MinimizeHeat(Pos start, bool ultraCrucibles)
    {
        HashSet<Pos> v = [];
        PriorityQueue<Pos, int> q = new();
        q.Enqueue(start, 0);
        while (q.TryDequeue(out Pos? current, out int heat))
        {
            if (v.Contains(current))
                continue;
            v.Add(current);

            if (!ultraCrucibles && current.Row == Goal.X && current.Col == Goal.Y)
                return heat;
            if (current.Row == Goal.X && current.Col == Goal.Y && current.DirBlockCount >= 3)
                return heat;

            foreach (var next in GetNext(current, ultraCrucibles))
            {
                if (v.Contains(next) || !Map.ContainsKey((next.Row, next.Col)))
                    continue;
                q.Enqueue(next, heat + Map[(next.Row, next.Col)]);
            }
        }
        return 0;
    }

    private IEnumerable<Pos> GetNext(Pos c, bool ultraCrucibles)
    {
        if (c.Dir == -1)
        {
            yield return new Pos(c.Row + Dir[0].X, c.Col + Dir[0].Y, 0, 0);
            yield return new Pos(c.Row + Dir[2].X, c.Col + Dir[2].Y, 0, 0);
        }
        else
        {
            if (c.DirBlockCount < (ultraCrucibles ? 9 : 2))
                yield return new Pos(c.Row + Dir[c.Dir].X, c.Col + Dir[c.Dir].Y, c.Dir, c.DirBlockCount + 1);
            if (c.DirBlockCount >= (ultraCrucibles ? 3 : 0))
                if (c.Dir < 2)
                {
                    yield return new Pos(c.Row + Dir[2].X, c.Col + Dir[2].Y, 2, 0);
                    yield return new Pos(c.Row + Dir[3].X, c.Col + Dir[3].Y, 3, 0);
                }
                else
                {
                    yield return new Pos(c.Row + Dir[0].X, c.Col + Dir[0].Y, 0, 0);
                    yield return new Pos(c.Row + Dir[1].X, c.Col + Dir[1].Y, 1, 0);
                }
        }
    }
    record Pos(int Row, int Col, int Dir, int DirBlockCount);
    private readonly List<(int X, int Y)> Dir = [
        (0, 1),
        (0, -1),
        (1, 0),
        (-1, 0)
    ];
}
