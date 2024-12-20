namespace AoC.Solvers.Y2021;

public class Day10(string input) : IDay
{
    public string Output => _output.ToString();

    private string[] Input = InputParsers.GetInputLines(input);
    private List<char> OpenChunk = ['(', '[', '{', '<'];
    private List<char> CloseChunk = [')', ']', '}', '>'];
    private Dictionary<char, int> ChunkPoints = new()
        {
            {')', 3},
            {']', 57},
            {'}', 1197},
            {'>', 25137}
        };

    private Dictionary<char, int> IncompletePoints = new()
        {
            {')', 1},
            {']', 2},
            {'}', 3},
            {'>', 4}
        };

    private List<string> Incomplete = [];

    public int Star1()
    {
        var points = ChunkPoints.ToDictionary(key => key.Value, value => 0);
        foreach (var chunk in Input)
        {
            var point = Corupt(chunk);
            if (point != 0)
                points[point]++;
            else
                Incomplete.Add(chunk);
        }
        return points.Where(t => t.Key != '0').Sum(t => t.Key * t.Value);
    }

    private long _output;
    public int Star2()
    {
        var res = new List<long>();
        foreach (var incomplete in Incomplete)
            res.Add(IncompleteCheck(incomplete));
        _output = res.OrderBy(s => s).ElementAt((int)Math.Floor((decimal)res.Count / 2));
        return -1;
    }

    private int Corupt(string chunk)
    {
        var stack = new Stack<char>();
        foreach (var bracet in chunk)
            if (OpenChunk.Contains(bracet))
                stack.Push(bracet);
            else if (CloseChunk.Contains(bracet))
                if (OpenChunk.IndexOf(stack.Peek()) == CloseChunk.IndexOf(bracet))
                    stack.Pop();
                else
                    return ChunkPoints[bracet];
        return 0;
    }

    private long IncompleteCheck(string chunk)
    {
        var stack = new Stack<char>();
        foreach (var bracet in chunk)
        {
            if (OpenChunk.Contains(bracet))
                stack.Push(bracet);
            else if (CloseChunk.Contains(bracet))
                if (OpenChunk.IndexOf(stack.Peek()) == CloseChunk.IndexOf(bracet))
                    stack.Pop();
        }
        long sum = 0;
        foreach (var cc in stack)
        {
            sum *= 5;
            sum += IncompletePoints[CloseChunk.ElementAt(OpenChunk.IndexOf(cc))];
        }

        return sum;
    }
}