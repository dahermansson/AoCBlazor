namespace AoC.Solvers.Y2025;

public class Day05(string input) : IDay
{
    public string Output => _output;
    private string _output = string.Empty;

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        var freshIngredients = Input.TakeWhile(t => t != string.Empty)
            .Select(i => new Range(
                long.Parse(i.Split('-')[0]),
                long.Parse(i.Split('-')[1])));

        var ingredientIds = Input.SkipWhile(t => t.Contains('-') || t == string.Empty)
            .Select(long.Parse);

        return ingredientIds.Count(ingredientId =>
            freshIngredients.Any(fresh => fresh.InRange(ingredientId)));
    }

    public int Star2()
    {
        var freshIngredients = Input.TakeWhile(t => t != string.Empty)
            .Select(i => new Range(
                long.Parse(i.Split('-')[0]),
                long.Parse(i.Split('-')[1]))).OrderBy(t => t.From).ToList();

        List<Range> ranges = [];
        HashSet<Range> used = [];
        while (freshIngredients.Any(i => !used.Contains(i)))
        {
            var range = freshIngredients.Where(t => !used.Contains(t)).First();
            used.Add(range);

            ranges.Add(freshIngredients.Where(t => !used.Contains(t)).Aggregate(range, (current, next) =>
            {
                if (current.Overlapp(next))
                {
                    used.Add(next);
                    return current with { To = next.To };
                }

                if (current.Includes(next))
                    used.Add(next);

                return current;
            }));
        }
        _output = ranges.Sum(t => t.IngredientIds).ToString();
        return -1;
    }

    private record Range(long From, long To)
    {
        public bool InRange(long IngredientID) => IngredientID >= From && IngredientID <= To;
        public bool Overlapp(Range range) => range.From >= From && range.From <= To && range.To >= To;
        public bool Includes(Range range) => range.From >= From && range.To <= To;
        public long IngredientIds => To - From + 1;
    }
}
