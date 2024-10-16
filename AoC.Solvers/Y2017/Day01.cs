namespace AoC.Solvers.Y2017;

public class Day01: IDay
{
    public Day01(string input) => Input = input.Select(t => int.Parse(t.ToString())).ToArray();
    public string Output => throw new NotImplementedException();
    private int[] Input {get; set;}
    public int Star1() => Input.Select((value, index) => (value, index)).Aggregate(0, (a,b) => a += b.value == Input[(b.index+1) % Input.Length] ? b.value : 0);
    public int Star2() => Input.Select((value, index) => (value, index)).Aggregate(0, (a,b) => a += b.value == Input[(b.index+Input.Length/2) % Input.Length] ? b.value : 0);
}
