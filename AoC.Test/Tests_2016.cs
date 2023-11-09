namespace AoC.Test;

using AoC.Solvers.Y2016;
public class Tests_2016
{
    
    [InlineData("R2, L3", 5)]
    [InlineData("R2, R2, R2", 2)]
    [InlineData("R5, L5, R5, R3", 12)]
    [Theory]
    public void Day1_Star1(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("R8, R4, R4, R8", 4)]
    [Theory]
    public void Day1_Star2(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("abc", "18f47a30")]
    [Theory]
    public void Day5_Star1(string input, string res)
    {
        var actor = new Day05(input);
        _ = actor.Star1();
        Assert.Equal(res, actor.Output);
    }

    [InlineData("abc", "05ace8e3")]
    [Theory]
    public void Day5_Star2(string input, string res)
    {
        var actor = new Day05(input);
        _ = actor.Star2();
        Assert.Equal(res, actor.Output);
    }
}