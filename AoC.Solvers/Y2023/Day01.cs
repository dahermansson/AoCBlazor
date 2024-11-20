namespace AoC.Solvers.Y2023;

public class Day01(string input) : IDay
{
    public string Output => throw new NotImplementedException();
    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    public int Star1() => Input.Sum(t => int.Parse($"{t.First(char.IsAsciiDigit)}{t.Last(char.IsAsciiDigit)}"));
    public int Star2() => Input.Sum(l => {
            var firstDigit = Digits.Select((d, i) => (i: l.IndexOf(d), v: Values[i])).Where(r => r.i > -1).MinBy(r => r.i).v;
            var lastDigit = Digits.Select((d, i) => (i: l.LastIndexOf(d), v: Values[i])).Where(r => r.i > -1).MaxBy(r => r.i).v;
            return int.Parse(firstDigit+lastDigit);
        });
    private string[] Digits { get; } = "1,2,3,4,5,6,7,8,9,one,two,three,four,five,six,seven,eight,nine".Split(',');
    private string[] Values { get; } = "1,2,3,4,5,6,7,8,9,1,2,3,4,5,6,7,8,9".Split(',');
}
