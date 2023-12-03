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

    [InlineData("""
    Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
    Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
    Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
    Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
    Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
    """, 8)]
    [Theory]
    public void Day2_Star1(string input, int res)
    {
        var actor = new Day02(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
    Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
    Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
    Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
    Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
    """, 2286)]
    [Theory]
    public void Day2_Star2(string input, int res)
    {
        var actor = new Day02(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("""
    467..114..
    ...*......
    ..35..633.
    ......#...
    617*......
    .....+.58.
    ..592.....
    ......755.
    ...$.*....
    .664.598..
    """, 4361)]
    [Theory]
    public void Day3_Star1(string input, int res)
    {
        var actor = new Day03(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    467..114..
    ...*......
    ..35..633.
    ......#...
    617*......
    .....+.58.
    ..592.....
    ......755.
    ...$.*....
    .664.598..
    """, 467835)]
    [Theory]
    public void Day3_Star2(string input, int res)
    {
        var actor = new Day03(input);
        Assert.Equal(res, actor.Star2());
    }
}