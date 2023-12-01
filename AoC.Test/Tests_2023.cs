namespace AoC.Test;

using AoC.Solvers.Y2023;
public class Tests_2023
{
    
    [InlineData("""
        1abc2
        pqr3stu8vwx
        a1b2c3d4e5f
        treb7uchet
    """, 142)]
    [Theory]
    public void Day1_Star1(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
        two1nine
        eightwothree
        abcone2threexyz
        xtwone3four
        4nineeightseven2
        zoneight234
        7pqrstsixteen
    """, 281)]
    [Theory]
    public void Day1_Star2(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star2());
    }



}