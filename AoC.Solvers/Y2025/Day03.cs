namespace AoC.Solvers.Y2025;

public class Day03(string input) : IDay
{
    public string Output => _output;
    private string _output = string.Empty;
    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        _output = SumJolts(2);
        return -1;
    }

    public int Star2()
    {
        _output = SumJolts(12);
        return -1;
    }

    private string SumJolts(int jolts) => Input.Select(k => 
        {
            var bank = k.Select(t => int.Parse(t.ToString())).ToList();
            long res = 0;
            for (int i = jolts; i > 0; i--)
            {
                var max = bank[..^(i - 1)].Max();
                bank = bank[(bank.IndexOf(max) + 1) ..];
                res = res * 10 + max;
            }
            return res;
        }).Sum().ToString();
}
