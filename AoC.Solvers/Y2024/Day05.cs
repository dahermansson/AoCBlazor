namespace AoC.Solvers.Y2024;

public class Day05(string input) : IDay
{
    public string Output => throw new NotImplementedException();
    private List<List<int>> Updates { get; init; } = InputParsers.GetInputLines(input).Where(t => t.Contains(',')).Select(t => t.Split(',').Select(int.Parse).ToList()).ToList();
    private List<(int value, int before)> PageOrderingRules { get; init; } = InputParsers.GetInputLines(input).Where(t =>
        t.Contains('|'))
            .Select(t => 
                { 
                    var ints = t.ExtractIntegers();
                    return (value: ints.First(), before: ints.Last());
                }).ToList();

    private bool IsInRightOrder(List<int> update) => !update.Any(page => PageOrderingRules.Any(rule =>
                    rule.value == page
                    && update.Contains(rule.before)
                    && update.IndexOf(rule.before) < update.IndexOf(page)));
    public int Star1() => Updates.Where(IsInRightOrder).Sum(t => t.Skip(t.Count / 2).First());

    public int Star2()
    {
        List<int> GetCorrectedUpdate(List<int> update)
        {
            for (int i = 0; i < update.Count; i++)
            {
                foreach (var (value, before) in PageOrderingRules.Where(t =>
                        t.value == update[i]
                        && update.Contains(t.before)
                        && update.IndexOf(t.before) < i))
                {
                    update.RemoveAt(i);
                    update.Insert(update.IndexOf(before), value);
                    return [.. update];
                }
            }
            return [.. update];
        }

        return Updates.Where(t => !IsInRightOrder(t)).Sum(update =>
        {
            var tempUpdate = update;
            do
            {
                tempUpdate = GetCorrectedUpdate(tempUpdate);
            } while (!IsInRightOrder(tempUpdate));
            return tempUpdate.Skip(tempUpdate.Count / 2).First();
        });
    }
}
