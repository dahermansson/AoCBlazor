using AoC.Utils;

namespace AoC.Solvers.Y2023;

public class Day05: IDay
{
    public Day05(string input) => Input = input;
    public string Output => throw new NotImplementedException();

    private string Input {get; set;}
    private int[] Seeds;
    public int Star1()
    {
        ParseInput(Input);
        return 0;
    }

    public int Star2()
    {
        return 0;
    }

    private void ParseInput(string s)
    {
        var lines = InputParsers.GetInputLines(s);
        Seeds = lines.First().Substring(lines.First().IndexOf(":")+1).Trim().Split(" ").Select(t => int.Parse(t.Trim())).ToArray();

        var maps = s.Split($"{Environment.NewLine}{Environment.NewLine}").Skip(1).Select(t => new Map(t)).ToList();


    }

    record Map
    {
        public string Name { get; set; }
        public List<Range> Ranges { get; set; }

        public Map(string s)
        {
            var lines = s.Split(Environment.NewLine);
            Name = lines[0].Replace(" map:", "");
            Ranges = lines.Skip(1).Select(t => { var n = t.Split(" ").Select(int.Parse).ToArray(); return new Range(n[0], n[1], n[2]);}).ToList();
        }
        
        public int GetDestination(int seedNr)
        {
            var r = Ranges.SingleOrDefault(t => t.GetSource.start <= seedNr && seedNr <= t.GetSource.stop);
            if(r != null)
                return 0;
            else
                return 0;
        }

    }
    record Range(int DestStart, int SourceStart, int Length)
    {
        public (int start, int stop) GetSource => (SourceStart, SourceStart+Length);
    }
}
