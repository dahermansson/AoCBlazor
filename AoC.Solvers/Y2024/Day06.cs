namespace AoC.Solvers.Y2024;

public class Day06(string input) : IDay
{
    public string Output => throw new NotImplementedException();
    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    private readonly (int x, int y)[] Dir = [(-1, 0), (0, 1), (1, 0), (0, -1)];
    private readonly (int x, int y, int dir) Start = InputParsers.GetInputLines(input).Select((row, x) => (row, x)).Where((t) => t.row.Contains('^')).Select(t => (t.x, t.row.IndexOf('^'), 0)).Single();

    bool IsBlockCreatingLoop((int x, int y, int dir) block) => GetGuardsPath(block).Count == 0; 
    
    private HashSet<(int x, int y, int dir)> GetGuardsPath((int x, int y, int dir)? block = null)
    {
        var currentDir = 0;
        var current = Start;
        HashSet<(int x, int y, int dir)> positions = [current];
        
        while (true)
        {
            var possibleNext = (x: current.x + Dir[currentDir].x, y: current.y + Dir[currentDir].y);
            if (possibleNext.x >= Input.Length || possibleNext.x < 0
                || possibleNext.y >= Input[0].Length || possibleNext.y < 0)
                return [.. positions.DistinctBy(t => (t.x, t.y))];

            while (Input[possibleNext.x][possibleNext.y] == '#'
                   || (block != null && block.Value.x == possibleNext.x && block.Value.y == possibleNext.y))
            {
                currentDir = (currentDir + 1) % Dir.Length;
                possibleNext = (current.x + Dir[currentDir].x, current.y + Dir[currentDir].y);
            }
            current = (current.x + Dir[currentDir].x, current.y + Dir[currentDir].y, currentDir);
            if(!positions.Add(current))
                return [];
        }
    }

    public int Star1() => GetGuardsPath().Count;
    public int Star2()
    {
        var t = GetGuardsPath().Count(IsBlockCreatingLoop);

        return t;
    }
}
