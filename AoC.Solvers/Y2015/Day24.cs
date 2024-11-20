namespace AoC.Solvers.Y2015;

public class Day24(string input) : IDay
{
    public string Output => output;
    private long[] Input { get; set; } = [.. InputParsers.GetInputLines(input).Select(t => long.Parse(t)).OrderByDescending(t => t)];

    public int Star1()
    {
        output = FindBestLegRoom(3).ToString();
        return -1;
    }
    public int Star2()
    {
        output = FindBestLegRoom(4).ToString();
        return -1;
    }

    public long FindBestLegRoom(int numberOfGroups)
    {
        var groupWeigth = Input.Sum()/numberOfGroups;
        int maxGroupSize = Input.Length/numberOfGroups;
        List<Group> res =new();
        FindGroups([], Input, groupWeigth, res, ref maxGroupSize);
        return res.Min(t => t.QE);
    }

    private string output { get; set; } = string.Empty;
    private void FindGroups(long[] used, long[] left, long sum, List<Group> res, ref int maxGroupSize)
    {
        if(used.Sum() > sum || used.Length > maxGroupSize)
            return;
        if(used.Sum() == sum)
        {
            res.Add(new Group(used));
            if(used.Length < maxGroupSize)
                maxGroupSize = used.Length;
            return;
        }
        for (int i = 0; i < left.Length; i++)
        {
            if(left[i] > sum - used.Sum())
                continue;
            FindGroups([..used, left[i]], left.Skip(i+1).ToArray(), sum, res, ref maxGroupSize);
        }
        return;
    }
    private record Group(long[] Ints)
    {
        public long QE => Ints.Aggregate((a,b)=> a*b);
        public bool NotAnySame(Group g) => !Ints.Intersect(g.Ints).Any();
        public long Weigt => Ints.Sum();
    }
}
