namespace AoC.Solvers.Y2025;

public class Day02(string input) : IDay
{
    public string Output => _output;
    private string _output = string.Empty;

    private string[] Input { get; set; } = input.Split(",");

    public int Star1()
    {
        var invalid = Input.Select(t => {
            var k = t.Split('-').Select(long.Parse).ToArray();
            return CreateRange(k[0], k[1]-k[0] + 1);
        }).SelectMany(t => t).Where(i => {
            var t = i.ToString();
            var l = t.Length/2;
            return t[..l] == t[l..];
        }).ToList();

        _output = invalid.Sum().ToString();
        return -1;
    }

    public int Star2()
    {        
        var invalid = Input.Select(t => {
                var k = t.Split('-').Select(long.Parse).ToArray();
                return CreateRange(k[0], k[1]-k[0] + 1);
            }).SelectMany(t => t).Where(i => {
                var t = i.ToString();
                var l = t.Length/2;
            
                return Enumerable.Range(1, l).Any(c => 
                {
                    var chunks = t.Chunk(c).Select(p => string.Concat(p)).ToArray();
                    return chunks.Skip(1).All(p => p == chunks[0]);
                });
            }).ToList();

        _output = invalid.Sum().ToString();
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
