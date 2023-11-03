using AoC.Utils;

namespace AoC.Solvers.Y2015;

public class Day19: IDay
{
    public Day19(string input)
    {
        Input = InputParsers.GetInputLines(input);
    }
    public string Output => throw new NotImplementedException();

    private Replacement Parse(string s) => new Replacement(s.Split(" ").First(), s.Split(" ").Last());

    private string[] Input {get; set;}

    public int Star1()
    {
        var res = new List<string>();
        var molecule = Input.Last();
        var replacements = Input.TakeWhile(t => !string.IsNullOrEmpty(t)).Select(t => Parse(t)).ToList();
        foreach (var replace in replacements)
        {
            var i = -replace.Replace.Length;
            while((i = molecule.IndexOf(replace.Replace, i+replace.Replace.Length)) > -1)
            {
                var str = molecule;
                str = str.Remove(i, replace.Replace.Length);
                str = str.Insert(i, replace.With);
                res.Add(str);
            }
        }

        return res.Distinct().Count();
    }

    public int Star2()
    {
        var molecule = Input.Last();
        var targetMolecule = "e";
        var replacements = Input.TakeWhile(t => !string.IsNullOrEmpty(t)).Select(t => Parse(t)).OrderByDescending( t=> t.With.Length).ToList();
        var steps = 0;
        while(molecule != targetMolecule)
        {
            var savedMolecule = molecule;
            foreach (var replace in replacements)
            {
                if(replace.Replace == "e" && molecule != replace.With)
                    continue;
                if(molecule.Any(t => char.IsLower(t)) && !replace.With.Any(t => char.IsLower(t)))
                    continue;
                

                var i = -1;
                while( (i = molecule.IndexOf(replace.With)) > -1)
                {
                    if(i != -1)
                    {
                        molecule = molecule.Remove(i, replace.With.Length);
                        molecule = molecule.Insert(i, replace.Replace);
                        steps++;       
                    }
                }
            }
            if(molecule == savedMolecule)
            {
                steps = 0;
                replacements = replacements.OrderBy(t => Guid.NewGuid()).ToList();
                molecule = Input.Last();
            }
        }

        return steps;
    }

    private record Replacement(string Replace, string With);
}
