using AoC.AoCUtils;

namespace AoC.Solvers.Y2023;

public class Day02 : IDay
{
    public Day02(string input) => Input = InputParsers.GetInputLines(input);
    public string Output => throw new NotImplementedException();
    private string[] Input { get; set; }
    private Dictionary<string, int> Bag { get; } = new() {
        {"red", 12},
        {"green", 13},
        {"blue", 14},
    };

    public int Star1() => Input.Select(l => new Game(l)).Sum(g =>
        Bag.Any(b => g.Draws.Any(d => d.Color == b.Key && d.Count > b.Value))
            ? 0 : g.Id);

    public int Star2() => Input.Select(l => new Game(l)).Sum(g => g.Draws.GroupBy(t => t.Color)
        .Select(g => g.MaxBy(c => c.Count))
        .Aggregate(1, (a, b) => a * b.Count)
        );

    record Game
    {
        public int Id { get; set; }
        public List<(string Color, int Count)> Draws { get; set; } = [];
        public Game(string s)
        {
            var splits = s.Split(' ');
            for (int i = 2; i < splits.Length - 1; i += 2)
                Draws.Add((splits[i + 1].TrimEnd(';', ','), int.Parse(splits[i])));
            Id = int.Parse(splits[1].Trim(':'));
        }
    }
}
