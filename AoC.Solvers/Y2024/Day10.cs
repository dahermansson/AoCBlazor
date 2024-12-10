namespace AoC.Solvers.Y2024;

public class Day10(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    private record Pos(int X, int Y);
    private IEnumerable<Pos> Dir { get; init; } = [new(0, 1), new(1, 0), new(0, -1), new(-1, 0)];

    private List<Pos> Trailheads { get; init; } = InputParsers.GetInputLines(input)
        .SelectMany((row, x) => row.Select((c, y) => (x, y, c))).Where(t => t.c == '0').Select(t => new Pos(t.x, t.y)).ToList();

    public int Star1()
    {
        int GetTrailheadScore(Pos trailhead)
        {
            var score = 0;
            HashSet<Pos> visited = [];
            var positions = new Stack<Pos>();
            positions.Push(trailhead);

            while (positions.Count != 0)
            {
                var current = positions.Pop();
                visited.Add(current);

                if (Input[current.X][current.Y] == '9')
                    score++;

                GetNeighbor(current)
                    .Where(neighbor => Input[neighbor.X][neighbor.Y] - Input[current.X][current.Y] == 1
                        && !visited.Contains(new(neighbor.X, neighbor.Y)))
                        .ToList().ForEach(positions.Push);
            }
            return score;
        }

        return Trailheads.Sum(GetTrailheadScore);
    }

    private IEnumerable<Pos> GetNeighbor(Pos current) =>
        Dir.Select(d => new Pos(current.X + d.X, current.Y + d.Y)).Where(n => n.X < Input.Length && n.X > -1 && n.Y < Input[0].Length && n.Y > -1);

    public int Star2()
    {
        void FindDistinctHikingTrails(Pos current, HashSet<Pos> visited, List<List<Pos>> hikingTrails)
        {
            visited.Add(current);
            if (Input[current.X][current.Y] == '9')
                hikingTrails.Add([.. visited]);

            GetNeighbor(current).Where(t => t != null && Input[t.X][t.Y] - Input[current.X][current.Y] == 1 && !visited.Contains(new(t.X, t.Y))).ToList().ForEach(next =>
                FindDistinctHikingTrails(next, [.. visited], hikingTrails));
        }

        var hikingTrails = new List<List<Pos>>();
        Trailheads.ForEach(t => FindDistinctHikingTrails(t, [], hikingTrails));
        return hikingTrails.Count;
    }
}