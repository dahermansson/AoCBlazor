using AoC.AoCUtils;

namespace AoC.Solvers.Y2023;

public class Day06 : IDay
{
    public Day06(string input)
    {
        var lines = InputParsers.GetInputLines(input);
        var times = lines[0].Replace("Time:", "").Trim().Split(' ').Where(t => t != "").Select(int.Parse).ToArray();
        var distances = lines[1].Replace("Distance:", "").Trim().Split(' ').Where(t => t != "").Select(int.Parse).ToArray();
        Input = times.Zip(distances, (time, dist) => new Race(time, dist)).ToArray();
    }
    public string Output => throw new NotImplementedException();
    private Race[] Input { get; set; }
    public int Star1() => Input.Select(t => t.Wins().Count()).Aggregate(1, (a, b) => a * b);
    public int Star2() => new Race(int.Parse($"{string.Concat(Input.Select(t => t.Time))}"),
        long.Parse($"{string.Concat(Input.Select(t => t.Distance))}")).Wins().Count();

    record Race(int Time, long Distance)
    {
        public IEnumerable<long> Wins()
        {
            for (long i = 0; i <= Time; i++)
                if (i * (Time - i) > Distance)
                    yield return i;
        }
    }
}
