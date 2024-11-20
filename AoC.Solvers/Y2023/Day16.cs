namespace AoC.Solvers.Y2023;

public class Day16(string input) : IDay
{
    public string Output => throw new NotImplementedException();
    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    public int Star1() => BeamTiles(new Pos(0, 0, 0));
    
    public int Star2()
    {
        List<Pos> starts = [];
        for (int row = 0; row < Input.Length; row++)
            starts.AddRange([new Pos(row, 0, 0), new Pos(row, Input[row].Length-1, 1)]);       
        for (int col = 0; col < Input[0].Length; col++)
            starts.AddRange([new Pos(0, col, 2), new Pos(Input.Length-1, col, 3)]);       
        return starts.Max(t => BeamTiles(t));
    }

    private int BeamTiles(Pos start)
    {
        HashSet<Pos> v = [];
        Queue<Pos> q = [];
        q.Enqueue(start);
        while(q.Count > 0)
        {
            var current = q.Dequeue();
            if(current.Col >= Input[0].Length || current.Row >= Input.Length)
                continue;
            List<Pos> nexts = GetNext(current).Where(n => !v.Contains(n)).ToList();
            foreach (var next in nexts)
            {
                if(next.Row < 0 || next.Col < 0 || next.Row >= Input.Length || next.Col >= Input[next.Row].Length)
                    continue;
                q.Enqueue(next);
            }
            v.Add(current);
        }
        return v.DistinctBy(t => t.RowAndCol).Count();
    }

    private List<Pos> GetNext(Pos current)
    {
        List<Pos> res = [];
        if(Input[current.Row][current.Col] == '.')
            res.Add(new Pos(current.Row + Dir[current.Dir].X, current.Col + Dir[current.Dir].Y, current.Dir));
        if(Input[current.Row][current.Col] == '|')
            if(current.Dir > 1)
                res.Add(new Pos(current.Row + Dir[current.Dir].X, current.Col + Dir[current.Dir].Y, current.Dir));
            else
            {
                res.Add(new Pos(current.Row + Dir[2].X, current.Col + Dir[2].Y, 2));
                res.Add(new Pos(current.Row + Dir[3].X, current.Col + Dir[3].Y, 3));
            }
        if(Input[current.Row][current.Col] == '-')
            if(current.Dir < 2)
                res.Add(new Pos(current.Row + Dir[current.Dir].X, current.Col + Dir[current.Dir].Y, current.Dir));
            else
            {
                res.Add(new Pos(current.Row + Dir[0].X, current.Col + Dir[0].Y, 0));
                res.Add(new Pos(current.Row + Dir[1].X, current.Col + Dir[1].Y, 1));
            }
        if(Input[current.Row][current.Col] == '\\')
            if(current.Dir == 0)
                res.Add(new Pos(current.Row + Dir[2].X, current.Col + Dir[2].Y, 2));
            else if(current.Dir == 1)
                res.Add(new Pos(current.Row + Dir[3].X, current.Col + Dir[3].Y, 3));
            else if(current.Dir == 3)
                res.Add(new Pos(current.Row + Dir[1].X, current.Col + Dir[1].Y, 1));
            else if(current.Dir == 2)
                res.Add(new Pos(current.Row + Dir[0].X, current.Col + Dir[0].Y, 0));
        
        if(Input[current.Row][current.Col] == '/')
            if(current.Dir == 0)
                res.Add(new Pos(current.Row + Dir[3].X, current.Col + Dir[3].Y, 3));
            else if(current.Dir == 1)
                res.Add(new Pos(current.Row + Dir[2].X, current.Col + Dir[2].Y, 2));
            else if(current.Dir == 2)
                res.Add(new Pos(current.Row + Dir[1].X, current.Col + Dir[1].Y, 1));
            else if(current.Dir == 3)
                res.Add(new Pos(current.Row + Dir[0].X, current.Col + Dir[0].Y, 0));
        return res;
    }
   
    record Pos(int Row, int Col, int Dir)
    {
        public (int Row, int Col) RowAndCol => (Row, Col);
    }

    private readonly List<(int X, int Y)> Dir = [
        (0, 1),
        (0, -1),
        (1, 0),
        (-1, 0)
    ];
}
