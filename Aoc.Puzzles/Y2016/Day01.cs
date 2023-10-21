using AoC.Utils;

namespace Aoc.Puzzles.Y2016;

public class Day01 : IDay
{
    public string Output => throw new NotImplementedException();

    private string Input ="R1, L3, R5, R5, R5, L4, R5, R1, R2, L1, L1, R5, R1, L3, L5, L2, R4, L1, R4, R5, L3, R5, L1, R3, L5, R1, L2, R1, L5, L1, R1, R4, R1, L1, L3, R3, R5, L3, R4, L4, R5, L5, L1, L2, R4, R3, R3, L185, R3, R4, L5, L4, R48, R1, R2, L1, R1, L4, L4, R77, R5, L2, R192, R2, R5, L4, L5, L3, R2, L4, R1, L5, R5, R4, R1, R2, L3, R4, R4, L2, L4, L3, R5, R4, L2, L1, L3, R1, R5, R5, R2, L5, L2, L3, L4, R2, R1, L4, L1, R1, R5, R3, R3, R4, L1, L4, R1, L2, R3, L3, L2, L1, L2, L2, L1, L2, R3, R1, L4, R1, L1, L4, R1, L2, L5, R3, L5, L2, L2, L3, R1, L4, R1, R1, R2, L1, L4, L4, R2, R2, R2, R2, R5, R1, L1, L4, L5, R2, R4, L3, L5, R2, R3, L4, L1, R2, R3, R5, L2, L3, R3, R1, R3";

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
