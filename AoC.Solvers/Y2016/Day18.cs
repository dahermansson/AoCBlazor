namespace AoC.Solvers.Y2016;

public class Day18(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string Input { get; set; } = input;

    public int Star1()
    {
        List<string> res = [Input];
        for (int i = 0; i < 39; i++)
            res.Add(GetNextRow(res.Last().Select(t => t == '^').ToArray()));
        return res.Sum(t => t.Count(c => c == '.'));
    }
    public int Star2()
    {
        List<string> res = [Input];
        for (int i = 0; i < 399999; i++)
            res.Add(GetNextRow(res.Last().Select(t => t == '^').ToArray()));
        return res.Sum(t => t.Count(c => c == '.'));
    }
    private static string GetNextRow(bool[] s)
    {
        List<bool> res = [];
        res.Add(IsTrap([false, ..s[..2]]));
        for (int i = 0; i < s.Length-2; i++)
            res.Add(IsTrap(s.Skip(i).Take(3).ToArray()));
        res.Add(IsTrap([..s[^2..], false]));
        return string.Concat(res.Select(t => t ? '^': '.'));
    }

    private static bool IsTrap(bool[] v)
    {
        if(v[0] && v[1] && !v[2])
            return true;
        if(!v[0] && v[1] && v[2])
            return true;
        if(v[0] && !v[1] && !v[2])
            return true;
        if(!v[0] && !v[1] && v[2])
            return true;
        return false;
    }
}
