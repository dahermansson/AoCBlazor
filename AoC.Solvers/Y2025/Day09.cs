using NetTopologySuite.Geometries;

namespace AoC.Solvers.Y2025;

public class Day09(string input) : IDay
{
    public string Output => _output;
    private string _output = string.Empty;

    private string Input { get; set; } = input;

    private readonly GeometryFactory geometryFactory = new();

    public int Star1()
    {
        var koordinates = InputParsers.GetInputLines(Input).Select(t => new Point(long.Parse(t.Split(',')[0]), long.Parse(t.Split(',')[1])));
        var pairs = koordinates.Select((p1, i) => koordinates.Skip(i + 1).Select(p2 => (p1, p2))).SelectMany(t => t);
        var bigestSquare = pairs.Max(t => (Math.Abs(t.p1.X - t.p2.X) + 1) * (Math.Abs(t.p1.Y - t.p2.Y) + 1));
        _output = bigestSquare.ToString();
        return -1;
    }

    public int Star2()
    {
        var coordinates = InputParsers.GetInputLines(Input).Select(t => new Coordinate(double.Parse(t.Split(',')[0]), double.Parse(t.Split(',')[1]))).ToArray();
        var polygon = geometryFactory.CreatePolygon([.. coordinates, coordinates.First()]);
        var rectangle = coordinates.Select((c1, i) => coordinates.Skip(i + 1).Select(c2 => new CoordinatesPair(c1, c2)))
            .SelectMany(t => t)
            .Select(b => geometryFactory.CreatePolygon(b.BBox))
            .OrderByDescending(t => t.Area).First(polygon.Contains);

        var p1 = rectangle.Coordinates[0];
        var p2 = rectangle.Coordinates[2];
        _output = ((Math.Abs(p1.X - p2.X) + 1) * (Math.Abs(p1.Y - p2.Y) + 1)).ToString();
        return -1;
    }

    record Point(long X, long Y);
    record CoordinatesPair(Coordinate Coordinate1, Coordinate Coordinate2)
    {
        public Coordinate[] BBox => [
            new (Coordinate1.X, Coordinate1.Y),
            new (Coordinate2.X, Coordinate1.Y),
            new (Coordinate2.X, Coordinate2.Y),
            new (Coordinate1.X, Coordinate2.Y),
            new (Coordinate1.X, Coordinate1.Y)];
    }
}
