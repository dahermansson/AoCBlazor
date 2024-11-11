namespace AoC.Solvers.Y2017;

public class Day07: IDay
{
    public Day07(string input) => Input = InputParsers.GetInputLines(input);
    public string Output => _output;

    private string[] Input {get; set;}
    private string _output {get; set;} = "";

    //jtlqxy
    //ytrsmfx
    public int Star1()
    {
        //Input = InputParsers.GetInputLines("""
        //pbga (66)
        //xhth (57)
        //ebii (61)
        //havc (66)
        //ktlj (57)
        //fwft (72) -> ktlj, cntj, xhth
        //qoyq (66)
        //padx (45) -> pbga, havc, qoyq
        //tknk (41) -> ugml, padx, fwft
        //jptl (61)
        //ugml (68) -> gyxo, ebii, jptl
        //gyxo (61)
        //cntj (57)
        //""");



        var programs = Input.Select(t => {
            var splits = t.Replace("(", "").Replace(")", "").Split("->");
            if(splits.Length == 2)
            {
                var p = splits.First().Split(" ");
                var holdsUp = splits.Last().Replace(" ", "").Split(",");
            
                return new Program(p.First(), int.Parse(p[1]), [..holdsUp]);
            }
            else
            {
                var p = splits.First().Split(" ");
                return new Program(p.First(), int.Parse(p.Last()), []);
            }
        });

        var allHoldsUp = programs.SelectMany(t => t.HoldsUp);
        var root = programs.Single(t => !allHoldsUp.Contains(t.Name));

        //var te = programs.Where(t => t.HoldsUp.Count() == 0);
        //var current = root;
        //while(current.HoldsUp.Count > 0)
        //{
        //    current = programs.SingleOrDefault(t => t.Name == current.HoldsUp.Last());
        //}

        _output = root.Name;
        return -1;
    }

    public int Star2()
    {
        return 0;
    }

    private record Program (string Name, int Weigth, List<string> HoldsUp);
}
