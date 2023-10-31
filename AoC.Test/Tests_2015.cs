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

    
    [InlineData("aaa", 0)]
    [InlineData("abcdefeghi", 0)]
    [InlineData("qjhvhtzxzqqjkmpb", 1)]
    [InlineData("xxyxx", 1)]
    [InlineData("uurcxstgmygtbstg", 0)]
    [InlineData("ieodomkazucvgmuy", 0)]
    [Theory]
    public void Day5_Star2(string input, int res)
    {
        var actor = new Day05(input);
        Assert.Equal(res, actor.Star2());
    }

    
    [InlineData("turn on 0,0 through 999,999", 1_000_000)]
    [InlineData("toggle 0,0 through 999,0", 1000)]
    [InlineData("turn off 499,499 through 500,500", 0)]
    [Theory]
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

    [InlineData("""
    123 -> x
    456 -> y
    x AND y -> d
    x OR y -> e
    x LSHIFT 2 -> f
    y RSHIFT 2 -> g
    NOT x -> h
    NOT y -> i
    """, 65079)]
    [Theory]
    public void Day7_Star1(string input, int res)
    {
        var actor = new Day07(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
        ""
        "abc"
        "aaa\"aaa"
        "\x27"
        """, 12)]
    [Theory]
    public void Day8_Star1(string input, int res)
    {
        var actor = new Day08(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
        ""
        "abc"
        "aaa\"aaa"
        "\x27"
        """, 19)]
    [Theory]
    public void Day8_Star2(string input, int res)
    {
        var actor = new Day08(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("""
        London to Dublin = 464
        London to Belfast = 518
        Dublin to Belfast = 141
        """, 605)]
    [Theory]
    public void Day9_Star1(string input, int res)
    {
        var actor = new Day09(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
        London to Dublin = 464
        London to Belfast = 518
        Dublin to Belfast = 141
        """, 982)]
    [Theory]
    public void Day9_Star2(string input, int res)
    {
        var actor = new Day09(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("1", 82350)]
    [Theory]
    public void Day10_Star1(string input, int res)
    {
        var actor = new Day10(input);
        Assert.Equal(res, actor.Star1());
    }
    [InlineData("1", 1166642)]
    [Theory]
    public void Day10_Star2(string input, int res)
    {
        var actor = new Day10(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("abcdefgh","abcdffaa")]
    [InlineData("ghijklmn", "ghjaabcc")]
    [InlineData("abcdffaa", "abcdffbb")]

    [Theory]
    public void Day11_Star1(string input, string res)
    {
        var actor = new Day11(input);
        actor.Star1();
        Assert.Equal(res, actor.Output);
    }

    [InlineData("abcdefgh", "abcdffbb")]
    [Theory]
    public void Day11_Star2(string input, string res)
    {
        var actor = new Day11(input);
        actor.Star2();
        Assert.Equal(res, actor.Output);
    }
    [InlineData("[1,2,3]", 6)]
    [InlineData("{\"a\":2,\"b\":4}", 6)]
    [InlineData("{\"a\":[-1,1]}", 0)]
    [Theory]
    public void Day12_Star1(string input, int res)
    {
        var actor = new Day12(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("{\"a\":2,\"b\":4}", 6)]
    [InlineData("[1,{\"c\":\"red\",\"b\":2},3]", 4)]
    [Theory]
    public void Day12_Star2(string input, int res)
    {
        var actor = new Day12(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("""
    Alice would gain 54 happiness units by sitting next to Bob.
    Alice would lose 79 happiness units by sitting next to Carol.
    Alice would lose 2 happiness units by sitting next to David.
    Bob would gain 83 happiness units by sitting next to Alice.
    Bob would lose 7 happiness units by sitting next to Carol.
    Bob would lose 63 happiness units by sitting next to David.
    Carol would lose 62 happiness units by sitting next to Alice.
    Carol would gain 60 happiness units by sitting next to Bob.
    Carol would gain 55 happiness units by sitting next to David.
    David would gain 46 happiness units by sitting next to Alice.
    David would lose 7 happiness units by sitting next to Bob.
    David would gain 41 happiness units by sitting next to Carol.
    """, 330)]
    [Theory]
    public void Day13_Star1(string input, int res)
    {
        var actor = new Day13(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    Alice would gain 54 happiness units by sitting next to Bob.
    Alice would lose 79 happiness units by sitting next to Carol.
    Alice would lose 2 happiness units by sitting next to David.
    Bob would gain 83 happiness units by sitting next to Alice.
    Bob would lose 7 happiness units by sitting next to Carol.
    Bob would lose 63 happiness units by sitting next to David.
    Carol would lose 62 happiness units by sitting next to Alice.
    Carol would gain 60 happiness units by sitting next to Bob.
    Carol would gain 55 happiness units by sitting next to David.
    David would gain 46 happiness units by sitting next to Alice.
    David would lose 7 happiness units by sitting next to Bob.
    David would gain 41 happiness units by sitting next to Carol.
    """, 286)]
    [Theory]
    public void Day13_Star2(string input, int res)
    {
        var actor = new Day13(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("""
    Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
    Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.
    """, 1120)]
    [Theory]
    public void Day14_Star1(string input, int res)
    {
        var actor = new Day14(input, 1000);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
    Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.
    """, 689)]
    [Theory]
    public void Day14_Star2(string input, int res)
    {
        var actor = new Day14(input, 1000);
        Assert.Equal(res, actor.Star2());
    }
}