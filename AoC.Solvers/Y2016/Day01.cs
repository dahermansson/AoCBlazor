using AoC.AoCUtils;
namespace AoC.Solvers.Y2016;

public class Day01(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string Input { get; set; } = input;
    public int Star1()
    {
        var turns = Input.Split(", ");
        int dir = 0;
        int ns=0, ew=0;

        foreach (var turn in turns)
        {
            if(turn[0]=='R')
                dir = (dir+1)%4;
            else{
                dir = (dir-1);
                if(dir <0)
                    dir= 3;
            }

            if(dir == 0)
                ns+=int.Parse(turn.Substring(1));
            else if(dir == 1)
                ew+=int.Parse(turn.Substring(1));
            else if(dir == 2)
                ns-=int.Parse(turn.Substring(1));
            else 
                ew-=int.Parse(turn.Substring(1));
        }


        return Math.Abs(ns) + Math.Abs(ew);
    }

    public int Star2()
    {
        var turns = Input.Split(", ");
        int dir = 0;
        var current = (0,0);
        HashSet<(int, int)> path = [current];
        foreach (var turn in turns)
        {
            if(turn[0]=='R')
                dir = (dir+1)%4;
            else{
                dir = (dir-1);
                if(dir <0)
                    dir= 3;
            }

            if(dir == 0)
            {
                var steps = int.Parse(turn.Substring(1));
                for(int i = 0;i<steps;i++)
                {
                    current.Item1++;
                    if(!path.Contains(current))
                        path.Add(current);
                    else
                        return Math.Abs(current.Item1) + Math.Abs(current.Item2);
                }
            }
            else if(dir == 1)
                {
                var steps = int.Parse(turn.Substring(1));
                for(int i = 0;i<steps;i++)
                {
                    current.Item2++;
                    if(!path.Contains(current))
                        path.Add(current);
                    else
                        return Math.Abs(current.Item1) + Math.Abs(current.Item2);
                }
            }
            else if(dir == 2)
            {
                var steps = int.Parse(turn.Substring(1));
                for(int i = 0;i<steps;i++)
                {
                    current.Item1--;
                    if(!path.Contains(current))
                        path.Add(current);
                    else
                        return Math.Abs(current.Item1) + Math.Abs(current.Item2);
                }
            }
            else 
            {
                var steps = int.Parse(turn.Substring(1));
                for(int i = 0;i<steps;i++)
                {
                    current.Item2--;
                    if(!path.Contains(current))
                        path.Add(current);
                    else
                        return Math.Abs(current.Item1) + Math.Abs(current.Item2);
                }
            }
        }
        return 0;
    }
}
