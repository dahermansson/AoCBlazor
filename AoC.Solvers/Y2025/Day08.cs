namespace AoC.Solvers.Y2025;

public class Day08(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string Input { get; set; } = input;
    private Junctionbox[] Junctionboxes => InputParsers.GetInputLines(Input).Select(t => new Junctionbox(t.Split(","))).ToArray();

    public int Star1()
    {
        var closestPairsToConnect = Junctionboxes.Select((j1, skip)
        => Junctionboxes.Skip(skip + 1)
            .Select(j2 => new Pair(j1, j2)))
                .SelectMany(t => t)
                .OrderBy(t => t.StraightLineDistance())
                .Take(1000)
                .ToList();

        List<HashSet<Junctionbox>> circuits = [];

        foreach (var pair in closestPairsToConnect)
        {
            var circuit = circuits.Where(c => c.Contains(pair.J1) || c.Contains(pair.J2)).ToList();
            if (circuit.Count == 2)
            {
                circuits.Add([.. circuit[0], .. circuit[1], pair.J1, pair.J2]);
                circuits.Remove(circuit[0]);
                circuits.Remove(circuit[1]);
            }
            else if (circuit.Count == 1)
            {
                var c = circuit.Single();
                c.Add(pair.J1);
                c.Add(pair.J2);
            }
            else if (circuit.Count == 0)
            {
                circuits.Add([pair.J1, pair.J2]);
            }
        }

        return circuits.OrderByDescending(t => t.Count).Take(3).Select(t => t.Count).Aggregate(1, (s, t) => s * t);
    }

    public int Star2()
    {
        var paird_junction_boxes = Junctionboxes.Select((j1)
            => Junctionboxes.Select(j2 => new Pair(j1, j2)))
                .SelectMany(t => t)
                .OrderBy(t => t.StraightLineDistance())
                .ToList();

        List<HashSet<Junctionbox>> circuits = [];
        int extension_cable_length = 0;
        foreach (var pair in paird_junction_boxes)
        {
            var circuit = circuits.Where(c => c.Contains(pair.J1) || c.Contains(pair.J2)).ToList();
            if (circuit.Count == 2)
            {
                circuits.Add([.. circuit[0], .. circuit[1], pair.J1, pair.J2]);
                circuits.Remove(circuit[0]);
                circuits.Remove(circuit[1]);
                if (circuits.Count == 1)
                {
                    extension_cable_length = pair.J1.X * pair.J2.X;
                    break;
                }
            }
            else if (circuit.Count == 1)
            {
                var c = circuit.Single();
                c.Add(pair.J1);
                c.Add(pair.J2);
            }
            else if (circuit.Count == 0)
            {
                circuits.Add([pair.J1, pair.J2]);
            }
        }
        return extension_cable_length;
    }

    private record Junctionbox
    {
        public Junctionbox(string[] S)
        {
            X = int.Parse(S[0]);
            Y = int.Parse(S[1]);
            Z = int.Parse(S[2]);

        }
        public int X { get; init; }
        public int Y { get; init; }
        public int Z { get; init; }
    }

    private record Pair(Junctionbox J1, Junctionbox J2)
    {
        public double StraightLineDistance() =>
            Math.Sqrt(
                Math.Pow(J1.X - J2.X, 2)
                + Math.Pow(J1.Y - J2.Y, 2)
                + Math.Pow(J1.Z - J2.Z, 2));
    }
}
