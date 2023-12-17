using System.Numerics;

namespace AoC.Solvers.Y2023;

public class Day17 : IDay
{
    public Day17(string input)
    {
        Input = InputParsers.GetInputLines(input);
        Map = new();
        for (int row = 0; row < Input.Length; row++)
            for (int col = 0; col < Input[row].Length; col++)
                Map[(row, col)] = int.Parse(Input[row][col].ToString());

    }
    private Dictionary<(int X, int Y), int> Map { get; set; }
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; }

    public int Star1()
    {
        //return 0;
        return new Pos[] {
            new Pos(0, 0, 0, 1), 
            new Pos(0, 0, 2, 1) 
            }.Min(m => FindWay(m, Map.Max(t => t.Key.X), Map.Max(t => t.Key.Y), false));
    }
    public int Star2()
    {
        var t = new Pos[] {
            new Pos(0, 0, 0, 1), 
            new Pos(0, 0, 2, 1) 
            }.Min(m => FindWay(m, Map.Max(t => t.Key.X), Map.Max(t => t.Key.Y), true));
        //foreach (var a in t)
        //{
          //  Console.WriteLine(a);
        //}
        return t;
    }

    private int FindWay(Pos start, int goalX, int goalY, bool ultraBlock)
    {
        //954 to low
        //1144 to low
        //1148 wrong
        //1149 Correct
        //1152 wrong
        //1188 to high
        HashSet<Pos> v = [];
        PriorityQueue<Pos, int> q = new();
        q.Enqueue(start, 0);
        while (q.TryDequeue(out Pos? current, out int heat))
        {
            if (v.Contains(current))
                continue;
            v.Add(current);
            if(ultraBlock)
            {
                if (current.Row == goalX && current.Col == goalY && current.DirBlockCount == 5)
                    return heat;
            }   
            else
                if (current.Row == goalX && current.Col == goalY)
                    return heat;
                
            foreach (var next in ultraBlock ? GetNextUltraBlock(current).ToArray() : GetNext(current).ToArray())
            {
                if (v.Contains(next) || !Map.ContainsKey((next.Row, next.Col)))
                    continue;
                int nextHeat = heat + Map[(next.Row, next.Col)];
                q.Enqueue(next, nextHeat);
            }

        }
        return 0;
    }

    private IEnumerable<Pos> GetNext(Pos c)
    {
        if (c.DirBlockCount < 3)
            yield return new Pos(c.Row + Dir[c.Dir].X, c.Col + Dir[c.Dir].Y, c.Dir, c.DirBlockCount + 1);
        if (c.Dir == 0 || c.Dir == 1)
        {
            yield return new Pos(c.Row + Dir[2].X, c.Col + Dir[2].Y, 2, 1);
            yield return new Pos(c.Row + Dir[3].X, c.Col + Dir[3].Y, 3, 1);
        }
        else if (c.Dir == 2 || c.Dir == 3)
        {
            yield return new Pos(c.Row + Dir[0].X, c.Col + Dir[0].Y, 0, 1);
            yield return new Pos(c.Row + Dir[1].X, c.Col + Dir[1].Y, 1, 1);
        }
    }
    private IEnumerable<Pos> GetNextUltraBlock(Pos c)
    {
        if (c.DirBlockCount < 10) // can go straight
            yield return new Pos(c.Row + Dir[c.Dir].X, c.Col + Dir[c.Dir].Y, c.Dir, c.DirBlockCount + 1);
        if (c.DirBlockCount >= 4) // allow turn
        {
            if(c.Dir == 0 || c.Dir == 1)
            {
                yield return new Pos(c.Row + Dir[2].X, c.Col + Dir[2].Y, 2, 1);
                yield return new Pos(c.Row + Dir[3].X, c.Col + Dir[3].Y, 3, 1);
            }
            else if(c.Dir == 2 || c.Dir == 3)
            {
                yield return new Pos(c.Row + Dir[0].X, c.Col + Dir[0].Y, 0, 1);
                yield return new Pos(c.Row + Dir[1].X, c.Col + Dir[1].Y, 1, 1);
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
