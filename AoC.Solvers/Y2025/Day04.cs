namespace AoC.Solvers.Y2025;

public class Day04(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    private static List<Pos> AccessedByAForklift(char[][] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;

        IEnumerable<Pos> ValidAdjacents(int currentRow, int currentCol)
            => Adjacents().Select(a
                => new Pos(currentRow + a.Row, currentCol + a.Col))
                    .Where(pos => pos.Row > -1 && pos.Row < rows
                                && pos.Col > -1 && pos.Col < cols);

        return Enumerable.Range(0, rows).SelectMany(
            row => Enumerable.Range(0, rows)
                .Where(col => grid[row][col] == '@'
                    && ValidAdjacents(row, col).Count(adjacent =>
                        grid[adjacent.Row][adjacent.Col] == '@') < 4
                ).Select(c => new Pos(row, c))).ToList();
    }

    public int Star1() => 
        AccessedByAForklift([..Input.Select(row => row.ToCharArray())]).Count;
        
    public int Star2()
    {
        char[][] grid = [.. Input.Select(row => row.ToCharArray())];

        while (AccessedByAForklift(grid) is { Count: > 0 } accessedByAForklift)
            accessedByAForklift.ForEach(f => grid[f.Row][f.Col] = 'x');

        return grid.SelectMany(t => t).Count(c => c == 'x');
    }

    private record Pos(int Row, int Col);

    private static IEnumerable<Pos> Adjacents()
    {
        yield return new(-1, -1);
        yield return new(-1, 0);
        yield return new(-1, 1);
        yield return new(0, -1);
        yield return new(0, 1);
        yield return new(1, -1);
        yield return new(1, 0);
        yield return new(1, 1);
    }
}