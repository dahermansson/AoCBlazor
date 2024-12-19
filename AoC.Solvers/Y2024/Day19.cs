namespace AoC.Solvers.Y2024;

public class Day19(string input) : IDay
{
    public string Output => output;
    private string output = string.Empty;
    private readonly string[] Patterns = InputParsers.GetInputLines(input).First().Split(", ").OrderByDescending(t => t.Length).ToArray();
    private string[] Input { get; set; } = InputParsers.GetInputLines(input);

    public int Star1()
    {
        bool IsValid(string current, string goal)
        {
            if(current == goal)
                return true;
            if(current.Length > goal.Length || !goal.StartsWith(current))
                return false;

            var g = goal[current.Length..];    
            foreach (var next in Patterns.Where(t => g.StartsWith(t)))
                if(IsValid(current + next, goal))
                    return true;
            return false;
        }
        return Input.Skip(2).Count(t => IsValid("", t));
    }

    public int Star2()
    {
        var d = new Dictionary<string, long>();

        long ValidCount(string current)
        {   
            if(string.IsNullOrEmpty(current))
                return 1;
            
            long total = 0;
            foreach (var pattern in Patterns.Where(current.StartsWith))
            {
                var next = current[pattern.Length..];
                if(d.TryGetValue(next, out long known))
                    total += known;
                else
                {
                    known = ValidCount(next);
                    total += known;
                    d.Add(next, known);
                }
            }
            return total;
        }
        var t = Input.Skip(2).Sum(t => ValidCount(t));
        output = t.ToString();
        return -1;
    }
}
