namespace AoC.Solvers.Y2023;

public class Day18(string input) : IDay
{
    public string Output { get; private set; } = string.Empty;
    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    public int Star1()
    {
        List<Point> polygon = [new Point(0, 0, 0)];
        foreach (var s in Input)
            polygon.Add(GetNextPoint(s, polygon.Last()));

        var area = polygon.Take(polygon.Count - 1)
            .Select((p, i) => (polygon[i + 1].X - p.X) * (polygon[i + 1].Y + p.Y))
            .Sum() / 2 + polygon.Sum(t => t.Steps) / 2 + 1;
        Output = area.ToString();
        return -1;
    }

    public int Star2()
    {
        List<Point> polygon = [new Point(0, 0, 0)];
        foreach (var s in Input)
            polygon.Add(GetNextPointHex(s, polygon.Last()));

        var area = polygon.Take(polygon.Count - 1)
            .Select((p, i) => (polygon[i + 1].X - p.X) * (polygon[i + 1].Y + p.Y))
            .Sum() / 2 + polygon.Sum(t => t.Steps) / 2 + 1;
        Output = area.ToString();
        return -1;
    }
    private static Point GetNextPoint(string s, Point prevPoint)
    {
        var steps = int.Parse(s.Split(" ")[1]);
        if (s[0] == 'U')
            return new Point(prevPoint.X - steps, prevPoint.Y, steps);
        if (s[0] == 'D')
            return new Point(prevPoint.X + steps, prevPoint.Y, steps);
        if (s[0] == 'R')
            return new Point(prevPoint.X, prevPoint.Y + steps, steps);
        if (s[0] == 'L')
            return new Point(prevPoint.X, prevPoint.Y - steps, steps);
        return prevPoint;
    }
    private static Point GetNextPointHex(string s, Point prevPoint)
    {
        var hex = s.Split(" ")[2].Trim('(', ')', '#');
        var steps = long.Parse(hex[0..^1], System.Globalization.NumberStyles.HexNumber);
        if (hex[5] == '0') // R
            return new Point(prevPoint.X, prevPoint.Y + steps, steps);
        if (hex[5] == '1') //D
            return new Point(prevPoint.X + steps, prevPoint.Y, steps);
        if (hex[5] == '2') //L
            return new Point(prevPoint.X, prevPoint.Y - steps, steps);
        if (hex[5] == '3') // U
            return new Point(prevPoint.X - steps, prevPoint.Y, steps);
        return prevPoint;
    }
    record Point(long X, long Y, long Steps);
}
