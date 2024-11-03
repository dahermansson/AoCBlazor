namespace AoC.Solvers.Y2016;

public class Day03: IDay
{
    public Day03(string input)
    {
        var temp = input;
        while(temp.Contains("  "))
            temp = temp.Replace("  ", " ");
        Input = InputParsers.GetInputLines(temp).Select(t => t.Trim()).ToArray();
    }
    public string Output => throw new NotImplementedException();

    public string []Input {get; private set;}

    public int Star1()
    {
        return Input.Select(t => {
            var k = t.Split(" ");
            var d = k.Select(i => int.Parse(i)).OrderBy(p => p);
            return d.Take(2).Sum() > d.Last();
            }).Count(t => t);
    }

    public int Star2()
    {
        var groups = Input.Select((v, i) => (v, i)).GroupBy(i => i.i / 3, v => v.v).ToList();
        var triangle = new List<string>();
        foreach (var g in groups.Select(t => t.ToArray()))
        {
            var l1 = g[0].Split(" ");
            var l2 = g[1].Split(" ");
            var l3 = g[2].Split(" ");
            triangle.Add(string.Join(";", l1[0], l2[0], l3[0]));
            triangle.Add(string.Join(";", l1[1], l2[1], l3[1]));
            triangle.Add(string.Join(";", l1[2], l2[2], l3[2]));
                
        }
        return triangle.Select(t => {
            var k = t.Split(";");
            var d = k.Select(i => int.Parse(i)).OrderBy(p => p);
            return d.Take(2).Sum() > d.Last();
        }).Count(t => t);
    }
}
