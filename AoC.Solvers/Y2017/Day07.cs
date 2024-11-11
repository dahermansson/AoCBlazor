namespace AoC.Solvers.Y2017;

public class Day07 : IDay
{
    public Day07(string input) => Input = InputParsers.GetInputLines(input);
    public string Output => _output;

    private string[] Input { get; set; }
    private string _output { get; set; } = "";

    public int Star1()
    {
        var programs = Input.Select(t =>
        {
            var splits = t.Replace("(", "").Replace(")", "").Split("->");
            if (splits.Length == 2)
            {
                var p = splits.First().Split(" ");
                var holdsUp = splits.Last().Replace(" ", "").Split(",");

                return new Program(p.First(), int.Parse(p[1]), [.. holdsUp]);
            }
            else
            {
                var p = splits.First().Split(" ");
                return new Program(p.First(), int.Parse(p.Last()), []);
            }
        });

        var allHoldsUp = programs.SelectMany(t => t.HoldsUp);
        var root = programs.Single(t => !allHoldsUp.Contains(t.Name));
        _output = root.Name;
        return -1;
    }
   
    public int Star2()
    {
        _output = "";
        var programs = Input.Select(t =>
        {
            var splits = t.Replace("(", "").Replace(")", "").Split("->");
            if (splits.Length == 2)
            {
                var p = splits.First().Split(" ");
                var holdsUp = splits.Last().Replace(" ", "").Split(",");

                return new Program(p.First(), int.Parse(p[1]), [.. holdsUp]);
            }
            else
            {
                var p = splits.First().Split(" ");
                return new Program(p.First(), int.Parse(p.Last()), []);
            }
        }).ToDictionary(k => k.Name, v => v);

        var allHoldsUp = programs.Values.SelectMany(t => t.HoldsUp);
        var root = programs.Single(t => !allHoldsUp.Contains(t.Key));
        
        _ = WeigthDiff(root.Value, programs);

        return -1;
    }


    private (int ownWeigth, int totalWeigth) WeigthDiff(Program program, Dictionary<string, Program> programs)
    {
        if (program.HoldsUp.Count == 0)
            return (program.Weigth, program.Weigth);
        

        var sums = program.HoldsUp.Select(s => (name: s, ownWeigth: programs[s].Weigth, totalWeigth: programs[s].Weigth + programs[s].HoldsUp.Sum(t => WeigthDiff(programs[t], programs).totalWeigth))).GroupBy(k => k.totalWeigth);
        
        if (sums.Any(t => t.Key != sums.First().Key))
        {
            var toHeavy = sums.MaxBy(t => t.Key)!;
            var newWeigth = toHeavy.First().ownWeigth - (sums.Max(k => k.Key) - sums.Min(m => m.Key));
            if(_output == "")
                _output = newWeigth.ToString();
            return (newWeigth, newWeigth + sums.Sum(t => t.Sum(p => p.totalWeigth)));
        }
        else
            return (program.Weigth, program.Weigth + sums.Sum(t => t.Sum(p => p.totalWeigth)));
    }

    private record Program(string Name, int Weigth, List<string> HoldsUp);
}
