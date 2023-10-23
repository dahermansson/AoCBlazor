namespace AoC.Test;

using AoC.Solvers.Y2015;
public class Tests_2015
{
    [Theory]
    [InlineData("(())", 0)]
    [InlineData("()()", 0)]
    [InlineData("))(((((", 3)]
    [InlineData("())", -1)]
    [InlineData(")())())", -3)]
    public void Day1_Star1(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star1());
    }

    [Theory]
    [InlineData("()())", -1)]
    public void Day1_Star2(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star2());
    }
}