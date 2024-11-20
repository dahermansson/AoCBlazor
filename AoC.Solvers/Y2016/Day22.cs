namespace AoC.Solvers.Y2016;

public class Day22(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input).Skip(2).ToArray();

    public int Star1()
    {
        var nodes = Input.Select(s => new Node(s));
        return nodes.Select(n => nodes.Count(t => n.Used > 0 && t.Key != n.Key && n.CanMoveTo(t))).Sum();
    }

    public int Star2()
    {
        var nodes = Input.Select(s => new Node(s));
        var maxX = nodes.Max(t => t.X);
        var maxY = nodes.Max(t => t.Y);
        var empty = nodes.Single(t => t.Used == 0);
        // to round the blocking wall for empty node
        var stepsToMoveEmpty = empty.X + empty.Y + maxX-1;
        //4 + 1 to move goal one step except first and last
        return ((maxX-1)*5) + stepsToMoveEmpty + 1;
    }

    private class Node
    {
        public Node(string s)
        {
            var parts = s.Split(" ").Where(t => t.Length > 0).ToList();
            Key = parts.First();
            X = int.Parse(Key.Split("-")[1].Remove(0,1));
            Y = int.Parse(Key.Split("-")[2].Remove(0,1));
            Size = int.Parse(parts[1].Replace("T", ""));
            Used = int.Parse(parts[2].Replace("T", ""));
            Avil = int.Parse(parts[3].Replace("T", ""));
            Usep = int.Parse(parts[4].Replace("%", ""));
        }
        public string Key { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public int Used { get; set; }
        public int Avil { get; set; }
        public int Usep { get; set; }

        public bool CanMoveTo(Node to) => to.Avil > Used;
    }
}