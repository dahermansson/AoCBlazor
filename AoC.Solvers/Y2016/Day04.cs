using System.Text;
using AoC.AoCUtils;

namespace AoC.Solvers.Y2016;

public class Day04(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    //176749 to high
    //174976 to high
    //173787
    public int Star1()
    {
        return Input.Where(t => Validate(t)).Select(t => t.ExtraxtPositivInteger()).Sum();
    }

    public bool Validate(string s)
    {
        
        var alphabet = GetAlphabet();
        var room = string.Concat(s.TakeWhile(t => t != '['));
        foreach (var c in room)
        {
            if(alphabet.ContainsKey(c))
                alphabet[c]++;
        }
        var hash = s.Substring(s.IndexOf('[')+1).Trim(']');
        if(hash.Any(t => !room.Contains(t)))
            return false;
        
        for(int i = 0;i<hash.Length-1;i++)
        {
            if(alphabet[hash[i]] ==0)
                return false;
            if(alphabet[hash[i]] < alphabet[hash[i+1]])
                return false;
            if(alphabet[hash[i]] == alphabet[hash[i+1]] && hash[i] - hash[i+1] > 1)
                return false;
        }

        return true;
    }

    private string Decrypt(string s, int id)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in s)
        {
            if(c == '-')
                sb.Append(' ');
            else
                sb.Append(c + (id % 26) > 122 ? "a" :  Convert.ToChar(c+(id % 26)));
        }
        return sb.ToString();

    }

    private Dictionary<char, int> GetAlphabet() =>
        Enumerable.Range(97, 26).ToDictionary(k => (char)k, v => 0);
    

    public int Star2()
    {
        return Input.Where(t => Validate(t) && Decrypt(t, t.ExtraxtPositivInteger()).Contains("north".ToLower())).First().ExtraxtPositivInteger();
    }
}
