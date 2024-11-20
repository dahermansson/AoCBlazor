namespace AoC.Solvers.Y2017;

public class Day01(string input) : IDay
{
    public string Output => throw new NotImplementedException();
    private int[] Input { get; set; } = input.Select(t => int.Parse(t.ToString())).ToArray();
    public int Star1() => Input.Select((value, index) => (value, index)).Aggregate(0, (a,b) => a += b.value == Input[(b.index+1) % Input.Length] ? b.value : 0);
    public int Star2() => Input.Select((value, index) => (value, index)).Aggregate(0, (a,b) => a += b.value == Input[(b.index+Input.Length/2) % Input.Length] ? b.value : 0);
}
