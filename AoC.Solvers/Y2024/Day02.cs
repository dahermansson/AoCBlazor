namespace AoC.Solvers.Y2024;

public class Day02(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private List<int[]> Input { get; set; } = InputParsers.GetInputLines(input).Select(t => t.Split(" ").Select(int.Parse).ToArray()).ToList();

    private static bool IsReportSafe(int[] report)
    {
        if (report.Zip(report.OrderByDescending(t => t)).Any(t => t.First != t.Second) && report.Zip(report.OrderBy(t => t)).Any(t => t.First != t.Second))
            return false;

        return report.Zip(report.Skip(1)).All(t => Math.Abs(t.First - t.Second) is 1 or 2 or 3);
    }

    static bool ProblemDampener(int[] report)
    {
        return Enumerable.Range(0, report.Length).Any(removeIndex =>
            IsReportSafe([.. report.Where((_, index) => index != removeIndex)]));
    }

    public int Star1() => Input.Count(IsReportSafe);

    public int Star2() => Input.Count(ProblemDampener);
}