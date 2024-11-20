namespace AoC.Solvers.Y2016;

public class Day02(string input) : IDay
{
    private string[] Input { get; set; } = InputParsers.GetInputLines(input);
    public string Output => _output;

    private int[][] Keypad = [
        [1,2,3],
        [4,5,6],
        [7,8,9]
    ];

    private string[][] KeypadV2 = [
        ["","","1","",""],
        ["","2","3","4",""],
        ["5","6","7","8","9"],
        ["","A","B","C",""],
        ["","","D","",""],
        
    ];

    public int Star1()
    {
        (int x, int y) button = (1,1);
        string res = "";
        foreach(var l in Input)
        {
            foreach(char c in l)
            {
                if(c == 'U')
                    if(button.x > 0)
                        button.x--;
                if(c == 'D')
                    if(button.x < 2)
                        button.x++;
                if(c == 'L')
                    if(button.y > 0)
                        button.y--;
                if(c == 'R')
                    if(button.y < 2)
                        button.y++;
            }
            res+=Keypad[button.x][button.y];
        }
        _output=res;
        return -1;
    }
    private string _output = "";
    public int Star2()
    {
        (int x, int y) button = (1,1);
        string res = "";
        foreach(var l in Input)
        {
            foreach(char c in l)
            {
                if(c == 'U')
                    if(button.x > 0 && KeypadV2[button.x-1][button.y] != "" )
                        button.x--;
                if(c == 'D')
                    if(button.x < 4 &&KeypadV2[button.x+1][button.y] != "" )
                        button.x++;
                if(c == 'L')
                    if(button.y > 0 &&KeypadV2[button.x][button.y-1] != "" )
                        button.y--;
                if(c == 'R')
                    if(button.y < 4 &&KeypadV2[button.x][button.y+1] != "" )
                        button.y++;
            }
            res+=KeypadV2[button.x][button.y];
        }
        _output = res;
        return -1;
    }
}
