namespace AoC.Solvers.Y2024;

public class Day11(string input) : IDay
{
    public string Output => output;
    private string output = string.Empty;
    private List<long> Input { get; set; } = input.Split(' ').Select(long.Parse).ToList();

    public int Star1()
    {
        List<long> numbers = [.. Input];
        for (int i = 0; i < 25; i++)
        {
            numbers = numbers.Aggregate(new List<long>(), (a, b) =>
            {
                if (b == 0)
                    a.Add(1);
                else if (b.ToString().Length % 2 == 0)
                {
                    var s = b.ToString();
                    var first = long.Parse(s[..(s.Length / 2)]);
                    var secound = long.Parse(s[(s.Length / 2)..]);
                    a.AddRange([first, secound]);
                }
                else
                    a.Add(b * 2024);
                return a;
            });
        }

        return numbers.Count;
    }

    public int Star2()
    {
        Dictionary<long, long> numbers = Input.ToDictionary(key => key, value => (long)1);
        for (int i = 0; i < 75; i++)
        {
            numbers = numbers.Aggregate(new Dictionary<long, long>(), (a, b) =>
            {
                if (b.Key == 0)
                    a[1] = a.TryGetValue(1, out long curentZeros) ? curentZeros + b.Value : b.Value;
                else if (b.Key.ToString().Length % 2 == 0)
                {
                    var s = b.Key.ToString();
                    var first = long.Parse(s[..(s.Length / 2)]);
                    var secound = long.Parse(s[(s.Length / 2)..]);
                    a[first] = a.TryGetValue(first, out long currentFirst) ? currentFirst + b.Value : b.Value;
                    a[secound] = a.TryGetValue(secound, out long currentSecound) ? currentSecound + b.Value : b.Value;
                }
                else
                    a[b.Key * 2024] = a.TryGetValue(b.Key * 2024, out long currentOther) ? currentOther + b.Value : b.Value;
                return a;
            });
        }
        output = numbers.Sum(t => t.Value).ToString();
        return -1;
    }
}
