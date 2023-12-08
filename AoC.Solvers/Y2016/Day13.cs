using System.Collections;
using AoC.AoCUtils;

namespace AoC.Solvers.Y2016;

public class Day13: IDay
{
    public Day13(string input)
    {
        Input = input;
        Goal = new Pos(int.Parse(Input.Split(" ")[1]),int.Parse(Input.Split(" ")[2]));
        OfficeFavNumber = int.Parse(Input.Split(" ")[0]); 
    }
    public string Output => throw new NotImplementedException();
    private int OfficeFavNumber { get; set; }
    private string Input {get; set;}
    private Pos Goal { get; set; }

    public int Star1()
    {
        var start = new Pos(1,1);
        List<(int dist, Pos pos)> visited = [];
        List<(int dist, Pos pos)> toVisit = [(0, start)];
        Dictionary<Pos, Pos?> parents = new()
        {
            [start] = null
        };
        
        while(toVisit.Count > 0)
        {
            var current = toVisit.OrderBy(t => t.dist).First();
            toVisit.Remove(current);

            var nexts = GetNexts(current.pos).ToArray();
            foreach (var next in nexts)
            {
                if(next == Goal)
                {
                    int steps = 0;
                    var prev = current.pos;
                    while(prev != null)
                    {
                        steps++;
                        prev = parents[prev];
                    }
                    return steps;
                }
                else
                {
                    var fromStart = current.dist + 1;
                    var toGoal = AoCUtils.Utils.ManhattanDistance(next.X, next.Y, Goal.X, Goal.Y);
                    var dist = fromStart + toGoal;
                    if(toVisit.Any(t => t.pos == next && t.dist < dist))
                        continue;
                    if(visited.Any(t => t.pos == next && t.dist < dist))
                        continue;
                    toVisit.Add((dist, next));
                    parents[next] = current.pos;
                }
            }
            visited.Add(current);
        }
        
        return 0;
    }

    public int Star2()
    {
        var visited = new HashSet<Pos>();
        var queue = new Queue<(int steps, Pos pos)>();
        var start = new Pos(1,1);
        queue.Enqueue((0, start));
        visited.Add(start);
        while (queue.Count != 0)
        {
            var current = queue.Dequeue();
            
            if(current.steps == 50)
                break;
            var nexts = GetNexts(current.pos);
            foreach (var next in nexts)
            {
                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    queue.Enqueue((current.steps + 1, next));
                }
            }
        }
        return visited.Count;
    }
    private IEnumerable<Pos> GetNexts(Pos pos)
    {
        if(IsOpenSpace(pos.X + 1, pos.Y)) yield return new Pos(pos.X + 1, pos.Y); 
        if(IsOpenSpace(pos.X - 1, pos.Y)) yield return new Pos(pos.X - 1, pos.Y); 
        if(IsOpenSpace(pos.X, pos.Y + 1)) yield return new Pos(pos.X, pos.Y + 1); 
        if(IsOpenSpace(pos.X, pos.Y - 1)) yield return new Pos(pos.X, pos.Y - 1);  
    }

    private bool IsOpenSpace(int x, int y)
    {
        if(x < 0 || y < 0)
            return false;
        var a = x*x + 3*x + 2*x*y + y + y*y;
        a+=OfficeFavNumber;
        return new BitArray(new int[]{a}).Cast<bool>().Count(t => t) % 2 == 0;
    }
    record Pos(int X, int Y);
 }
