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
    }
    private Dictionary<(int X, int Y), int> Map { get; set; }
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; }

    public int Star1() => new Pos[] {
            new(0, 0, 0, -1), 
            new(0, 0, 2, -1) 
            }.Min(t => FindWay(t, Map.Max(t => t.Key.X), Map.Max(t => t.Key.Y), false));
    
    public int Star2() => new Pos[] {
            new(0, 0, 0, -1), 
            new(0, 0, 2, -1) 
            }.Min(t => FindWay(t, Map.Max(t => t.Key.X), Map.Max(t => t.Key.Y), true));

    private int FindWay(Pos start, int goalX, int goalY, bool ultraCrucibles)
    {
        HashSet<Pos> v = [];
        PriorityQueue<Pos, int> q = new();
        q.Enqueue(start, 0);
        while (q.TryDequeue(out Pos? current, out int heat))
        {
            if (v.Contains(current))
                continue;
            v.Add(current);
            if(ultraCrucibles)
            {
                if (current.Row == goalX && current.Col == goalY && current.DirBlockCount >= 3)
                    return heat;
            }   
            else if (current.Row == goalX && current.Col == goalY)
                    return heat;
               
            foreach (var next in ultraCrucibles ? GetNextUltraBlock(current) : GetNext(current))
            {
                if (v.Contains(next) || !Map.ContainsKey((next.Row, next.Col)))
                    continue;
                q.Enqueue(next, heat + Map[(next.Row, next.Col)]);
            }
        }
        return 0;
    }

    private IEnumerable<Pos> GetNext(Pos c)
    {
        if (c.DirBlockCount < 2 )
            yield return new Pos(c.Row + Dir[c.Dir].X, c.Col + Dir[c.Dir].Y, c.Dir, c.DirBlockCount + 1);
        if (c.Dir < 2)
        {
            yield return new Pos(c.Row + Dir[2].X, c.Col + Dir[2].Y, 2, 0);
            yield return new Pos(c.Row + Dir[3].X, c.Col + Dir[3].Y, 3, 0);
        }
        else if (c.Dir > 1)
        {
            yield return new Pos(c.Row + Dir[0].X, c.Col + Dir[0].Y, 0, 0);
            yield return new Pos(c.Row + Dir[1].X, c.Col + Dir[1].Y, 1, 0);
        }
    }
    private IEnumerable<Pos> GetNextUltraBlock(Pos c)
    {
        if (c.DirBlockCount < 9)
            yield return new Pos(c.Row + Dir[c.Dir].X, c.Col + Dir[c.Dir].Y, c.Dir, c.DirBlockCount + 1);
        if (c.DirBlockCount >= 3)
        {
            if(c.Dir < 2)
            {
                yield return new Pos(c.Row + Dir[2].X, c.Col + Dir[2].Y, 2, 0);
                yield return new Pos(c.Row + Dir[3].X, c.Col + Dir[3].Y, 3, 0);
            }
            else if(c.Dir > 1)
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
