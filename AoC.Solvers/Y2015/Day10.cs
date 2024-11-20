using System.Collections;
using System.Text;
using AoC.AoCUtils;

namespace AoC.Solvers.Y2015;

public class Day10(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string Input { get; set; } = input;

    public int Star1()
    {
        var res = Input;
        for (int i = 0; i < 40; i++)
            res = LookAndSay(res);
        Star1_to_Star2 = res;
        return res.Length;
    }

    private string Star1_to_Star2 {get; set;} = string.Empty;

    public int Star2()
    {
        if(Star1_to_Star2 == string.Empty)
            Star1();
        var res = Star1_to_Star2;
        for (int i = 0; i < 10; i++)
            res = LookAndSay(res);
        
        return res.Length;
    }

    private string LookAndSay(string s)
    {
        var res = new StringBuilder();
        int index = 0;
        var t = s.Select((t,i) => (t, i)).ToArray();
        while(index < s.Length)
        {
            var n = s[index];
            var say = t.Skip(index).TakeWhile(t => t.t == n).ToArray(); 
            res.Append($"{say.Length}{n}");
            index = say.Last().i+1;
        }
        return res.ToString();
    }
}
