namespace AoC.Solvers.Y2021;

public class Day7(string input) : IDay
{
    public string Output => throw new NotImplementedException();
    private readonly int[] Input = input.Split(',').Select(int.Parse).ToArray();
    public int Star1()
    {
        int[] posIndex = new int[Input.Max() + 1];
        for (int position = 0; position < posIndex.Length; position++)
        {
            var totalFuel = 0;
            foreach (var crab in Input)
                totalFuel += Math.Abs(crab - position);
            posIndex[position] = totalFuel;
        }
        return posIndex.Min();
    }

    public int Star2()
    {
        int[] posIndex = new int[Input.Max() + 1];
        for (int position = 0; position < posIndex.Length; position++)
        {
            var totalFuel = 0;
            foreach (var crab in Input)
                totalFuel += Enumerable.Range(1, Math.Abs(crab - position)).ToArray().Sum();
            posIndex[position] = totalFuel;
        }
        return posIndex.Min();
    }
}