namespace AoC.Solvers.Y2024;

public class Day01(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private IEnumerable<(int left, int right)> Input { get; set; } = InputParsers.GetInputLines(input).Select(t =>
    {
        var locationIds = t.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        return (int.Parse(locationIds[0]), int.Parse(locationIds[1]));
    });

    public int Star1() => Input.Select(t => t.left).OrderBy(t => t).Zip(Input.Select(t => t.right).OrderBy(t => t)).Sum(t => Math.Abs(t.First - t.Second));

    public int Star2() => Input.Sum(t => t.left * Input.Count(s => s.right == t.left));
}