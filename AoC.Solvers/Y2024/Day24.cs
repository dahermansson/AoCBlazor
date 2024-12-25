namespace AoC.Solvers.Y2024;

public class Day24(string input) : IDay
{
    public string Output => output;
    private string output = string.Empty;
    private Dictionary<string, bool> StartValues { get; init; } = InputParsers.GetInputLines(input).TakeWhile(row => row.Length > 0).Select(t =>
    {
        var v = t.Split(":");
        return (Wire: v[0], value: int.Parse(v[1].Trim()) == 1);
    }).ToDictionary();
    private Dictionary<string, Gate> Gates { get; init; } = InputParsers.GetInputLines(input).SkipWhile(row => row.Length == 0 || row.Contains(':')).Select(t =>
    {
        var v = t.Split(" ");
        return new Gate(v[4], v[1], v[0], v[2]);
    }).ToDictionary(key => key.Wire, v => v);

    private record Gate(string Wire, string Operator, string Input1, string Input2);

    public int Star1()
    {
        bool GetWireValue(string wire)
        {
            if (StartValues.TryGetValue(wire, out var value))
                return value;
            var gate = Gates[wire];
            if (gate.Operator == "AND")
                return GetWireValue(gate.Input1) && GetWireValue(gate.Input2);
            else if (gate.Operator == "XOR")
                return GetWireValue(gate.Input1) ^ GetWireValue(gate.Input2);
            return GetWireValue(gate.Input1) || GetWireValue(gate.Input2);
        }

        output = new BitArray(Gates.Where(k => k.Key.StartsWith('z')).OrderBy(t => t.Key.ExtraxtInteger()).Select(t => GetWireValue(t.Key)).ToArray()).ToInt64().ToString();
        return -1;
    }

    public int Star2()
    {
        List<string> badGates = [];
        foreach (var gate in Gates)
        {
            if (gate.Key.StartsWith('z') && gate.Value.Operator != "XOR" && gate.Key != "z45")
                badGates.Add(gate.Key);

            else if (!gate.Key.StartsWith('z') && !(gate.Value.Input1[0] is 'x' or 'y') && !(gate.Value.Input1[0] is 'x' or 'y') && gate.Value.Operator == "XOR")
                badGates.Add(gate.Key);

            else if ((gate.Value.Input1[0] is 'x' or 'y') && (gate.Value.Input2[0] is 'x' or 'y') && !gate.Value.Input1.Contains("00") && !gate.Value.Input2.Contains("00"))
            {
                var nextGate = gate.Value.Operator == "XOR" ? "XOR" : "OR";
                if (!Gates.Values.Any(t => t != gate.Value &&
                    (t.Input1 == gate.Key || t.Input2 == gate.Key)
                        && t.Operator == nextGate))
                    badGates.Add(gate.Key);
            }
        }

        output = string.Join(',', badGates.OrderBy(t => t));
        return -1;
    }
}
