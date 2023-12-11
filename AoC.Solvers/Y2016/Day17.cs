using System.Security.Cryptography;

namespace AoC.Solvers.Y2016;

public class Day17 : IDay
{
    public Day17(string input)
    {
        Input = input;
        DoorOpener = MD5.Create();
    }
    public string Output => output;
    private string output = string.Empty;
    private MD5 DoorOpener { get; init; }

    private string Input { get; set; }
    private int GridSize { get; set; } = 4;

    public int Star1()
    {
        var start = new Position(0, 0, "");
        var end = new Position(3, 3, "");
        var open = new Queue<Position>();
        var visited = new HashSet<Position>();
        open.Enqueue(start);
        while (open.Count > 0)
        {
            var current = open.Dequeue();
            visited.Add(current);
            if (current.X == end.X && current.Y == end.Y)
            {
                output = current.Path;
                return -1;
            }
            foreach (var next in GetNext(current))
            {
                if (visited.Contains(next))
                    continue;
                open.Enqueue(next);
            }
        }
        return 0;
    }

    public int Star2()
    {
        var start = new Position(0, 0, "");
        var end = new Position(3, 3, "");
        var open = new Queue<Position>();
        var visited = new HashSet<Position>();
        int pathLength = 0;
        open.Enqueue(start);
        while (open.Count > 0)
        {
            var current = open.Dequeue();
            visited.Add(current);
            if (current.X == end.X && current.Y == end.Y)
            {
                if (current.Path.Length > pathLength)
                    pathLength = current.Path.Length;
                continue;
            }
            foreach (var next in GetNext(current))
            {
                if (visited.Contains(next))
                    continue;
                open.Enqueue(next);
            }
        }
        return pathLength;
    }
    IEnumerable<Position> GetNext(Position current) =>
        OpenDoors(current.Path).Select(t => NextPosition(current, t));

    Position NextPosition(Position current, int dir)
    {
        if (dir == 0 && current.X > 0) return new Position(current.X - 1, current.Y, current.Path + "U");
        if (dir == 1 && current.X < GridSize - 1) return new Position(current.X + 1, current.Y, current.Path + "D");
        if (dir == 2 && current.Y > 0) return new Position(current.X, current.Y - 1, current.Path + "L");
        if (dir == 3 && current.Y < GridSize - 1) return new Position(current.X, current.Y + 1, current.Path + "R");
        return current;
    }

    IEnumerable<int> OpenDoors(string path)
    {
        var keys = "bcdef";
        var inputBytes = System.Text.Encoding.ASCII.GetBytes(Input + path);
        var hashBytes = DoorOpener.ComputeHash(inputBytes);
        var doors = Convert.ToHexString(hashBytes).ToLower().Take(4);
        return doors.Select((d, i) => (d, i)).Where(t => keys.Contains(t.d)).Select(t => t.i);
    }
    record Position(int X, int Y, string Path);
}
