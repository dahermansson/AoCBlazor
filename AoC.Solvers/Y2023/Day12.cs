namespace AoC.Solvers.Y2023;

public class Day12: IDay
{
    public Day12(string input) => Input = InputParsers.GetInputLines(input);
    public string Output => throw new NotImplementedException();

    private string[] Input {get; set;}

    public int Star1()
    {
        var sum = 0;
        Input = ["?#?#?#?#?#?#?#? 1,3,1,6"];

        foreach (var row in Input)
        {
            var spring = row.Split(" ")[0];
            var unknowns = spring.IndexOfMany(c => c == '?').ToArray();
            var brokenGroups = row.Split(" ")[1].Split(',').Select(int.Parse).ToArray();
            var res = new List<char[]>();
            GetArrangements([..spring], unknowns, res);
            
            var hepp = res.Where(k => IsValid(string.Concat(k), brokenGroups)).ToList();
        }
        return sum;
    }
    public int Star2()
    {
        return 0;
    }

    private void GetArrangements(char[] s, int[] unknowns, List<char[]> res)
    {
        if(!s.Any(t => t == '?'))
        {
            res.Add(s);
            return;
        }
        else
        {
            foreach (var unknown in unknowns)
            {
                s[unknown] = '.';
                GetArrangements([..s], [..unknowns.Skip(1)], res);
                s[unknown] = '#';
                GetArrangements([..s], [..unknowns.Skip(1)], res);
            }
        }
        
    }

    private bool IsValid(string springs, int[] brokenGroups)
    {
        var broken = springs.Split('.').Where(t => t.All(c => c == '#') && t.Length != 0).ToArray();
        for(int i = 0; i <broken.Length; i++)
            if(broken[i].Length != brokenGroups[i])
                return false;
        return true;
    }
}
