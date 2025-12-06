namespace AoC.Solvers.Y2025;

public class Day02(string input) : IDay
{
    public string Output => _output;
    private string _output = string.Empty;

    private string[] Input { get; set; } = input.Split(",");

    private IEnumerable<IEnumerable<long>> ProductIds => Input.Select(t =>
    {
        var k = t.Split('-').Select(long.Parse).ToArray();
        return CreateRange(k[0], k[1] - k[0] + 1);
    });

    private static bool ValidStar1(long productId)
    {
        var productIdString = productId.ToString();
        var l = productIdString.Length / 2;
        return productIdString[..l] == productIdString[l..];
    }

    public int Star1()
    {
        _output = ProductIds.SelectMany(t => t).Where(ValidStar1).Sum().ToString();
        return -1;
    }

    private static bool ValidStar2(long productId)
    {
        var productIdString = productId.ToString();
        return Enumerable.Range(1, productIdString.Length/2).Any(c =>
        {
            var chunks = productIdString.Chunk(c).Select(p => string.Concat(p)).ToArray();
            return chunks.Skip(1).All(p => p == chunks[0]);
        });
    }

    public int Star2()
    {
        _output = ProductIds.SelectMany(t => t).Where(ValidStar2).Sum().ToString();
        return -1;
    }

    private static IEnumerable<long> CreateRange(long start, long count)
    {
        var limit = start + count;
        while (start < limit)
        {
            yield return start;
            start++;
        }
    }
}