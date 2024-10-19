namespace AoC.Solvers.Y2017;

public class Day05(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private int[] Input { get; set; } = InputParsers.GetInputLines<int>(input);

    public int Star1()
    {
        var jumps = Input.Select((v, i) => (Key: i, Value: v)).ToDictionary(a => a.Key, a => a.Value);
        int index = 0;
        int turns = 0;
        do
        {
            var jump = jumps[index]++;
            index += jump;
            turns++;
        } while (jumps.ContainsKey(index));
        return turns;
    }

    public int Star2()
    {
        var jumps = Input.Select((v, i) => (Key: i, Value: v)).ToDictionary(a => a.Key, a => a.Value);
        int index = 0;
        int turns = 0;
        do
        {
            var jump = jumps[index];
            jumps[index] = jump >= 3 ? jumps[index] - 1 : jumps[index] + 1;
            index += jump;
            turns++;
        } while (jumps.ContainsKey(index));
        return turns;
    }
}
