using AoC.AoCUtils;

namespace AoC.Solvers.Y2015;

public class Day02: IDay
{
    public Day02(string input)
    {
        Input = input;
    }
    public string Output => throw new NotImplementedException();

    private string Input {get; set;}

    public int Star1()
    {
        return InputParsers.GetInputLines(Input).Select(t => Parse(t)).Select(t => new { l = t[0]*t[1], w = t[1] * t[2], h = t[2]*t[0]}).Select(t => 2 * t.l + 2 * t.w + 2 * t.h + new int[]{t.l, t.w, t.h}.Min()).Sum();
    }

    public int Star2()
    {

        return InputParsers.GetInputLines(Input).Select(t => Parse(t)).Select(t => new { sides = t.OrderBy(a => a).Take(2).ToArray(), bow = t[0] * t[1] * t[2]}).Select(t => t.sides[0]+t.sides[0]+t.sides[1]+t.sides[1] + t.bow).Sum();
    }

    private int[] Parse(string s)
    {
        return s.Split("x").Select(t => int.Parse(t)).ToArray();
    }
}
