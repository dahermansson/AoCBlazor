namespace AoC.Solvers.Y2016;

public class Day15(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private List<Disc> Input { get; set; } = InputParsers.GetInputLines(input).Select((t, i) =>
    {
        var s = t.Split(" ");
        return new Disc(i + 1, int.Parse(s[3]), int.Parse(s.Last().Trim('.')));
    }).ToList();

    public int Star1() => Enumerable.Range(0, int.MaxValue).First(t => Input.TrueForAll(d => d.Pass(t)));

    public int Star2()
    {
        Input.Add(new Disc(Input.Count + 1, 11, 0));
        return Enumerable.Range(0, int.MaxValue).First(t => Input.TrueForAll(d => d.Pass(t)));
    }

    record Disc(int Index, int Positions, int AtZero)
    {
        public bool Pass(int time) => (AtZero + time + Index) % Positions == 0;
    }
}
