namespace AoC.Solvers.Y2017;

public class Day02 : IDay
{
    public Day02(string input) => Input = InputParsers.GetInputLines(input);
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; }

    public int Star1() => Input.Sum(t =>
        {
            var a = t.Split('\t').Select(int.Parse);
            return a.Max() - a.Min();
        });
    public int Star2() => Input.Sum(t =>
        {
            var ints = t.Split('\t').Select(int.Parse);
            var first = ints.Single(f => ints.Any(b => f > b && f % b == 0));
            var sec = ints.Where(s => s < first).Single(p => first % p == 0);
            return first/sec;
        });
}
