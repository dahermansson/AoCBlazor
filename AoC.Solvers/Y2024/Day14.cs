using System.Text;

namespace AoC.Solvers.Y2024;

public class Day14(string input) : IDay
{
    record Robot((int X, int Y) Pos, (int Vx, int Vy) Vel, int MaxX, int MaxY)
    {
        public Robot NextPos(int seconds = 1)
        {
            var x = (Pos.X + Vel.Vx * seconds).GetWrappingIndex(MaxX);
            var y = (Pos.Y + Vel.Vy * seconds).GetWrappingIndex(MaxY);
            return this with { Pos = (x, y) };
        }
    }

    private int Height => Test ? 7 : 103;
    private int Width => Test ? 11 : 101;

    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    private bool Test { get; set; } = input.Split(Environment.NewLine).Length < 15;

    public int Star1()
    {
        var robots = Input.Select(i =>
        {
            var values = i.ExtractIntegers().ToList();
            return new Robot((values[0], values[1]), (values[2], values[3]), Width, Height).NextPos(100);
        });
    
        var xMax = Width;
        var xMid = Width / 2;
        var yMax = Height;
        var yMid = Height / 2;


        List<((int x, int y) start, (int x, int y) end)> quadrants = [
            (start: (x: 0, y: 0), end: (x: xMid - 1, y: yMid - 1)),
            (start: (x: 0, y: yMid + 1), end: (x: xMid - 1, y: yMax - 1)),
            (start: (x: xMid + 1, y: 0), end: (x: xMax - 1, y: yMid - 1)),
            (start: (x: xMid + 1, y: yMid + 1), end: (x: xMax-1, y: yMax - 1))
            ];

        return quadrants.Select(k =>
            robots.Count(r =>
                r.Pos.X >= k.start.x && r.Pos.X <= k.end.x &&
                r.Pos.Y >= k.start.y && r.Pos.Y <= k.end.y)
                )
                .Aggregate(1, (a, b) => a * b);

    }

    public int Star2()
    {
        var robots = Input.Select(i =>
        {
            var values = i.ExtractIntegers().ToList();
            return new Robot((values[0], values[1]), (values[2], values[3]), Width, Height);
        }).ToArray();

        int seconds = 0;
        while (true)
        {
            robots = robots.Select(t => t.NextPos()).ToArray();
            seconds++;
            
            //Lines with more then 10 robots
            //to a dic with line (y) as key and hash of the lines all robots x value
            var linesToCheck = robots.GroupBy(t => t.Pos.Y).Where(t => t.Count() > 10)
                .ToDictionary(key => key.First().Pos.Y, v => new HashSet<int>(v.Select(t => t.Pos.X)));
            foreach (var line in linesToCheck)
            {
                var l = CreateLine(line.Value);
                if (l.Contains("########"))
                {
                    //Console.WriteLine(PlotMap(robots));
                    return seconds;
                }
            }
        }
        
    }

    string CreateLine(HashSet<int> lineRobotsHash)
    {
        var chars = new char[Width];
        for (int x = 0; x < Width; x++)
            chars[x] = lineRobotsHash.Contains(x) ? '#' : '.';
        return new(chars);
    }

    string PlotMap(Robot[] robots)
    {
        StringBuilder sb = new();
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
                sb.Append(robots.Any(t => t.Pos.X == x && t.Pos.Y == y) ? "#" : ".");
            sb.AppendLine();
        }
        return sb.ToString();
    }
}
