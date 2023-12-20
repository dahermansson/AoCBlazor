namespace AoC.Solvers.Y2023;

public class Day19(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    private Dictionary<string, WorkFlow> Workflows = [];
    private List<Part> Parts = [];
    public int Star1()
    {
        bool workflows = true;
        foreach (var s in Input)
        {
            if(s == string.Empty)
            {
                workflows = false;
                continue;
            }
            if(workflows)
                Workflows.Add(s[..s.IndexOf('{')], new WorkFlow(s));
            else
                Parts.Add(new Part(s));
        }
        return Parts.Where(p => RunPart(p)).Sum(p => p.Values.Sum());
    }

    private bool RunPart(Part part)
    {
        var next = "in";
        while (!new string[] { "A", "R" }.Contains(next))
            next = Workflows[next].Run(part);
        return next == "A";
    }



    public int Star2()
    {
        return CalcRanges().Count();
    }

    private IEnumerable<Ranges> CalcRanges()
    {
        var q = new Queue<(string wf, Ranges R)>();
        q.Enqueue(("in", new Ranges()));

        while(q.Count > 0)
        {
            var wf = q.Dequeue();
            if(wf.wf == "A")
                yield return wf.R;
            if(wf.wf == "R")
                yield return wf.R;
            var current = Workflows[wf.wf];
            
            foreach (var rule in current.Rules)
            {
                if(rule.Next == "A")
                {
                    yield return wf.R;
                    continue;
                }
                if(rule.Next == "R")
                {
                    yield return wf.R;
                    continue;
                }
                q.Enqueue((rule.Next, wf.R));
            }
        }
    }

    private static Ranges CreateRange(Ranges ranges, Rule rule)
    {
        return null;
        //var range = ranges.R[rule.Value]
        //return ;
    }

    private static readonly Dictionary<char, int> ValueLut = new(){
        {'x', 0},
        {'m', 1},
        {'a', 2},
        {'s', 3},
    };

    class Ranges
    {
        public Dictionary<int, List<Range>> R { get; set; }
        public Ranges()
        {
            R = new(){
                {0, new List<Range>()},
                {1, new List<Range>()},
                {2, new List<Range>()},
                {3, new List<Range>()},
            };
        }
    }

    record Range(int Min, int Max);

    enum RuleType
    {
        GreaterThen,
        SmallerThen,
        Accept,
        Reject
    }

    //in{s<1351:px,qqz}
    class WorkFlow(string s)
    {
        public List<Rule> Rules { get; set; } = s[s.IndexOf('{')..].Trim('{', '}').Split(',').Select(r => new Rule(r)).ToList();
        public string Run(Part part) => Rules.First(t => t.Exec(part)).Next;

    }

    class Rule
    {
        public Rule(string s)
        {
            if (s.Contains(':'))
            {
                Type = s.Contains('>') ? RuleType.GreaterThen : RuleType.SmallerThen;
                Value = ValueLut[s[0]];
                I = Utils.ExtraxtInteger(s);
                Next = s.Substring(s.IndexOf(':') + 1);
            }
            else
                Next = s;
        }

        public int Value { get; set; }
        public RuleType Type { get; set; }
        public string Next { get; set; }
        public int I { get; set; }

        private bool Run(Part p, RuleType rule) => rule switch
        {
            RuleType.GreaterThen => p.Values[Value] > I,
            RuleType.SmallerThen => p.Values[Value] < I,
            _ => true
        };

        public bool Exec(Part part) => Run(part, Type);
    }


    //{x=787,m=2655,a=1222,s=2876}
    record Part
    {
        public int X { get; set; }
        public int M { get; set; }
        public int A { get; set; }
        public int S { get; set; }
        public int[] Values { get; set; }
        public Part(string s)
        {
            var parts = s.Split(",");
            X = Utils.ExtraxtInteger(parts[0]);
            M = Utils.ExtraxtInteger(parts[1]);
            A = Utils.ExtraxtInteger(parts[2]);
            S = Utils.ExtraxtInteger(parts[3]);
            Values = [X, M, A, S];
        }
    }
}
