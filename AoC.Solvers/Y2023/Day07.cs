using AoC.Utils;

namespace AoC.Solvers.Y2023;

public class Day07 : IDay
{
    public Day07(string input) => Input = InputParsers.GetInputLines(input);
    public string Output => throw new NotImplementedException();
    private string[] Input { get; set; }
    public static Dictionary<char, int> Cards = new(){
        {'A',14},{'K',13},{'Q',12},{'J',11},{'T',10},{'9',9},{'8',8},{'7',7},{'6',6},{'5',5},{'4',4},{'3',3},{'2',2}
     };

    public int Star1() => Input.Select(t => new Hand(t)).OrderBy(t => t).Select((t, i) => t.Bid * (i + 1)).Sum();

    public int Star2()
    {
        Cards['J'] = 1;
        return Input.Select(t => new Hand(t)).OrderBy(t => t).Select((t, i) => t.Bid * (i + 1)).Sum();
    }
    record Hand : IComparable<Hand>
    {
        public int[] Card;
        public int Bid { get; set; }
        public Hand(string card)
        {
            Card = card.Split(" ")[0].Select(c => Cards[c]).ToArray();
            Bid = int.Parse(card.Split(" ")[1]);
        }

        public int CompareTo(Hand? other)
        {
            if (other == null) return 1;

            if (GetScore().Hand > other.GetScore().Hand)
                return 1;
            if (GetScore().Hand < other.GetScore().Hand)
                return -1;

            foreach (var v in Card.Zip(other.Card, (int t, int o) => (t, o)).Where(c => c.t != c.o))
                return v.t > v.o ? 1 : -1;

            return 0;
        }

        public (int Hand, int[] Cards) GetScore()
        {
            var card = Card;
            if (card.Contains(1) && card.Any(t => t != 1))
            {
                var groupOf = card.Where(t => t != 1).GroupBy(c => c).OrderByDescending(t => t.Count()).First();
                card = Card.Select(t => t == 1 ? groupOf.First() : t).ToArray();
            }
            var g = card.GroupBy(c => c);
            if (g.Count() == 1)
                return (7, Card);
            if (g.Count() == 2)
            {
                if (g.Any(t => t.Count() == 4))
                    return (6, Card);
                return (5, Card);
            }
            if (g.Count() == 3)
            {
                if (g.Any(t => t.Count() == 3))
                    return (4, Card);
                return (3, Card);
            }
            if (g.Count() == 4)
            {
                return (2, Card);
            }
            return (1, Card);
        }
    }
}