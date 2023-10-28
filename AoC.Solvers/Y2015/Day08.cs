using System.Text.RegularExpressions;
using AoC.Utils;

namespace AoC.Solvers.Y2015;

public class Day08: IDay
{
    public Day08(string input)
    {
        Input = InputParsers.GetInputLines(input).ToArray();
    }
    public string Output => throw new NotImplementedException();

    private string[] Input {get; set;}

    public int Star1() => Input.Sum(s => s.Length) - Input.Sum(s => CountChars(s));

    public int Star2() => Input.Sum(s => CountEncode(s)) - Input.Sum(s => s.Length);

    private int CountChars(string s)
    {   
        var trimmed = Regex.Replace(s, @"\\x[0-9a-f]{2}", "-");
        trimmed = Regex.Replace(trimmed, @"\\\\", "-");
        trimmed = Regex.Replace(trimmed, @"\\""", "-");
        return trimmed.Length -2;
    }
    private int CountEncode(string s)
    {   
        var t = s.Replace(@"\", @"\\");
        t = t.Replace("\"", @"\""");
        return t.Length + 2;
    }
}
