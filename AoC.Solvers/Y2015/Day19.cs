namespace AoC.Solvers.Y2015;

public class Day19(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private Replacement Parse(string s) => new Replacement(s.Split(" ").First(), s.Split(" ").Last());

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

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
        
        var reduceFunctions = replacements.Where(t => t.With.Contains("Rn") && t.With.EndsWith("Ar")).Select(t => 
            (FuncMolecule: t.With.Substring(0, t.With.IndexOf("Rn")), With: t.With, Replace: t.Replace)).ToList();
        var funcBreakingMolecules = replacements.Where(t => !t.With.Contains("Rn") && 
            reduceFunctions.Any(f => t.With.EndsWith(f.FuncMolecule))).ToList();
        var reduceMoleculs = replacements.Where(t => !funcBreakingMolecules.Any(f => f.With == t.With) && !reduceFunctions.Any(r => r.With == t.With)).ToList();
        
        var run = ReducRun.ReduceFuncs;
        var steps = 0;
        while(molecule != targetMolecule)
        {
            var savedMolecule = molecule;
            if(run == ReducRun.ReduceFuncs)
                foreach (var replace in reduceFunctions)
                {
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
            else if(run == ReducRun.ReduceMoleculs)
                foreach (var replace in reduceMoleculs)
                {
                    if(replace.Replace == "e" && molecule != replace.With)
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
            else
                foreach (var replace in funcBreakingMolecules)
                {
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
                if(run == ReducRun.ReduceFuncs)
                    run = ReducRun.ReduceMoleculs;
                else if(run == ReducRun.ReduceMoleculs && reduceFunctions.Any(t => molecule.Contains(t.With)))
                    run = ReducRun.ReduceFuncs;
                else if(run == ReducRun.ReduceMoleculs)
                    run = ReducRun.FuncBreakingMolecules;
                else
                    run = ReducRun.ReduceFuncs;
                Console.WriteLine(molecule);
            }
        }
        return steps;
    }

    enum ReducRun
    {
        ReduceFuncs,
        FuncBreakingMolecules,
        ReduceMoleculs
    }

    private record Replacement(string Replace, string With);
}
