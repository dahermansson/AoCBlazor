namespace AoC.AoCUtils;

public interface IDay
{
    int Star1();
    int Star2();
    string Output { get; }
}

public abstract class Day
{
    public Day(int year, int day)
    {
        #if DEBUG
            InputPath = @"bin\Debug\net8.0\Y2016\Inputs\01.txt";
        #endif
    }

    public string InputPath { get; set; }
}