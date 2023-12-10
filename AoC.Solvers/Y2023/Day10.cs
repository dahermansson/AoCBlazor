using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Prepared;
namespace AoC.Solvers.Y2023;

public class Day10 : IDay
{
    public Day10(string input)
    {
        Input = InputParsers.GetInputLines(input).Select(t => t.ToList()).ToList();
        Maze = Input.Select(t => t.ToArray()).ToArray();
    }
    public string Output => throw new NotImplementedException();

    private List<List<char>> Input { get; set; }
    private char[][] Maze { get; set; }
    public int Star1()
    {
        var goal = GetStart();
        Dictionary<(int x, int y), int> distances = [];
        distances[goal] = 0;
        foreach (var start in GetStarts(goal))
        {
            var current = start;
            var prev = goal;
            var steps = 1;
            do
            {
                if (!distances.TryGetValue(current, out int value) || value > steps)
                    distances[current] = steps;
                var temp = current;
                current = NextPart(current, prev);
                prev = temp;
                steps++;
            } while (current != goal);

        }
        return distances.Max(t => t.Value);
    }


    public int Star2()
    {
        var goal = GetStart();
        var start = GetStarts(goal).First();
        List<(int x, int y)> pointsInLoop = [goal, start];

        var current = start;
        var prev = goal;
        do
        {
            var temp = current;
            current = NextPart(current, prev);
            prev = temp;
            pointsInLoop.Add(current);
        } while (current != goal);

        var geoFactory = new PreparedGeometryFactory();
        var loop = geoFactory.Create(
        Geometry.DefaultFactory.CreatePolygon(pointsInLoop.Select(t => new Coordinate(t.x, t.y)).ToArray()));

        int inLoop = 0;
        for (int x = 0; x < Maze.Length; x++)
            for (int y = 0; y < Maze[0].Length; y++)
                if (loop.Contains(Geometry.DefaultFactory.CreatePoint(new Coordinate(x, y))))
                    inLoop++;
        return inLoop;
    }

    private (int X, int Y) GetStart()
    {
        var x = Input.FindIndex(t => t.Contains('S'));
        var y = Input.ElementAt(x).IndexOf('S');
        return (x, y);
    }

    private IEnumerable<(int x, int y)> GetStarts((int x, int y) s)
    {
        if (s.x + 1 < Maze.Length - 1 && new[] { '|', 'J', 'L' }.Contains(Maze[s.x + 1][s.y]))
            yield return (s.x + 1, s.y);
        if (s.x > 0 && new[] { '|', 'F', '7' }.Contains(Maze[s.x - 1][s.y]))
            yield return (s.x - 1, s.y);
        if (s.y + 1 < Maze[0].Length - 1 && new[] { '-', '7', 'J' }.Contains(Maze[s.x][s.y + 1]))
            yield return (s.x, s.y + 1);
        if (s.y > 0 && new[] { '-', 'F', 'L' }.Contains(Maze[s.x][s.y - 1]))
            yield return (s.x, s.y - 1);
    }
    private (int X, int Y) NextPart((int x, int y) c, (int x, int y) p)
    {
        var current = Maze[c.x][c.y];
        if (current == 'S')
        {
            if (new[] { '|', 'J', 'L' }.Contains(Maze[c.x + 1][c.y]))
                return (c.x + 1, c.y);
            if (new[] { '|', 'F', '7' }.Contains(Maze[c.x - 1][c.y]))
                return (c.x - 1, c.y);
            if (new[] { '-', '7', 'J' }.Contains(Maze[c.x][c.y + 1]))
                return (c.x, c.y + 1);
            if (new[] { '-', 'F', 'L' }.Contains(Maze[c.x][c.y - 1]))
                return (c.x, c.y - 1);
        }
        else
        {
            if (current == '|')
            {
                if (c.x + 1 != p.x)
                    return (c.x + 1, c.y);
                else
                    return (c.x - 1, c.y);
            }
            else if (current == '-')
            {
                if (c.y + 1 != p.y)
                    return (c.x, c.y + 1);
                else
                    return (c.x, c.y - 1);
            }
            else if (current == 'J')
            {
                if (c.x - 1 != p.x)
                    return (c.x - 1, c.y);
                else
                    return (c.x, c.y - 1);
            }
            else if (current == '7')
            {
                if (c.x + 1 != p.x)
                    return (c.x + 1, c.y);
                else
                    return (c.x, c.y - 1);
            }
            else if (current == 'F')
            {
                if (c.x + 1 != p.x)
                    return (c.x + 1, c.y);
                else
                    return (c.x, c.y + 1);
            }
            else if (current == 'L')
            {
                if (c.x - 1 != p.x)
                    return (c.x - 1, c.y);
                else
                    return (c.x, c.y + 1);
            }

        }
        return (-1, -1);
    }

}
