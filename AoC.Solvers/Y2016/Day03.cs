using AoC.Utils;

namespace AoC.Solvers.Y2016;

public class Day03: IDay
{
    public Day03(string input)
    {
        while(input.Contains("  "))
            input = input.Replace("  ", " ");
        Input = InputParsers.GetInputLines(input).Select(t => t.Trim()).ToArray();
    }
    public string Output => throw new NotImplementedException();

    public string []Input {get; private set;}

    public int Star1()
    {
        return Input.Select(t => {
            var k = t.Split(" ");
            var d = k.Select(i => int.Parse(i)).OrderBy(p => p);
            return d.Take(2).Sum() > d.Last();
            }).Count(t => t);
    }

    public int Star2()
    {
        return 0;
    }
}
