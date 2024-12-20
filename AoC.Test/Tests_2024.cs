namespace AoC.Test;

using AoC.Solvers.Y2024;

public class Tests_2024
{
    [InlineData("""
    3   4
    4   3
    2   5
    1   3
    3   9
    3   3
    """, 11)]
    [Theory]
    public void Day1_Star1(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    3   4
    4   3
    2   5
    1   3
    3   9
    3   3
    """, 31)]
    [Theory]
    public void Day1_Star2(string input, int res)
    {
        var actor = new Day01(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("""
    7 6 4 2 1
    1 2 7 8 9
    9 7 6 2 1
    1 3 2 4 5
    8 6 4 4 1
    1 3 6 7 9
    """, 2)]
    [Theory]
    public void Day2_Star1(string input, int res)
    {
        var actor = new Day02(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    7 6 4 2 1
    1 2 7 8 9
    9 7 6 2 1
    1 3 2 4 5
    8 6 4 4 1
    1 3 6 7 9
    """, 4)]
    [InlineData("""
    5 10 4 2 1
    """, 1)]
    [InlineData("""
    1 3 5 6 8 9 12 9
    """, 1)]
    [Theory]
    public void Day2_Star2(string input, int res)
    {
        var actor = new Day02(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))", 161)]
    [Theory]
    public void Day3_Star1(string input, int res)
    {
        var actor = new Day03(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))", 48)]
    [Theory]
    public void Day3_Star2(string input, int res)
    {
        var actor = new Day03(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("""
    MMMSXXMASM
    MSAMXMSMSA
    AMXSXMAAMM
    MSAMASMSMX
    XMASAMXAMM
    XXAMMXXAMA
    SMSMSASXSS
    SAXAMASAAA
    MAMMMXMMMM
    MXMXAXMASX
    """, 18)]
    [Theory]
    public void Day4_Star1(string input, int res)
    {
        var actor = new Day04(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    MMMSXXMASM
    MSAMXMSMSA
    AMXSXMAAMM
    MSAMASMSMX
    XMASAMXAMM
    XXAMMXXAMA
    SMSMSASXSS
    SAXAMASAAA
    MAMMMXMMMM
    MXMXAXMASX
    """, 9)]
    [Theory]
    public void Day4_Star2(string input, int res)
    {
        var actor = new Day04(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("""
    47|53
    97|13
    97|61
    97|47
    75|29
    61|13
    75|53
    29|13
    97|29
    53|29
    61|53
    97|53
    61|29
    47|13
    75|47
    97|75
    47|61
    75|61
    47|29
    75|13
    53|13

    75,47,61,53,29
    97,61,53,29,13
    75,29,13
    75,97,47,61,53
    61,13,29
    97,13,75,29,47
    """, 143)]
    [Theory]
    public void Day5_Star1(string input, int res)
    {
        var actor = new Day05(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    47|53
    97|13
    97|61
    97|47
    75|29
    61|13
    75|53
    29|13
    97|29
    53|29
    61|53
    97|53
    61|29
    47|13
    75|47
    97|75
    47|61
    75|61
    47|29
    75|13
    53|13

    75,47,61,53,29
    97,61,53,29,13
    75,29,13
    75,97,47,61,53
    61,13,29
    97,13,75,29,47q
    """, 123)]
    [Theory]
    public void Day5_Star2(string input, int res)
    {
        var actor = new Day05(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("""
    ....#.....
    .........#
    ..........
    ..#.......
    .......#..
    ..........
    .#..^.....
    ........#.
    #.........
    ......#...
    """, 41)]
    [Theory]
    public void Day6_Star1(string input, int res)
    {
        var actor = new Day06(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    ....#.....
    .........#
    ..........
    ..#.......
    .......#..
    ..........
    .#..^.....
    ........#.
    #.........
    ......#...
    """, 6)]
    [Theory]
    public void Day6_Star2(string input, int res)
    {
        var actor = new Day06(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("""
    190: 10 19
    3267: 81 40 27
    83: 17 5
    156: 15 6
    7290: 6 8 6 15
    161011: 16 10 13
    192: 17 8 14
    21037: 9 7 18 13
    292: 11 6 16 20
    """, 3749)]
    [Theory]
    public void Day7_Star1(string input, int res)
    {
        var actor = new Day07(input);
        _ = actor.Star1();
        Assert.Equal(res.ToString(), actor.Output);
    }

    [InlineData("""
    190: 10 19
    3267: 81 40 27
    83: 17 5
    156: 15 6
    7290: 6 8 6 15
    161011: 16 10 13
    192: 17 8 14
    21037: 9 7 18 13
    292: 11 6 16 20
    """, 11387)]
    [Theory]
    public void Day7_Star2(string input, int res)
    {
        var actor = new Day07(input);
        _ = actor.Star2();
        Assert.Equal(res.ToString(), actor.Output);
    }

    [InlineData("""
    ............
    ........0...
    .....0......
    .......0....
    ....0.......
    ......A.....
    ............
    ............
    ........A...
    .........A..
    ............
    ............
    """, 14)]
    [Theory]
    public void Day8_Star1(string input, int res)
    {
        var actor = new Day08(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    ............
    ........0...
    .....0......
    .......0....
    ....0.......
    ......A.....
    ............
    ............
    ........A...
    .........A..
    ............
    ............
    """, 34)]
    [Theory]
    public void Day8_Star2(string input, int res)
    {
        var actor = new Day08(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("2333133121414131402", 1928)]
    [Theory]
    public void Day9_Star1(string input, int res)
    {
        var actor = new Day09(input);
        _ = actor.Star1();
        Assert.Equal(res.ToString(), actor.Output);
    }

    [InlineData("2333133121414131402", 2858)]
    [Theory]
    public void Day9_Star2(string input, int res)
    {
        var actor = new Day09(input);
        _ = actor.Star2();
        Assert.Equal(res.ToString(), actor.Output);
    }

    [InlineData("""
    89010123
    78121874
    87430965
    96549874
    45678903
    32019012
    01329801
    10456732
    """, 36)]
    [Theory]
    public void Day10_Star1(string input, int res)
    {
        var actor = new Day10(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    89010123
    78121874
    87430965
    96549874
    45678903
    32019012
    01329801
    10456732
    """, 81)]
    [Theory]
    public void Day10_Star2(string input, int res)
    {
        var actor = new Day10(input);
        Assert.Equal(res, actor.Star2());
    }

    [InlineData("125 17", 55312)]
    [Theory]
    public void Day11_Star1(string input, int res)
    {
        var actor = new Day11(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("125 17", "65601038650482")]
    [Theory]
    public void Day11_Star2(string input, string res)
    {
        var actor = new Day11(input);
        _ = actor.Star2();
        Assert.Equal(res, actor.Output);
    }

    [InlineData("""
    Button A: X+94, Y+34
    Button B: X+22, Y+67
    Prize: X=8400, Y=5400

    Button A: X+26, Y+66
    Button B: X+67, Y+21
    Prize: X=12748, Y=12176

    Button A: X+17, Y+86
    Button B: X+84, Y+37
    Prize: X=7870, Y=6450

    Button A: X+69, Y+23
    Button B: X+27, Y+71
    Prize: X=18641, Y=10279
    """, "480")]
    [Theory]
    public void Day13_Star1(string input, string res)
    {
        var actor = new Day13(input);
        _ = actor.Star1();
        Assert.Equal(res, actor.Output);
    }

    [InlineData("""
    Button A: X+94, Y+34
    Button B: X+22, Y+67
    Prize: X=8400, Y=5400
    
    Button A: X+26, Y+66
    Button B: X+67, Y+21
    Prize: X=12748, Y=12176
    
    Button A: X+17, Y+86
    Button B: X+84, Y+37
    Prize: X=7870, Y=6450
    
    Button A: X+69, Y+23
    Button B: X+27, Y+71
    Prize: X=18641, Y=10279
    """, "875318608908")]
    [Theory]
    public void Day13_Star2(string input, string res)
    {
        var actor = new Day13(input);
        _ = actor.Star2();
        Assert.Equal(res, actor.Output);
    }

    [InlineData("""
    p=0,4 v=3,-3
    p=6,3 v=-1,-3
    p=10,3 v=-1,2
    p=2,0 v=2,-1
    p=0,0 v=1,3
    p=3,0 v=-2,-2
    p=7,6 v=-1,-3
    p=3,0 v=-1,-2
    p=9,3 v=2,3
    p=7,3 v=-1,2
    p=2,4 v=2,-3
    p=9,5 v=-3,-3
    """, 12)]
    [Theory]
    public void Day14_Star1(string input, int res)
    {
        var actor = new Day14(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    Register A: 729
    Register B: 0
    Register C: 0

    Program: 0,1,5,4,3,0
    """, "4,6,3,5,6,3,5,2,1,0")]
    [InlineData("""
    Register A: 10
    Register B: 0
    Register C: 0

    Program: 5,0,5,1,5,4
    """, "0,1,2")]
    [InlineData("""
    Register A: 2024
    Register B: 0
    Register C: 0

    Program: 0,1,5,4,3,0
    """, "4,2,5,6,7,7,7,7,3,1,0")]
    [Theory]
    public void Day17_Star1(string input, string res)
    {
        var actor = new Day17(input);
        _ = actor.Star1();
        Assert.Equal(res, actor.Output);
    }

    [InlineData("""
    r, wr, b, g, bwu, rb, gb, br

    brwrr
    bggr
    gbbr
    rrbgbr
    ubwu
    bwurrg
    brgr
    bbrgwb
    """, 6)]
    [Theory]
    public void Day19_Star1(string input, int res)
    {
        var actor = new Day19(input);
        Assert.Equal(res, actor.Star1());
    }

    [InlineData("""
    r, wr, b, g, bwu, rb, gb, br

    brwrr
    bggr
    gbbr
    rrbgbr
    ubwu
    bwurrg
    brgr
    bbrgwb
    """, "16")]
    [Theory]
    public void Day19_Star2(string input, string res)
    {
        var actor = new Day19(input);
        _ = actor.Star2();
        Assert.Equal(res, actor.Output);
    }
}
