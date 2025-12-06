namespace AoC.Solvers.Y2025;

public class Day06(string input) : IDay
{
    public string Output => _output;
    private string _output = string.Empty;
    private string Input { get; set; } = input;

    public int Star1()
    {
        var worksheet = InputParsers.GetInputLines(Input)
            .Select(t => t.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();
        
        List<List<string>> colums = [ ..Enumerable.Range(0, worksheet[0].Length)
            .Select(col => Enumerable.Range(0, worksheet.Length)
                .Select(row => worksheet[row][col]).ToList()
        )];
        

        _output = colums.Sum(c => {
            var operand = c.Last();
            return operand switch{
                "+" => c.SkipLast(1).Select(long.Parse).Sum(),
                "*" => c.SkipLast(1).Select(long.Parse).Aggregate((long)1, (res, p) => res * p),
                _ => 0
            };
        }).ToString();

        return -1;
    }

    public int Star2()
    {
        var worksheet = InputParsers.GetInputLines(Input)
            .Select(t => t.ToCharArray()).ToArray();

        List<Problem> problems = [];
        List<string> numbers = [];
        for(var col = worksheet[0].Length-1; col > -1; col--)
        {
            numbers.Add(string.Concat(
                Enumerable.Range(0, worksheet.Length)
                    .Select(row => worksheet[row][col])));

            if(numbers.Last().Last() != ' ')
            {
                problems.Add(
                    new Problem([..numbers.SkipLast(1), string.Concat(numbers.Last().SkipLast(1))],
                    numbers.Last().Last().ToString()));
                numbers = [];
            }
        }

        _output = problems.Sum(c => c.Operator switch
        {
            "+" => c.AsLongs.Sum(),
            "*" => c.AsLongs.Aggregate((long)1, (res, p) => res * p),
            _ => 0
        }).ToString();

        return -1;
    }

    private record Problem(List<string> Numbers, string Operator)
    {
        public IEnumerable<long> AsLongs =>
            Numbers.Where(p =>
                p.Any(char.IsDigit))
                .Select(t =>
                    long.Parse(string.Concat(t.Where(char.IsDigit))));
    }
}
