using System.Runtime.CompilerServices;
using AoC.AoCUtils;

namespace AoC.Solvers.Y2015;

public class Day11: IDay
{
    public Day11(string input)
    {
        Input = input;
        var alfaBase = Enumerable.Range(0, 26).Select(t => (char)(t+97)).ToList();
        alfaBase.Insert(0, '-');
        bc = new BaseConverter(alfaBase.ToArray());
    }
    public string Output => _output;

    private string Input {get; set;}
    private string _output {get; set;} = default!;
    private string _star1_to_star2 = string.Empty;
    private BaseConverter bc {get; set;} = default!;

    public int Star1()
    {
        var password = GeneratePassword(Input);
        _star1_to_star2 = password;
        _output = _star1_to_star2;
        return -1;
    }

    public int Star2()
    {
        if(_star1_to_star2 == string.Empty)
            Star1();
        _output = GeneratePassword(_star1_to_star2);
        return -1;
    }

    private string GeneratePassword(string pwd)
    {
        var nummer = bc.ToNumber(pwd);
        var res = string.Empty;
        do{
            nummer++;
            res = bc.ToBase(nummer);
        }while(!ValidNummer(res));
        return res;
    }

    private bool ValidNummer(string s) => !new []{'-','i','o','l'}.Any(t => s.Contains(t)) && IncreasingLetters(s) && ContainsPair(s);
    private bool IncreasingLetters(string s)
    {
        if(s.Length < 3)
            return false;
        return s.SkipLast(2).Select((c, i) =>(c, i)).Any(t => s[t.i] - s[t.i+1] == -1 && s[t.i+1] - s[t.i+2] == -1);
    }
    private bool ContainsPair(string s)
    {
        var distincts = s.Distinct();
        return distincts.Count(t => s.Contains($"{t}{t}")) > 1; 
    }
}
