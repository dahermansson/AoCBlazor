using NetTopologySuite.Geometries;

namespace AoC.Solvers.Y2024;

public class Day10: IDay
{
    public Day10(string input) => Input = InputParsers.GetInputLines(input);
    public string Output => throw new NotImplementedException();

    private string[] Input {get; set;}
    private record Pos(int X, int Y);

    public int Star1()
    {
        //Input = InputParsers.GetInputLines("""
        //89010123
        //78121874
        //87430965
        //96549874
        //45678903
        //32019012
        //01329801
        //10456732
        //""");

        int DFS(Pos start)
        {
            IEnumerable<Pos> dir = [new (0,1), new (1,0), new (0,-1), new (-1, 0)];
            var res = 0;
            var spots = new Stack<Pos>();
            HashSet<Pos> visited = [];
            spots.Push(start);
            
            while(spots.Count > 0)
            {
                var current = spots.Pop();
                
                if(Input[current.X][current.Y] == '9')
                {
                    visited.Add(current);
                    res++;
                    continue;
                }
                visited.Add(current);
                
                foreach(var next in dir.Select(d => {
                    var x = current.X + d.X;
                    var y = current.Y + d.Y;
                    if(x < Input.Length && x > -1 && y < Input[0].Length && y > -1)
                        return new Pos(x, y);
                    else
                        return null;
                    }).Where(t => t != null && Input[t.X][t.Y] - Input[current.X][current.Y] == 1 && !visited.Contains(new (t.X, t.Y))))
                    {
                        spots.Push(next);
                    }
            }
            return res;
        }

        return Input.SelectMany((row, x) => row.Select((c, y) => (x, y, c) )).Where(t => t.c == '0').Select(t => new Pos(t.x, t.y)).Sum(DFS);
    }



    public int Star2()
    {
        IEnumerable<Pos> dir = [new (0,1), new (1,0), new (0,-1), new (-1, 0)];
        int foundPaths = 0;
        bool DfsR(Pos current, HashSet<Pos> visited, List<List<Pos>> paths)
        {
            if(visited == null)
                visited=[];
            if(Input[current.X][current.Y] == '9')
                return true;
            
            visited.Add(current);
            foreach(var next in dir.Select(d => {
                    var x = current.X + d.X;
                    var y = current.Y + d.Y;
                    if(x < Input.Length && x > -1 && y < Input[0].Length && y > -1)
                        return new Pos(x, y);
                    else
                        return null;
                    }).Where(t => t != null && Input[t.X][t.Y] - Input[current.X][current.Y] == 1 && !visited.Contains(new (t.X, t.Y))))
                    {
                        if(DfsR(next, [..visited], paths))
                            paths.Add([..visited, next]);
                    }
            return false;
        }
        var paths = new List<List<Pos>>();
        Input.SelectMany((row, x) => row.Select((c, y) => (x, y, c) )).Where(t => t.c == '0').Select(t => new Pos(t.x, t.y)).ToList().ForEach(t => DfsR(t, [], paths));
        return paths.Count;
    }
}
