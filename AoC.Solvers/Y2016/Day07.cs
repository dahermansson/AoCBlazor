using AoC.AoCUtils;

namespace AoC.Solvers.Y2016;

public class Day07(string input) : IDay
{
    public string Output => throw new NotImplementedException();
    private IP[] Input { get; set; } = InputParsers.GetInputLines(input).Select(t => new IP(t)).ToArray();
    public int Star1() => Input.Count(t => TLS(t));
    public int Star2() => Input.Count(t => SSL(t));
    private bool TLS(IP ip) => ip.Outer.Any(t => HasABBA(t) && !ip.Hypernet.Any(t => HasABBA(t)));
    private bool HasABBA(string s)
    {
        if (s.Length < 4)
            return false;
        for (int i = 0; i <= s.Length - 4; i++)
        {
            var a = s[i];
            var b = s[i + 1];
            var c = s[i + 2];
            var d = s[i + 3];
            if (a == d && b == c && b != a)
                return true;
        }
        return false;
    }
    private List<string> FindABA(string s)
    {
        var res = new List<string>();
        if (s.Length < 3)
            return res;
        for (int i = 0; i <= s.Length - 3; i++)
        {
            var a = s[i];
            var b = s[i + 1];
            var c = s[i + 2];
            if (a == c && a != b)
                res.Add(string.Concat(a, b, c));
        }
        return res;
    }
    private string CreateBAB(string s) => string.Concat(s[1], s[0], s[1]);
    private bool SSL(IP ip)
    {
        var aba = ip.Outer.Select(t => FindABA(t)).SelectMany(t => t).ToList();
        if (aba.Any())
            return aba.Select(t => CreateBAB(t)).Any(t => ip.Hypernet.Any(p => p.Contains(t)));
        return false;
    }

    private record IP
    {
        public List<string> Outer { get; set; } = new();
        public List<string> Hypernet { get; set; } = new();
        public IP(string s)
        {
            string[] temps = s.Split("[");

            Outer.Add(temps[0]);
            Outer.AddRange(temps.Skip(1).Select(t => string.Concat(t.SkipWhile(p => p != ']').Skip(1))));
            Hypernet.AddRange(temps.Skip(1).Select(t => string.Concat(t.TakeWhile(p => p != ']'))));
        }

    }
}