using AoC.Utils;

namespace AoC.Solvers.Y2015;

public class Day16: IDay
{
    public Day16(string input)
    {
        Input = InputParsers.GetInputLines(input);
    }
    public string Output => throw new NotImplementedException();

    private string[] Input {get; set;}

    public int Star1()
    {
        var sue = new Aunt("Sue 0: children: 3, cats: 7, samoyeds: 2, pomeranians: 3, akitas: 0, vizslas: 0, goldfish: 5, trees: 3, cars: 2, perfumes: 1");
        var aunts = Input.Select(t => new Aunt(t)).ToList();
        return aunts.Select(t => (matches: sue.ExactMatches(t), t.AuntNumber)).OrderBy(t => t.matches).Last().AuntNumber;
    }

    public int Star2()
    {
        var sue = new Aunt("Sue 0: children: 3, cats: 7, samoyeds: 2, pomeranians: 3, akitas: 0, vizslas: 0, goldfish: 5, trees: 3, cars: 2, perfumes: 1");
        var aunts = Input.Select(t => new Aunt(t)).ToList();
        var matches = aunts.Select(t => (matches: sue.RangeMatches(t), t.AuntNumber));
        return matches.OrderBy(t => t.matches).Last().AuntNumber;
    }

    private class Aunt
    {
        Dictionary<string, int> Things = new();
        public int AuntNumber { get; set; }
        public Aunt(string str)
        {
            var s = str.Split(" ");
            AuntNumber = int.Parse(s[1].Trim(':'));
            for (int i = 2; i < s.Length-1; i+=2)
                Things.Add(s[i].Trim(':'), int.Parse(s[i+1].Trim('.', ',')));
        }
        public int ExactMatches(Aunt aunt) => Things.Where(t => aunt.Things.TryGetValue(t.Key, out int value) && value == Things[t.Key]).Select(t => 1).Sum();   

        public int RangeMatches(Aunt aunt)
        {
            int matches = 0;
            foreach (var thing in Things)
                if((thing.Key == "cats" || thing.Key == "trees") && aunt.Things.TryGetValue(thing.Key, out int value) && value > Things[thing.Key])
                    matches++;
                else if((thing.Key == "pomeranians" || thing.Key == "goldfish") && aunt.Things.TryGetValue(thing.Key, out value) && value < Things[thing.Key])
                    matches++;
                else if(aunt.Things.TryGetValue(thing.Key, out value) && value == Things[thing.Key])
                    matches++;
            return matches;
        }
    }
}
