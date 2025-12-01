namespace AoC.Solvers.Y2025;

public class Day01(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private int[] Input { get; set; } = [..InputParsers.GetInputLines(input)
        .Select(t => t[0] == 'L' ? int.Parse(t.TrimStart('L')) * -1 : int.Parse(t.TrimStart('R')))];

    public int Star1() => Input.Aggregate((Zeros: 0, Position: 50), (agg, current) =>
            (Zeros: GetReminder(agg.Position + current, 100) is { } position && position == 0
            ? agg.Zeros + 1
            : agg.Zeros, Position: position))
        .Zeros;

    public int Star2() => Input.Aggregate((Zeros: 0, Position: 50), (agg, current) =>
        {
            return (Zeros: GetReminder(agg.Position + current, 100) is { } position &&
                position == 0 ||
                agg.Position != 0 &&
                current < 0 && position > agg.Position
                || (current > 0 && position < agg.Position)
                ? agg.Zeros + 1 + GetLaps(current)
                : agg.Zeros + GetLaps(current)
                , Position: position);

        }).Zeros;

    static int GetLaps(int distance) => (int)Math.Floor(Math.Abs(distance) / (double)100);
    private static int GetReminder(int a, int b) => a - b * (int)Math.Floor(a / (double)b);
}