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

    [Theory]
    [InlineData("2x3x4", 58)]
    [InlineData("1x1x10", 43)]
    public void Day2_Star1(string input, int res)
    {
        var actor = new Day02(input);
        Assert.Equal(res, actor.Star1());
    }

    [Theory]
    [InlineData("2x3x4", 34)]
    [InlineData("1x1x10", 14)]
    public void Day2_Star2(string input, int res)
    {
        var actor = new Day02(input);
        Assert.Equal(res, actor.Star2());
    }

    [Theory]
    [InlineData(">", 2)]
    [InlineData("^>v<", 4)]
    [InlineData("^v^v^v^v^v", 2)]
    public void Day3_Star1(string input, int res)
    {
        var actor = new Day03(input);
        Assert.Equal(res, actor.Star1());
    }

    [Theory]
    [InlineData("^v", 3)]
    [InlineData("^>v<", 3)]
    [InlineData("^v^v^v^v^v", 11)]
    public void Day3_Star2(string input, int res)
    {
        var actor = new Day03(input);
        Assert.Equal(res, actor.Star2());
    }

    [Theory]
    [InlineData("abcdef", 609043)]
    [InlineData("pqrstuv", 1048970)]
    public void Day4_Star1(string input, int res)
    {
        var actor = new Day04(input);
        Assert.Equal(res, actor.Star1());
    }

    [Theory]
    [InlineData("ugknbfddgicrmopn", 1)]
    [InlineData("aaa", 1)]
    [InlineData("jchzalrnumimnmhp", 0)]
    [InlineData("haegwjzuvuyypxyu", 0)]
    [InlineData("dvszwmarrgswjxmb", 0)]
    public void Day5_Star1(string input, int res)
    {
        var actor = new Day05(input);
        Assert.Equal(res, actor.Star1());
    }

    [Theory]
    [InlineData("^v", 3)]
    [InlineData("^>v<", 3)]
    [InlineData("^v^v^v^v^v", 11)]
    public void Day5_Star2(string input, int res)
    {
        var actor = new Day05(input);
        Assert.Equal(res, actor.Star2());
    }

    [Theory]
    [InlineData("turn on 0,0 through 999,999", 1_000_000)]
    [InlineData("toggle 0,0 through 999,0", 1000)]
    [InlineData("turn off 499,499 through 500,500", 0)]
    public void Day6_Star1(string input, int res)
    {
        var actor = new Day06(input);
        Assert.Equal(res, actor.Star1());
    }

    [Theory]
    [InlineData("turn on 0,0 through 0,0", 1)]
    [InlineData("toggle 0,0 through 999,999", 2000_000)]
    public void Day6_Star2(string input, int res)
    {
        var actor = new Day06(input);
        Assert.Equal(res, actor.Star2());
    }
}