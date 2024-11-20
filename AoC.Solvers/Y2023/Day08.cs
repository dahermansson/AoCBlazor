namespace AoC.Solvers.Y2023;

public class Day08: IDay
{
    public Day08(string input)
    {
        Input = InputParsers.GetInputLines(input);
        Directions = Input[0];
        Docs = Input.Skip(2).Select(d => new Doc(d)).ToDictionary(k => k.Id, v => v);
    }
    public string Output => output;
    private string output { get; set; } = default!;
    private string[] Input {get; set;}
    private string Directions {get; set;}
    private Dictionary<string, Doc> Docs { get; set; }
    public int Star1() => GetSteps("AAA", "ZZZ");
    public int Star2()
    {
        long lcm = 1;
        foreach (var doc in Docs.Where(t => t.Key.EndsWith('A')).Select(t => t.Key))
            lcm = AoCUtils.Utils.LCM(lcm, GetSteps(doc, "Z"));
        output = lcm.ToString();
        return -1;
    }

    private int GetSteps(string start, string end)
    {
        int i = 0;
        string current = start;
        while(!current.EndsWith(end))
            current = Directions[i++ % Directions.Length] == 'L' ? Docs[current].Left : Docs[current].Right;
        return i;
    }
    record Doc
    {
        public string Left { get; set; }
        public string Right { get; set; }
        public string Id { get; set; }
        public Doc(string s)
        {
            Id = s[..3];
            Left = s.Substring(7,3);
            Right = s.Substring(12,3);
        }
    }
}
