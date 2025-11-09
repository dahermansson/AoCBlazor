namespace AoC.Solvers.Y2024;

public class Day25(string input) : IDay
{
    public string Output => throw new NotImplementedException();
    private readonly string Input = input;

    private IEnumerable<(int[] Pins, bool IsLock)> LocksAndKeys { get; set; } = input.Split($"{Environment.NewLine}{Environment.NewLine}").Select(g =>
        {
            var isLock = g.StartsWith("#####");
            string[] lines = isLock ? [.. g.Split(Environment.NewLine)] : [.. g.Split(Environment.NewLine).Reverse()];

            int[] pins = Enumerable.Repeat(-1, 5).ToArray();
            foreach (var line in lines.Skip(1).Index())
                foreach (var pin in line.Item.Index())
                    if (pins[pin.Index] == -1 && pin.Item == '.')
                        pins[pin.Index] = line.Index;

            return (Pins: pins, IsLock: isLock);
        });

    public int Star1() => LocksAndKeys.Where(kl => kl.IsLock).Sum(l => LocksAndKeys.Count(k => !k.IsLock && l.Pins.Zip(k.Pins).All(p => p.First + p.Second <= 5)));

    public int Star2()
    {
        return 0;
    }
}
