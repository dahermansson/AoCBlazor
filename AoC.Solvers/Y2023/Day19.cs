namespace AoC.Solvers.Y2023;

public class Day19(string input) : IDay
{
    public string Output => output;
    private string output { get; set; } = default!;
    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    private Dictionary<string, WorkFlow> Workflows = [];
    private List<Part> Parts = [];
    public int Star1()
    {
        bool workflows = true;
        foreach (var s in Input)
        {
            if (s == string.Empty)
            {
                workflows = false;
                continue;
            }
            if (workflows)
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
        
        output = CalcRanges().ToString();
        return -1;
    }
        
    private long CalcRanges()
    {
        var q = new Queue<(string wf, Dictionary<int, Range> Ranges)>();
        q.Enqueue(("in", new(){
                {0, new Range(1, 4000)},
                {1, new Range(1, 4000)},
                {2, new Range(1, 4000)},
                {3, new Range(1, 4000)},
            }));
        long total = 0;
        while (q.Count > 0)
        {
            var wf = q.Dequeue();
            if (wf.wf == "A")
                {
                    long res = 1;
                    foreach (var range in wf.Ranges.Values)
                        res *= range.Max - (range.Min - 1);
                    total += res;
                    continue;
                }
                if (wf.wf == "R")
                    continue;

            var current = Workflows[wf.wf];
            foreach (var rule in current.Rules)
                if(rule.Type == RuleType.GreaterThen || rule.Type == RuleType.SmallerThen)
                {
                    var (t, f) = CreateRanges(wf.Ranges[rule.Xmas], rule);
                    var newRanges = new Dictionary<int, Range>(wf.Ranges);
                    {
                        newRanges[rule.Xmas] = t;
                        q.Enqueue((rule.Next, newRanges));
                    }
                    wf.Ranges[rule.Xmas] = f;
                }
                else
                    q.Enqueue((rule.Next, new (wf.Ranges)));
        }
        return total;
    }

    private static (Range t, Range f) CreateRanges(Range range, Rule rule)
    {
        Range @true;
        Range @false;
        if(rule.Type == RuleType.GreaterThen)
        {
            @true = new Range(rule.Comparer + 1, range.Max);
            @false = new Range(range.Min, rule.Comparer);
        }
        else
        {
            @true = new Range(range.Min, rule.Comparer-1);
            @false = new Range(rule.Comparer, range.Max);
        }
        return (@true, @false);
    }

    private static readonly Dictionary<char, int> ValueLut = new(){
        {'x', 0},
        {'m', 1},
        {'a', 2},
        {'s', 3},
    };

    class Ranges
    {
        public Dictionary<int, Range> R { get; set; }
        public Ranges()
        {
            R = new(){
                {0, new Range(1,4000)},
                {1, new Range(1, 4000)},
                {2, new Range(1, 4000)},
                {3, new Range(1, 4000)},
            };
        }
    }

    record Range(int Min, int Max);

    enum RuleType
    {
        Accept,
        Reject,
        GreaterThen,
        SmallerThen
    }


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
                Xmas = ValueLut[s[0]];
                Comparer = Utils.ExtraxtInteger(s);
                Next = s.Substring(s.IndexOf(':') + 1);
            }
            else
                Next = s;
        }
        public int Xmas { get; set; }
        public RuleType Type { get; set; }
        public string Next { get; set; }
        public int Comparer { get; set; }

        private bool Run(Part p, RuleType rule) => rule switch
        {
            RuleType.GreaterThen => p.Values[Xmas] > Comparer,
            RuleType.SmallerThen => p.Values[Xmas] < Comparer,
            _ => true
        };

        public bool Exec(Part part) => Run(part, Type);
    }
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
