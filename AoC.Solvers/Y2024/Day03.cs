using System.Text.RegularExpressions;

namespace AoC.Solvers.Y2024;

public partial class Day03(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string Input { get; set; } = input;

    [GeneratedRegex(@"(mul\(\d{1,3},\d{1,3}\))")]
    private static partial Regex RegexStar1();

    [GeneratedRegex(@"(mul\(\d{1,3},\d{1,3}\))|(don't\(\))|(do\(\))")]
    private static partial Regex RegexStar2();

    public int Star1() => RegexStar1().Matches(Input).Sum(t =>
            t.Value.ExtractIntegers().Aggregate((a, b) => a * b));

    public int Star2()
    {
        bool doMulti = true;
        var sum = 0;

        foreach (var match in RegexStar2().Matches(Input).Select(t => t.Value))
        {
            if (match == "do()")
                doMulti = true;
            else if (match == "don't()")
                doMulti = false;
            else
                sum += doMulti ? match.ExtractIntegers().Aggregate((a, b) => a * b) : 0;
        }
        return sum;
    }
}