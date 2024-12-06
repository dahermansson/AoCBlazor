namespace AoC.Solvers.Y2024;

public class Day06(string input) : IDay
{
    public string Output => throw new NotImplementedException();
    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    private readonly (int x, int y)[] Dir = [(-1, 0), (0, 1), (1, 0), (0, -1)];
    private readonly (int x, int y) Start = InputParsers.GetInputLines(input).Select((row, x) => (row, x)).Where((t) => t.row.Contains('^')).Select(t => (t.x, y: t.row.IndexOf('^'))).Single();

    bool IsBlockCreatingLoop((int x, int y) block) => GetGuardsPath(block).Count == 0; 
    
    private HashSet<(int x, int y)> GetGuardsPath((int x, int y)? block = null)
    {
        var currentDir = 0;
        var current = Start;
        HashSet<(int x, int y)> positions = [current];
        var stepsSensLastAdded = 0;
        while (true)
        {
            var possibleNext = (x: current.x + Dir[currentDir].x, y: current.y + Dir[currentDir].y);
            if (possibleNext.x >= Input.Length || possibleNext.x < 0
                || possibleNext.y >= Input[0].Length || possibleNext.y < 0)
                return positions;

            while (Input[possibleNext.x][possibleNext.y] == '#'
                   || (block != null && block.Value.x == possibleNext.x && block.Value.y == possibleNext.y))
            {
                currentDir = (currentDir + 1) % Dir.Length;
                possibleNext = (current.x + Dir[currentDir].x, current.y + Dir[currentDir].y);
            }
            current = (current.x + Dir[currentDir].x, current.y + Dir[currentDir].y);
            stepsSensLastAdded = positions.Add(current) ? 0 : stepsSensLastAdded + 1;
            if (stepsSensLastAdded > positions.Count)
                return [];
        }
    }

    public int Star1() => GetGuardsPath().Count;
    public int Star2() => GetGuardsPath().Count(IsBlockCreatingLoop);
}
