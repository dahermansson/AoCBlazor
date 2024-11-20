namespace AoC.Solvers.Y2015;

public class Day01(string input) : IDay
{
    public string Output => throw new NotImplementedException();

    private string Input { get; set; } = input;

    public int Star1()
    {
        return Input.Count(t => t == '(') - Input.Count(t => t == ')');
    }

    public int Star2()
    {
        int floor = 0;
        for (int i = 0; i < Input.Length; i++)
        {
            if (floor == -1)
                return i;
            if (Input[i] == '(')
                floor++;
            else
                floor--;
        }
        return -1;
    }
}
