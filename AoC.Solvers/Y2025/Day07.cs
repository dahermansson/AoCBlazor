namespace AoC.Solvers.Y2025;

public class Day07(string input) : IDay
{
    public string Output => _output;
    private string _output = string.Empty;
    private string Input { get; set; } = input;
    private string[] Tachyon_manifold => InputParsers.GetInputLines(Input);
    private int Start => Tachyon_manifold.First().IndexOf('S');

    public int Star1() => Tachyon_manifold.Skip(1).Aggregate((Splits: 0, Beams: new HashSet<int> { Start }), (current_beams, row) =>
    {
        var new_beams = current_beams.Beams.Select(beam => row[beam] == '^'
            ? (Beams: [beam - 1, beam + 1], Split: true)
            : (Beams: new List<int> { beam }, Split: false));

        return (current_beams.Splits + new_beams.Count(t => t.Split), [.. new_beams.SelectMany(t => t.Beams)]);
    }).Splits;

    public int Star2()
    {
        var tachyon_beams = Tachyon_manifold.Skip(1).Aggregate(new List<Beam> { new(Start, 1) }, (current_beams, row) =>
        {
            var new_beams = current_beams.Select(beam => row[beam.Pos] == '^'
                    ? [new(beam.Pos - 1, beam.Paths), new(beam.Pos + 1, beam.Paths)]
                    : new List<Beam> { beam })
                .SelectMany(t => t);
            return [.. new_beams.GroupBy(t => t.Pos).Select(t => new Beam(t.Key, t.Sum(p => p.Paths)))];
        });

        _output = tachyon_beams.Sum(t => t.Paths).ToString();
        return -1;
    }

    private record Beam(int Pos, long Paths);
}