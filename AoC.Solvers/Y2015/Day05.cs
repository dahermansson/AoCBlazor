using AoC.Utils;

namespace AoC.Solvers.Y2015;

public class Day05: IDay
{
    public Day05(string input)
    {
        Input = input;
    }
    public string Output => throw new NotImplementedException();

    private string Input {get; set;}

    public int Star1() => InputParsers.GetInputLines(Input).Count(t => IsValid(t));

    public int Star2()
    {
        return 0;
    }

    private bool IsValid(string s) => s.Count(t => "aeiou".Contains(t)) > 2 && HasDoubleLetter(s) && !Containsabcdpqxy(s);

    private bool HasDoubleLetter(string s)
    {
        for (int i = 0; i < s.Length-1; i++)
            if(s[i] == s[i+1])
                return true;
        return false;
    }

    private bool Containsabcdpqxy(string s) => new []{"ab", "cd", "pq", "xy"}.Any(t => s.Contains(t));
}
