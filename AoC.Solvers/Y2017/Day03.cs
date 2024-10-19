namespace AoC.Solvers.Y2017;

public class Day03 : IDay
{
    public Day03(string input) => Input = int.Parse(input);
    public string Output => _output;
    private int Input { get; set; }
    private string _output { get; set; } = string.Empty;

    public int Star1()
    {
        Dictionary<int, MazePosition> maze = [];
        int size = 2;
        int i = 1;
        int x = -1, y = 1;
        maze.Add(i++, new(0, 0));
        while (i <= Input)
        {
            for (int j = 0; j < size; j++)
                maze.Add(i++, new(++x, y));

            for (int j = 0; j < size; j++)
                maze.Add(i++, new(x, --y));

            for (int j = 0; j < size; j++)
                maze.Add(i++, new(--x, y));

            for (int j = 0; j < size; j++)
                maze.Add(i++, new(x, ++y));
            y++;
            x--;
            size += 2;
        }
        return Utils.ManhattanDistance(0, 0, maze[Input].Y, maze[Input].X);
    }

    public int Star2()
    {
        Dictionary<int, MazePosition> maze = [];

        long SumOfNeighbors(int x, int y) =>
            maze.Where(t => Math.Abs(t.Value.X - x) <= 1 && Math.Abs(t.Value.Y - y) <= 1).Sum(t => t.Value.Value);

        int size = 2;
        int i = 1;
        int x = -1, y = 1;
        maze.Add(i++, new(0, 0, 1));
        while (maze.Values.Max(t => t.Value) <= Input)
        {
            for (int j = 0; j < size; j++)
            {
                var v = SumOfNeighbors(++x, y);
                maze.Add(i++, new(x, y, v));
            }

            for (int j = 0; j < size; j++)
            {
                var v = SumOfNeighbors(x, --y);
                maze.Add(i++, new(x, y, v));
            }

            for (int j = 0; j < size; j++)
            {
                var v = SumOfNeighbors(--x, y);
                maze.Add(i++, new(x, y, v));
            }

            for (int j = 0; j < size; j++)
            {
                var v = SumOfNeighbors(x, ++y);
                maze.Add(i++, new(x, y, v));
            }
            y++;
            x--;
            size += 2;
        }
        _output = maze.Values.OrderBy(t => t.Value).First(t => t.Value > Input).Value.ToString();
        return -1;
    }

    private record MazePosition(int X, int Y, long Value = 0);
}
