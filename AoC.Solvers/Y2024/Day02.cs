using Microsoft.Extensions.Azure;

namespace AoC.Solvers.Y2024;

public class Day02: IDay
{
    public Day02(string input) => Input = InputParsers.GetInputLines(input);
    public string Output => throw new NotImplementedException();

    private string[] Input {get; set;}

    public int Star1()
    {
        bool Safe(int[] report)
        {
            if(report.Zip(report.OrderByDescending(t => t)).Any(t => t.First != t.Second) && report.Zip(report.OrderBy(t => t)).Any(t => t.First != t.Second))
                return false;
            return report.Zip(report.Skip(1)).All(t => Math.Abs(t.First- t.Second) is 1 or 2 or 3);
        }

        return Input.Count(t => Safe(t.Split(" ").Select(int.Parse).ToArray()));
    }

    public int Star2()
    {
        return 0;
    }
}
