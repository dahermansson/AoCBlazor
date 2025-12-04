namespace AoC.Solvers.Y2025;

public class Day04(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        var grid = Input.Select(row => row.Select(col => col).ToArray()).ToArray();
        int rows = grid.Length;
        int cols = grid[0].Length;

        var accessable = Enumerable.Range(0, rows).SelectMany(row => {
            var k =  Enumerable.Range(0, cols).Where(col =>
            {
                return grid[row][col] == '@' && Adjacents.Count(a => 
                    row + a.Row >= 0 && row + a.Row < rows &&
                    col + a.Col >= 0 && col + a.Col < cols &&
                    grid[row + a.Row][col + a.Col] == '@') < 4;
            });
            return k.Select(a => new Pos(row, a));
        });

        return accessable.Count();
    }

    public int Star2()
    {
        var grid = Input.Select(row => row.Select(col => col).ToArray()).ToArray();
        int rows = grid.Length;
        int cols = grid[0].Length;
        List<Pos> accessable = [];
        do {
            accessable = Enumerable.Range(0, rows).SelectMany(row => {
            var k =  Enumerable.Range(0, cols).Where(col =>
            {
                return grid[row][col] == '@' && Adjacents.Count(a => 
                    row + a.Row >= 0 && row + a.Row < rows &&
                    col + a.Col >= 0 && col + a.Col < cols &&
                    (grid[row + a.Row][col + a.Col] == '@')) < 4;
            });
            return k.Select(a => new Pos(row, a));
            }).ToList();
            accessable.ForEach(f => grid[f.Row][f.Col] = 'x');

        } while (accessable.Count != 0);
        
        return grid.SelectMany(t => t).Count(c => c == 'x');
    }

    private record Pos(int Row, int Col);

    private static readonly List<Pos> Adjacents = [
        new (-1, -1),
        new (-1, 0),
        new (-1, 1),
        new (0, -1),
        new (0, 1),
        new (1, -1),
        new (1, 0),
        new (1, 1)
    ]; 
}