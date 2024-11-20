namespace AoC.Solvers.Y2015;

public class Day05(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1() => Input.Count(t => IsValidStar1(t));

    public int Star2() => Input.Count(t => IsValidStar2(t));
    
    private bool Repeats(string str)
    {
       return str.SkipLast(2).Select((v, i) => (v,i)).Any(t => str.Any(v => str[t.i] == str[t.i+2]));
    }

    private bool MoreThenTwoPair(string str)
    {
        var pairs = str.SkipLast(1).Select((v, i) => $"{v}{str[i+1]}").ToArray();
        return pairs.Any(t => str.Split(t).Count() > 2);
    }
    private bool IsValidStar2(string s) => MoreThenTwoPair(s) && Repeats(s);
    private bool IsValidStar1(string s) => s.Count(t => "aeiou".Contains(t)) > 2 && HasDoubleLetter(s) && !Containsabcdpqxy(s);

    private bool HasDoubleLetter(string s)
    {
        for (int i = 0; i < s.Length-1; i++)
            if(s[i] == s[i+1])
                return true;
        return false;
    }

    private bool Containsabcdpqxy(string s) => new []{"ab", "cd", "pq", "xy"}.Any(t => s.Contains(t));
}
