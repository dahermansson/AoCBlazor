namespace AoC.Solvers.Y2017;

public class Day06(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string Input { get; set; } = input;

    public int Star1()
    {
        var memoryState = Input.Split('\t').Select(int.Parse).ToList();

        HashSet<string> bankConfigurations = [];
        int redistributions = 0;

        do
        {
            bankConfigurations.Add(string.Join(",", memoryState));
            memoryState = Reallocate(memoryState);
            redistributions++;
        } while (!bankConfigurations.Contains(string.Join(",", memoryState)));


        return redistributions;
    }

    public int Star2()
    {
        List<int> memoryState = Input.Split('\t').Select(int.Parse).ToList();
        HashSet<string> memoryStates = [];
        var cycleSize = 0;
        do
        {
            memoryStates.Add(string.Join(",", memoryState));
            memoryState = Reallocate(memoryState);
        } while (!memoryStates.Contains(string.Join(",", memoryState)));

        var cycleState = string.Join(",", memoryState);

        do
        {
            memoryState = Reallocate(memoryState);
            cycleSize++;
        } while (string.Join(",", memoryState) != cycleState);


        return cycleSize;
    }

    private static List<int> Reallocate(List<int> memoryBanks)
    {
        var value = memoryBanks.Max();
        var index = memoryBanks.IndexOf(value);

        memoryBanks[index] = 0;
        for (int i = value; i > 0; i--)
            memoryBanks[++index % memoryBanks.Count]++;

        return memoryBanks;
    }
}
