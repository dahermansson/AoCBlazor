namespace AoC.Solvers.Y2017;

public class Day03: IDay
{
    public Day03(string input) => Input = 9;// int.Parse(input);
    public string Output => throw new NotImplementedException();

    private int Input {get; set;}

    public int Star1()
    {
        Dictionary<int, MazePosition> maze = [
            
        ];
        int i = 1;
        maze.Add(i++, new (0,0));

        int x = 1, y = 0;
        int size = 1+2;
        while(i<=Input)
        {
            for(int dir = 1; dir<5;dir++)
            {
                for(int k = 1;k<size;k++)
                {
                    maze.Add(i++, new(x, y));
                    if(dir == 1)
                        y++;
                    if(dir == 2)
                        x--;
                    if(dir == 3)
                        y--;
                    if(dir == 4)
                        x++;
                }
            }
            x++;
        }

        return Input;
    }

    public int Star2()
    {
        return 0;
    }

    private record MazePosition(int X, int Y);
}
