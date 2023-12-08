using AoC.AoCUtils;

namespace AoC.Solvers.Y2023;

public class Day05: IDay
{
    public Day05(string input)
    {
        Input = input;
        Maps = ParseInput(Input);
        MapsRev = [];
        for (int i = Maps.Count-1; i > -1; i--)
            MapsRev.Add(Maps[i]);
    }
    public string Output => output;

    private string Input {get; set;}
    private string output {get; set;} = string.Empty;
    private long[] Seeds { get; set; } = default!;
    private List<Map> Maps { get; set;}
    private List<Map> MapsRev { get; set; }
    public int Star1()
    {
        long min = long.MaxValue;
        foreach (var seed in Seeds)
        {
            var dest = GetLocationNr(seed);
            if(dest < min)
                min = dest;
        }
        output = min.ToString();
        return -1;
    }

    public int Star2()
    {
        var seedRanges = new List<(long Start, long End)>();
        for (int i = 0; i < Seeds.Length/2; i+=2)
            seedRanges.Add((Seeds[i], Seeds[i] + Seeds[i+1]));

        long seed = 0;
        long testLocation = 1;
        bool foundSeed = false;
        while(true)
        {
            seed = GetSeed(testLocation);
            if(seedRanges.Any(t => t.Start <= seed && t.End >= seed))
            {
                foundSeed = true;
                testLocation -=1;
            }
            else
            {
                if(foundSeed)
                    break;
                testLocation +=1000;
            }
        }
        output = testLocation.ToString();
        return -1;
    }

    private long GetLocationNr(long seed)
    {
        var dest = seed;
        foreach (var map in Maps)
        {
            dest = map.GetDestination(dest);
        }
        return dest;
    }

    private long GetSeed(long location)
    {
        var seed = location;
        foreach (var map in MapsRev)
        {
            seed = map.GetSeed(seed);
        }
        return seed;
    }

    private List<Map> ParseInput(string s)
    {
        var lines = InputParsers.GetInputLines(s);
        Seeds = lines.First().Substring(lines.First().IndexOf(":")+1).Trim().Split(" ").Select(t => long.Parse(t.Trim())).ToArray();

        return s.Split($"{Environment.NewLine}{Environment.NewLine}").Skip(1).Select(t => new Map(t)).ToList();
    }

    record Map
    {
        public string Name { get; set; }
        public List<Range> Ranges { get; set; }

        public Map(string s)
        {
            var lines = s.Split(Environment.NewLine);
            Name = lines[0].Replace(" map:", "");
            Ranges = lines.Skip(1).Select(t => { var n = t.Split(" ").Select(long.Parse).ToArray(); return new Range(n[0], n[1], n[2]);}).ToList();
        }
        
        public long GetDestination(long seedNr)
        {
            var r = Ranges.FirstOrDefault(t => t.GetSource.start <= seedNr && seedNr <= t.GetSource.stop);
            if(r != null)
                return r.DestStart + (seedNr - r.SourceStart);
            else
                return seedNr;
        }

        public long GetSeed(long location)
        {
            var r = Ranges.FirstOrDefault(t => t.GetDest.start <= location && location <= t.GetDest.stop);
            if(r != null)
                return r.SourceStart + (location - r.DestStart);
            else
                return location;
        }

    }
    record Range(long DestStart, long SourceStart, long Length)
    {
        public (long start, long stop) GetSource => (SourceStart, SourceStart+Length);
        public (long start, long stop) GetDest => (DestStart, DestStart+Length);

    }
}
