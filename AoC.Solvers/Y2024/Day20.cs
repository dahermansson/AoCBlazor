namespace AoC.Solvers.Y2024;

public class Day20(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    private Dictionary<Pos, char> Maze = InputParsers.GetInputLines(input).SelectMany((t, x) => t.Select((r, y) => (pos: new Pos(x, y), value: r))).ToDictionary(key => key.pos, v => v.value);

    private record Pos(int X, int Y);
    private readonly List<Pos> Dirs = [new Pos(-1, 0), new Pos(0, 1), new Pos(1, 0), new Pos(0, -1)];
    public int Star1()
    {
        var start = Maze.Single(t => t.Value == 'S').Key;
        var end = Maze.Single(t => t.Value == 'E').Key;

        var noCheatPath = GetPath(start, end);
        List<int> foundCheat = [];
        foreach (var cheatStart in noCheatPath)
        {
            foundCheat.AddRange(GetShortcuts(cheatStart.Key, 2, noCheatPath).Select(t => t.Value));
        }

        return foundCheat.Count(t => t >= 100);
    }

    public int Star2()
    {
        var start = Maze.Single(t => t.Value == 'S').Key;
        var end = Maze.Single(t => t.Value == 'E').Key;

        var noCheatPath = GetPath(start, end);
        List<int> foundCheat = [];
        
        foreach (var cheatStart in noCheatPath)
        {
            if(cheatStart.Value + Utils.ManhattanDistance(cheatStart.Key.X, end.X, cheatStart.Key.Y, end.Y) < noCheatPath[end] - 100)
                foundCheat.AddRange(GetShortcuts(cheatStart.Key, 20, noCheatPath).Select(t => t.Value));
        }

        return foundCheat.Count(t => t >= 100);
    }

    Dictionary<Pos, int> GetPath(Pos start, Pos end)
    {
        Dictionary<Pos, int> seen = [];
        var queue = new Stack<(Pos Pos, int Steps)>();
        queue.Push((start, 0));
        while (queue.TryPop(out var current))
        {
            seen.Add(current.Pos, current.Steps);
            if (current.Pos == end)
            {
                return seen;
            }
            var neighbors = Dirs.Select(d => new Pos(current.Pos.X + d.X, current.Pos.Y + d.Y)).Where(t => Maze.TryGetValue(t, out char c) && c != '#' && !seen.ContainsKey(t));
            foreach (var n in neighbors)
            {
                if (!queue.Any(t => t.Pos == n))
                    queue.Push((n, current.Steps + 1));
            }
        }
        return [];
    }

    Dictionary<Pos, int> GetShortcuts(Pos shortCutStart, int maxCheatSteps, Dictionary<Pos, int> noCheatPath)
    {
        Dictionary<Pos, int> shortcuts = [];
        var start = noCheatPath.Single(t => t.Key == shortCutStart);
        HashSet<Pos> seen = [];

        var queue = new Queue<(Pos Pos, int CheatStepsTaken)>();
        queue.Enqueue((start.Key, 0));

        while (queue.TryDequeue(out var current))
        {
            seen.Add(current.Pos);

            if (noCheatPath.TryGetValue(current.Pos, out int noCheatSteps) && start.Value + current.CheatStepsTaken < noCheatSteps)
            {
                int savedSteps = noCheatSteps - (start.Value + current.CheatStepsTaken);
                shortcuts[current.Pos] = savedSteps;
            }
            if (current.CheatStepsTaken >= maxCheatSteps)
            {
                continue;
            }

            var neighbors = new List<Pos>();
            
            if (current.CheatStepsTaken < maxCheatSteps - 1)
                neighbors = Dirs.Select(d => new Pos(current.Pos.X + d.X, current.Pos.Y + d.Y)).Where(t => Maze.TryGetValue(t, out char c) && c == '#' && !seen.Contains(t)).ToList();

            neighbors.AddRange(Dirs.Select(d => new Pos(current.Pos.X + d.X, current.Pos.Y + d.Y)).Where(t => noCheatPath.ContainsKey(t) && !seen.Contains(t)));

            foreach (var n in neighbors)
            {
                if (!queue.Any(t => t.Pos == n))
                    queue.Enqueue((n, current.CheatStepsTaken + 1));
            }
        }
        return shortcuts;
    }
}
