using Microsoft.Toolkit.HighPerformance;

namespace AoC.Solvers.Y2017;

public class Day04(string input) : IDay
{
    public string Output => throw new NotImplementedException();
    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1() => Input.Count(t => t.Split(" ").Length == t.Split(" ").GroupBy(w => w).Count());

    public int Star2()
    {
        bool HasNotAnagram(string pwd)
        {
            var words = pwd.Split(" ").Select((t, i) => (Index: i, Word: t));
            return !words.Any(a => words.Any(b => a.Index != b.Index && string.Concat(a.Word.Order()) == string.Concat(b.Word.Order())));
        }
        return Input.Count(HasNotAnagram);
    }
}
