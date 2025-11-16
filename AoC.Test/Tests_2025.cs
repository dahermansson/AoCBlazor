namespace AoC.Test;

using AoC.Solvers.Y2025;

public class Tests_2025
{
    [InlineData("""

    """, 1)]
    [Theory]
    public void Day1_Star1(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""

    """, 2)]
    [Theory]
    public void Day1_Star2(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star2());
    }
}