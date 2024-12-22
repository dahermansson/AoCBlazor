namespace AoC.Solvers.Y2024;

public class Day22(string input) : IDay
{
    public string Output => output;
    private string output = string.Empty;

    private long[] Input { get; set; } = InputParsers.GetInputLines(input).Select(long.Parse).ToArray();

    private static long Mix(long secret, long mix) => secret ^ mix;
    private static long Prun(long secret) => secret % 16777216;
    private static long Op1(long secret) => Prun(Mix(secret, secret * 64));
    private static long Op2(long secret) => Prun(Mix(secret, secret / 32));
    private static long Op3(long secret) => Prun(Mix(secret, secret * 2048));
    
    private static long GetSecretNumber(long secret)
    {
        secret = Op1(secret);
        secret = Op2(secret);
        return Op3(secret);
    }

    private static IEnumerable<(int Bananas, int Diff)> GetPrice(long secretNumber)
    {
        yield return ((int)(secretNumber % 10), 0);
        var prevSecretNumber = secretNumber;
        for (var i = 1; i < 2000; i++) //First number is yield before the loop
        {
            secretNumber = GetSecretNumber(secretNumber);

            int bananas = (int)(secretNumber % 10);
            int prevBananas = (int)(prevSecretNumber % 10);
            yield return (bananas, bananas - prevBananas);
            prevSecretNumber = secretNumber;
        }
    }

    public int Star1()
    {
        output = Input.Sum(initialSecret => Enumerable.Range(0, 2000).Aggregate(initialSecret, (secretNumber, _) => GetSecretNumber(secretNumber))).ToString();
        return -1;
    }

    public int Star2()
    {
        Dictionary<(int a, int b, int c, int d), int> sequences = [];
        void PriceOnChanges((int Bananas, int Diff)[] prices)
        {
            HashSet<(int a, int b, int c, int d)> seen = [];
            foreach(var t in prices.Select((Price, Index) => (Price, Index)).Skip(4))
            {
                var sequence = (prices[t.Index - 3].Diff, prices[t.Index - 2].Diff, prices[t.Index - 1].Diff, prices[t.Index].Diff);
                if (seen.Contains(sequence))
                    continue;

                _ = sequences.TryGetValue(sequence, out var value);
                sequences[sequence] = value + t.Price.Bananas;
                seen.Add(sequence);
            }
        }

        Input.ToList().ForEach(t => PriceOnChanges(GetPrice(t).ToArray()));

        return sequences.Max(t => t.Value);
    }
}
