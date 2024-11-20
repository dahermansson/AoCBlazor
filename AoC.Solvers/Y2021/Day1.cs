namespace AoC.Solvers.Y2021;

public class Day1(string input) : IDay
{
    public string Output => throw new NotImplementedException();
    public int[] Input = InputParsers.GetInputLines<int>(input);
    private static int CountGreaterThenPrevious(IEnumerable<int> integers) => integers.Select((v, i) => new { Index = i, Value = v }).Skip(1).Count(t => t.Value > integers.ElementAt(t.Index - 1));
    public int Star1() => CountGreaterThenPrevious(Input);
    public int Star2() => CountGreaterThenPrevious(Input.SkipLast(2).Select((v, i) => Input.Skip(i).Take(3).Sum()));
}