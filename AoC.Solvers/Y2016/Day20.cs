namespace AoC.Solvers.Y2016;

public class Day20 : IDay
{
    public Day20(string input) => Input = InputParsers.GetInputLines(input);
    public string Output => res.ToString();
    private long res = 0;
    private string[] Input { get; set; }

    public int Star1()
    {
        var ranges = Input.Select(t =>
        {
            var r = t.Split("-");
            return new Range(long.Parse(r[0]), long.Parse(r[1]));
        }).ToList();

        var mergeobj = FindMergable(ranges);
        while (mergeobj != null)
        {
            mergeobj.Value.range.Merge(mergeobj.Value.merge);
            ranges.Remove(mergeobj.Value.merge);
            mergeobj = FindMergable(ranges);
        }
        res = ranges.OrderBy(t => t.Min).First().Max + 1;
        return -1;
    }

    public int Star2()
    {
        var ranges = Input.Select(t =>
        {
            var r = t.Split("-");
            return new Range(long.Parse(r[0]), long.Parse(r[1]));
        }).ToList();

        var mergeobj = FindMergable(ranges);
        while (mergeobj != null)
        {
            mergeobj.Value.range.Merge(mergeobj.Value.merge);
            ranges.Remove(mergeobj.Value.merge);
            mergeobj = FindMergable(ranges);
        }

        long allowed = 4294967295L + 1;
        var blacklist = ranges.OrderBy(t => t.Min).ToList();
        foreach (var range in blacklist)
            allowed -= range.Blackisted;

        res = allowed;
        return -1;
    }

    private static (Range range, Range merge)? FindMergable(List<Range> ranges)
    {
        var range = ranges.FirstOrDefault(r => ranges.Any(m => r != m && r.Mergeable(m)));
        if (range == null)
            return null;
        return (range, ranges.First(m => range != m && range.Mergeable(m)));
    }

    private class Range(long min, long max)
    {
        public long Min { get; set; } = min;
        public long Max { get; set; } = max;
        public long Blackisted => Max - Min + 1;
        public bool Mergeable(Range candidate)
        {
            if (candidate.Max - Min == 1 || (candidate.Min < Min && candidate.Max > Min))
                return true;
            if (candidate.Min - Max == 1 || (candidate.Min < Max && candidate.Max > Max))
                return true;
            return false;
        }

        public void Merge(Range merge)
        {
            if (merge.Max - Min == 1 || (merge.Min < Min && merge.Max > Min))
                Min = merge.Min;
            if (merge.Min - Max == 1 || (merge.Min < Max && merge.Max > Max))
                Max = merge.Max;
        }
    }
}
