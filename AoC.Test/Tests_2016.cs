namespace AoC.Test;

using AoC.Solvers.Y2016;
public class Tests_2016
{
    [Theory]
    [InlineData("R2, L3", 5)]
    [InlineData("R2, R2, R2", 2)]
    [InlineData("R5, L5, R5, R3", 12)]
    public void Day1_Star1(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star1());
    }

    [Theory]
    [InlineData("R8, R4, R4, R8", 4)]
    public void Day1_Star2(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star2());
    }
}