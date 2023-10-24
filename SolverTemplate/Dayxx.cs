using AoC.Utils;

namespace Company.ClassLibrary1;

public class Dayxx: IDay
{
    public Dayxx(string input)
    {
        Input = input;
    }
    public string Output => throw new NotImplementedException();

    private string Input {get; set;}

    public int Star1()
    {
        return 0;
    }

    public int Star2()
    {
        return 0;
    }
}
