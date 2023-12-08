using AoC.AoCUtils;

namespace AoC.Solvers.Y2023;

public class Day03 : IDay
{
    public Day03(string input) => Input = InputParsers.GetInputLines(input);
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; }

    public int Star1() => Input.Select((row, i) =>
    {
        int[] signs = [..GetSignIndex(row),
            ..(i >= 1 ? GetSignIndex(Input[i - 1]) : []),
            ..(i < Input.Length - 1 ? GetSignIndex(Input[i + 1]) : [])];
        return GetNumbers(row).Where(t => NextToSign(t.pos, signs)).Sum(t => t.number);
    }).Sum();

    public int Star2() => Input.Select((row, i) =>
    {
        List<(int number, List<int> pos)> numbers = [..GetNumbers(row),
            ..(i >= 1 ? GetNumbers(Input[i - 1]) : []),
            ..(i < Input.Length - 1 ? GetNumbers(Input[i + 1]) : [])];
        return GetGearIndex(row).Select(g => GetGears(g, numbers)).Where(g => g.Count == 2).Sum(p => p[0].number * p[1].number);
    }).Sum();

    private bool NextToSign(List<int> numbers, IEnumerable<int> signs) =>
        signs.Any(r => r >= numbers.Min() - 1 && r <= numbers.Max() + 1);
    private List<(int number, List<int> pos)> GetGears(int gear, List<(int number, List<int> pos)> numbers) =>
        numbers.Where(t => NextToSign(t.pos, [gear])).ToList();

    private List<(int number, List<int> pos)> GetNumbers(string s)
    {
        var numericIndex = new List<(int number, List<int> pos)>();
        for (int i = 0; i < s.Length; i++)
        {
            if (char.IsDigit(s[i]))
            {
                var n = new List<int>();
                while (i < s.Length && char.IsDigit(s[i]))
                    n.Add(i++);
                numericIndex.Add((int.Parse(s.Substring(n.Min(), n.Count)), n));
            }
        }
        return numericIndex;
    }
    private int[] GetSignIndex(string s) => s.Select((t, i) => (t, i))
        .Where(t => !char.IsDigit(t.t) && t.t != '.')
        .Select(t => t.i).ToArray();

    private IEnumerable<int> GetGearIndex(string s) => s.Select((t, i) => (t, i))
        .Where(t => t.t == '*')
        .Select(t => t.i);
}
