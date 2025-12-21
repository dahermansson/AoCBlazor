namespace AoC.Solvers.Y2025;

public class Day12(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        var presentsSizes = Input.Chunk(5).Take(6).Select((pressentParts, pressenName) => (pressenName, pressentParts.Sum(pp => pp.Count(p => p == '#')))).ToDictionary();
        
        return Input.Where(t => t.Contains('x')).Select(t =>
        {
            var tree = t.Split(':');
                var treeRegionSize = tree[0].Split('x').Aggregate(1, (product, a) => product * int.Parse(a));
                var presentsTotalSize = tree[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select((p, i) => int.Parse(p) * presentsSizes[i]).Sum();
                return (treeRegionSize, presentsTotalSize);
        }).Count(tree => tree.treeRegionSize > tree.presentsTotalSize);
    }

    public int Star2() => 22;
}