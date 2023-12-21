namespace AoC.Solvers.Y2023;

public class Day21(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        var start = FindStart() ?? throw new Exception("No start found");
        Queue<Pos> possiblePos =new();
        possiblePos.Enqueue(start);
        
        for (int step = 0; step < 64; step++)
        {
            List<Pos> nextPossible = [];
            while(possiblePos.TryDequeue(out var p))
                nextPossible.AddRange(GetNexts(p).Where(t => IsGardenPlots(t)).ToArray());
            possiblePos = new Queue<Pos>(nextPossible.Distinct());
        }
        return possiblePos.Count;
    }

    public int Star2()
    {
        var start = FindStart() ?? throw new Exception("No start found");
        Queue<Pos> possiblePos = new();
        possiblePos.Enqueue(start);
        
        for (int step = 0; step < 6; step++)
        {
            List<Pos> nextPossible = [];
            while(possiblePos.TryDequeue(out var p))
                nextPossible.AddRange(GetNexts(p).Where(t => IsGardenPlots2(t)).ToArray());
            possiblePos = new Queue<Pos>(nextPossible.Distinct());

            /*
            for (int row = 0; row < Input.Length; row++)
            {
                for (int col = 0; col < Input[row].Length; col++)
                    Console.Write(possiblePos.Contains(new Pos(row, col))? "O" : ".");
                Console.WriteLine();
            }
            Console.WriteLine();
            */
        }

        
        return possiblePos.Count;
    }
    private bool IsGardenPlots(Pos p)
    {
        if(p.X < 0 || p.X >= Input.Length || p.Y < 0 || p.Y >= Input[p.X].Length)
            return false;
        return Input[p.X][p.Y] == '.' || Input[p.X][p.Y] == 'S';
    }
    private bool IsGardenPlots2(Pos p)
    {
        var x = p.X % Input.Length;
        var y = p.Y % Input[0].Length;
        x += Input.Length;
        x %= Input.Length;
        y += Input[0].Length;
        y %= Input[0].Length;

        return Input[x][y] == '.' || Input[x][y] == 'S';
    }

    private static IEnumerable<Pos> GetNexts(Pos current)
    {
        yield return new Pos(current.X + 1, current.Y);
        yield return new Pos(current.X - 1, current.Y);
        yield return new Pos(current.X, current.Y + 1);
        yield return new Pos(current.X, current.Y - 1);
    }
    private Pos? FindStart()
    {
        for (int i = 0; i < Input.Length; i++)
            if(Input[i].Contains('S'))
                return new Pos(i, Input[i].IndexOf('S'));
        return null;
    }

    record Pos(int X, int Y);
}
