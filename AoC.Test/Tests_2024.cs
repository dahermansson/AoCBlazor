namespace AoC.Test;

using AoC.Solvers.Y2024;

public class Tests_2024
{
    [InlineData("""
    3   4
    4   3
    2   5
    1   3
    3   9
    3   3
    """, 11)]
    [Theory]
    public void Day1_Star1(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    3   4
    4   3
    2   5
    1   3
    3   9
    3   3
    """, 31)]
    [Theory]
    public void Day1_Star2(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star2());
    }
}
