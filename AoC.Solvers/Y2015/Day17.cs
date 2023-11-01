using AoC.Utils;

namespace AoC.Solvers.Y2015;

public class Day17: IDay
{
    public Day17(string input)
    {
        Input = InputParsers.GetInputLines(input).Select(t => int.Parse(t)).ToArray();
        Target = 150;
    }
    public Day17(string input, int target)
    {
        Input = InputParsers.GetInputLines(input).Select(t => int.Parse(t)).ToArray();
        Target = target;
    }
    private int Target { get; init; }
    public string Output => throw new NotImplementedException();

    private int[] Input {get; set;}

    public int Star1()
    {
        int count = 0;
        Sum(Input.ToList(), Target, new List<int>(), ref count);
        return count;
    }
    public int Star2()
    {
        int count = 0;
        int min = Input.Length;
        SumMin(Input.ToList(), Target, new List<int>(), ref count, ref min);
        return count;
    }

    private void Sum(List<int> containers, int target, List<int> used, ref int count)
    {
        var amount = used.Sum();
        if(amount == target)
        {
            count++;
            return;
        }
        if(amount > target)
            return;

        for (int i = 0; i < containers.Count; i++)
        {
            List<int> notUsed = new();
            int c = containers[i];
            for(int a = i+1;a<containers.Count;a++)
                notUsed.Add(containers[a]);    
            Sum([.. notUsed], target, [..used, c], ref count);
        }
    }

    private void SumMin(List<int> containers, int target, List<int> used, ref int count, ref int min)
    {
        var amount = used.Sum();
        if(amount == target)
        {
            if(used.Count() < min )
            {
                min = used.Count;
                count = 1;
            }
            else if(used.Count == min)
                count++;
            return;
        }
        if(amount > target || used.Count() >= min)
            return;

        for (int i = 0; i < containers.Count; i++)
        {
            List<int> notUsed = new();
            int c = containers[i];
            for(int a = i+1;a<containers.Count;a++)
                notUsed.Add(containers[a]);    
            SumMin([.. notUsed], target, [..used, c], ref count, ref min);
        }
    }
}
