using AoC.Utils;
using System.IO;
namespace Aoc.Puzzles.Y2016;

public class Day01: IDay
{
    public Day01(string input)
    {
        Input = input;
    }
    public string Output => throw new NotImplementedException();

    public string Input {get; private set;}
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
        return 2;
    }
}
