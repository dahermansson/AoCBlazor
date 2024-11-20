namespace AoC.Solvers.Y2023;

public class Day04(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private Card[] Input { get; set; } = InputParsers.GetInputLines(input).Select(i => new Card(i)).ToArray();

    public int Star1() => Input.Sum(t => (int)Math.Pow(2, t.Points - 1));

    public int Star2()
    {
        var cards = Enumerable.Range(0, Input.Length).Select(t => 1).ToArray();
        foreach (var card in Input.Select((c, i) => (card: c, id: i)))
            for (int i = 1; i <= card.card.Points; i++)
                if (card.id + i < Input.Length)
                    cards[card.id + i] += 1 * cards[card.id];
        return cards.Sum();
    }

    record Card
    {
        public Card(string s)
        {
            s = s.Replace("   ", " ");
            s = s.Replace("  ", " ");
            var splits = s.Split("|");
            Winning = splits[0].Substring(splits[0].IndexOf(':') + 1).Trim().Split(" ").Select(int.Parse).ToList();
            You = splits[1].Trim().Split(" ").Select(t => int.Parse(t.Trim())).ToList();
        }
        public List<int> Winning { get; set; }
        public List<int> You { get; set; }
        public int Points => Winning.Intersect(You).Count();
    }
}
