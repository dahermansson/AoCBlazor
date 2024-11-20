namespace AoC.Solvers.Y2023;

public class Day09(string input) : IDay
{
    public string Output => throw new NotImplementedException();
    private List<int[]> Input { get; set; } = InputParsers.GetInputLines(input)
        .Select(t => t.Split(" ")
        .Select(i => int.Parse(i, System.Globalization.NumberStyles.AllowLeadingSign)).ToArray()).ToList();
    public int Star1() => Input.Sum(t => Extrapolate(Predict([..t]).Select(t => t.Last()).ToArray()).Last());
    public int Star2() => Input.Sum(t => ExtrapolateBack(Predict([..t]).Select(t => t.First()).ToArray()).Last());

    private IEnumerable<int[]> Predict(int[] dataset)
    {
        while (dataset.Any(t => t != 0))
        {
            yield return dataset;
            var next = new int[dataset.Length - 1];
            for (int i = 0; i < dataset.Length - 1; i++)
                next[i] = dataset[i + 1] - dataset[i];
            dataset = next;
        }
        yield return dataset;
    }

    private IEnumerable<int> Extrapolate(int[] values)
    {
        var value = 0;
        for (int i = 0; i < values.Length; i++)
            yield return value += values[i];
    }

    private IEnumerable<int> ExtrapolateBack(int[] values)
    {
        var value = 0;
        for (int i = values.Length - 1; i > -1; i--)
            yield return value = values[i] - value;
    }
}
