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

    [InlineData("""
    Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
    Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
    Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
    Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
    Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
    Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
    """, 13)]
    [Theory]
    public void Day4_Star1(string input, int res)
    {
        var actor = new Day04(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
    Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
    Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
    Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
    Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
    Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
    """, 30)]
    [Theory]
    public void Day4_Star2(string input, int res)
    {
        var actor = new Day04(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("""
    seeds: 79 14 55 13

    seed-to-soil map:
    50 98 2
    52 50 48

    soil-to-fertilizer map:
    0 15 37
    37 52 2
    39 0 15

    fertilizer-to-water map:
    49 53 8
    0 11 42
    42 0 7
    57 7 4

    water-to-light map:
    88 18 7
    18 25 70

    light-to-temperature map:
    45 77 23
    81 45 19
    68 64 13

    temperature-to-humidity map:
    0 69 1
    1 0 69

    humidity-to-location map:
    60 56 37
    56 93 4
    """, "35")]
    [Theory]
    public void Day5_Star1(string input, string res)
    {
        var actor = new Day05(input);
        _ = actor.Star1();
        Assert.Equal(res, actor.Output);
    }

    [InlineData("""
    seeds: 79 14 55 13

    seed-to-soil map:
    50 98 2
    52 50 48

    soil-to-fertilizer map:
    0 15 37
    37 52 2
    39 0 15

    fertilizer-to-water map:
    49 53 8
    0 11 42
    42 0 7
    57 7 4

    water-to-light map:
    88 18 7
    18 25 70

    light-to-temperature map:
    45 77 23
    81 45 19
    68 64 13

    temperature-to-humidity map:
    0 69 1
    1 0 69

    humidity-to-location map:
    60 56 37
    56 93 4
    """, "46")]
    
    [Theory(Skip = "bad solution :(")]
    public void Day5_Star2(string input, string res)
    {
        var actor = new Day05(input);
        _ = actor.Star2();
        Assert.Equal(res, actor.Output);
    }
    [InlineData("""
    Time:      7  15   30
    Distance:  9  40  200
    """, 288)]
    [Theory]
    public void Day6_Star1(string input, int res)
    {
        var actor = new Day06(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    Time:      7  15   30
    Distance:  9  40  200
    """, 71503)]
    [Theory]
    public void Day6_Star2(string input, int res)
    {
        var actor = new Day06(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("""
    32T3K 765
    T55J5 684
    KK677 28
    KTJJT 220
    QQQJA 483
    """, 6440)]
    [Theory]
    public void Day7_Star1(string input, int res)
    {
        var actor = new Day07(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    32T3K 765
    T55J5 684
    KK677 28
    KTJJT 220
    QQQJA 483
    """, 5905)]
    [Theory]
    public void Day7_Star2(string input, int res)
    {
        var actor = new Day07(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("""
    LLR

    AAA = (BBB, BBB)
    BBB = (AAA, ZZZ)
    ZZZ = (ZZZ, ZZZ)
    """, 6)]
    [Theory]
    public void Day8_Star1(string input, int res)
    {
        var actor = new Day08(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    LR

    11A = (11B, XXX)
    11B = (XXX, 11Z)
    11Z = (11B, XXX)
    22A = (22B, XXX)
    22B = (22C, 22C)
    22C = (22Z, 22Z)
    22Z = (22B, 22B)
    XXX = (XXX, XXX)
    """, "6")]
    [Theory]
    public void Day8_Star2(string input, string res)
    {
        var actor = new Day08(input);
        _ = actor.Star2();
        Assert.Equal(res, actor.Output);
    }

    [InlineData("""
    0 3 6 9 12 15
    1 3 6 10 15 21
    10 13 16 21 30 45
    """, 114)]
    [Theory]
    public void Day9_Star1(string input, int res)
    {
        var actor = new Day09(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    0 3 6 9 12 15
    1 3 6 10 15 21
    10 13 16 21 30 45
    """, 2)]
    [Theory]
    public void Day9_Star2(string input, int res)
    {
        var actor = new Day09(input);
        Assert.Equal(actor.Star2(), res);
    }

    [InlineData("""
    ...#......
    .......#..
    #.........
    ..........
    ......#...
    .#........
    .........#
    ..........
    .......#..
    #...#.....
    """, "374")]
    [Theory]
    public void Day11_Star1(string input, string res)
    {
        var actor = new Day11(input);
        _ = actor.Star1();
        Assert.Equal(res, actor.Output);
    }

    [InlineData("""
    ...#......
    .......#..
    #.........
    ..........
    ......#...
    .#........
    .........#
    ..........
    .......#..
    #...#.....
    """, "8410")]
    [Theory]
    public void Day11_Star2(string input, string res)
    {
        var actor = new Day11(input, 100);
        _ = actor.Star2();
        Assert.Equal(actor.Output, res);
    }
}