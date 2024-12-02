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

    [InlineData("""
    7 6 4 2 1
    1 2 7 8 9
    9 7 6 2 1
    1 3 2 4 5
    8 6 4 4 1
    1 3 6 7 9
    """, 2)]
    [Theory]
    public void Day2_Star1(string input, int res)
    {
        var actor = new Day02(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    7 6 4 2 1
    1 2 7 8 9
    9 7 6 2 1
    1 3 2 4 5
    8 6 4 4 1
    1 3 6 7 9
    """, 4)]
    [InlineData("""
    5 10 4 2 1
    """, 1)]
    [InlineData("""
    1 3 5 6 8 9 12 9
    """, 1)]
    [Theory]
    public void Day2_Star2(string input, int res)
    {
        var actor = new Day02(input);
        Assert.Equal(res, actor.Star2());
    }
}
