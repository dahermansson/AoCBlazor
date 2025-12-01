namespace AoC.Test;

using AoC.Solvers.Y2025;

public class Tests_2025
{
    [InlineData("""
    L68
    L30
    R48
    L5
    R60
    L55
    L1
    L99
    R14
    L82
    """, 3)]
    [Theory]
    public void Day1_Star1(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    L68
    L30
    R48
    L5
    R60
    L55
    L1
    L99
    R14
    L82
    """, 6)]
    [Theory]
    public void Day1_Star2(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star2());
    }
}